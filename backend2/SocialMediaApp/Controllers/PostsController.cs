using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SocialMediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly SocialMediaContext _context;

        public PostsController(SocialMediaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostViewModel>>> GetAll()
        {
            var userId = GetCurrentUserId();
            var followedAuthorIds = await _context.AuthorFollows
                .Where(af => af.FollowerId == userId)
                .Select(af => af.FollowingId)
                .ToListAsync();
            
            var posts = await _context.Posts
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author) 
                .Include(p => p.Author)   
                .Include(p => p.Reactions)
                .OrderByDescending(p => followedAuthorIds.Contains(p.AuthorId))
                .ThenByDescending(p => p.CreatedAt)
                .ToListAsync();

            var postViewModels = posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Author = new AuthorViewModel
                {
                    Id = p.Author.Id,
                    Name = p.Author.FirstName + ' ' + p.Author.LastName
                },
                Comments = MapComments(p.Comments),
                Reactions = p.Reactions
                    .GroupBy(r => r.react) 
                    .ToDictionary(g => g.Key, g => g.Count()), 
                React = p.Reactions.FirstOrDefault(r => r.AuthorId == userId)?.react.ToString()
            }).ToList();

            return Ok(postViewModels);
        }

        [HttpGet("Author/{authorId}")]
        public async Task<ActionResult<List<PostViewModel>>> GetAllByAuthor(int authorId)
        {
            var posts = await _context.Posts
                .Where(p => p.Author.Id == authorId)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .Include(p => p.Author)
                .Include(p => p.Reactions)
                .ToListAsync();

            var postViewModels = posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Author = new AuthorViewModel
                {
                    Id = p.Author.Id,
                    Name = p.Author.FirstName + ' ' + p.Author.LastName
                },
                Comments = MapComments(p.Comments),
                Reactions = p.Reactions
                    .GroupBy(r => r.react)
                    .ToDictionary(g => g.Key, g => g.Count()),
                React = p.Reactions.FirstOrDefault(r => r.AuthorId == GetCurrentUserId())?.react.ToString()
            }).ToList();

            return Ok(postViewModels);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePostViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Post model cannot be null.");
            }

            string imageUrl = "";
            if (model.ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imageUrl = $"/images/{model.ImageFile.FileName}";
            }
            var newPost = new Post
            {
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = imageUrl,
                AuthorId = GetCurrentUserId()
            };

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id }, newPost);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewModel>> GetPostById([FromRoute] int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .Include(p => p.Author)
                .Include(p => p.Reactions)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Author = new AuthorViewModel
                {
                    Id = post.Author.Id,
                    Name = post.Author.FirstName + ' ' + post.Author.LastName,
                    ProfileImageUrl = post.Author.ProfileImageUrl
                },
                Comments = MapComments(post.Comments),
                Reactions = post.Reactions
                    .GroupBy(r => r.react)
                    .ToDictionary(g => g.Key, g => g.Count()),
                React = post.Reactions.FirstOrDefault(r => r.AuthorId == GetCurrentUserId())?.react.ToString()
            };

            return Ok(postViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePostViewModel updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            post.Content = updatedPost.Content;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private List<CommentViewModel> MapComments(List<Comment> comments)
        {
            return comments.Where(c => c.ParentId == null)
                .Select(c => new CommentViewModel
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
                    SubComments = GetNestedComments(c.Id)
                }).ToList();
        }

        private List<CommentViewModel> GetNestedComments(int parentId)
        {
            var comments = _context.Comments
                .Where(c => c.ParentId == parentId)
                .Include(c => c.Author)
                .ToList();

            return comments.Select(c => new CommentViewModel
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
                SubComments = GetNestedComments(c.Id) 
            }).ToList();
        }

        private async Task<string> HandelImage(CreatePostViewModel model)
        {
            if (model.ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                return $"/images/{model.ImageFile.FileName}";
            }

            return null;
        }
        private int GetCurrentUserId()
        {
            return 1;
        }
    }
}
