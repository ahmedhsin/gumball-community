using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        
        private int GetCurrentUserId()
        {
            
            return 1;
        }
        private readonly SocialMediaContext _context;

        public CommentsController(SocialMediaContext context)
        {
            _context = context;
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<List<CommentViewModel>>> GetCommentsByPostId(int postId)
        {
            var comments = await _context.Comments
                .Include(c => c.Author)
                .Where(c => c.PostId == postId)
                .ToListAsync();

            var commentViewModels = MapComments(comments);

            return Ok(commentViewModels);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] CreateCommentViewModel model)
        {
            var comment = new Comment
            {
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                PostId = model.PostId,
                AuthorId = GetCurrentUserId(),
                ParentId = model.ParentId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentsByPostId), new { postId = comment.PostId }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentViewModel model)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            comment.Content = model.Content;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private List<CommentViewModel> MapComments(List<Comment> comments)
        {
            var commentDictionary = comments
                .Where(c => c.ParentId == null)
                .ToDictionary(c => c.Id, c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    Author = new AuthorViewModel
                    {
                        Id = c.Author.Id,
                        Name = c.Author.FirstName + ' ' + c.Author.LastName,
                        ProfileImageUrl = c.Author.ProfileImageUrl
                    },
                    SubComments = new List<CommentViewModel>()
                });

            foreach (var comment in comments)
            {
                if (comment.ParentId != null)
                {
                    if (commentDictionary.TryGetValue(comment.ParentId.Value, out var parentCommentViewModel))
                    {
                        parentCommentViewModel.SubComments.Add(new CommentViewModel
                        {
                            Id = comment.Id,
                            Content = comment.Content,
                            CreatedAt = comment.CreatedAt,
                            Author = new AuthorViewModel
                            {
                                Id = comment.Author.Id,
                                Name = comment.Author.FirstName + ' ' + comment.Author.LastName,
                                ProfileImageUrl = comment.Author.ProfileImageUrl
                            },
                        });
                    }
                }
            }

            return commentDictionary.Values.ToList();
        }
    }
}
