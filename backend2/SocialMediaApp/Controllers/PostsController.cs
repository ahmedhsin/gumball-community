using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SocialMediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly AuthService _authService;
        public PostsController(SocialMediaContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<PostViewModel>>> GetAll()
        {
            var userId = _authService.GetCurrentUserId();
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
                CreatedAt = p.CreatedAt.Humanize(),
                ImageUrl = p.ImageUrl,
                Author = new AuthorViewModel
                {
                    Id = p.Author.Id,
                    Name = p.Author.FirstName + ' ' + p.Author.LastName,
                    ProfileImageUrl = p.Author.ProfileImageUrl
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
                ImageUrl = p.ImageUrl,
                Content = p.Content,
                CreatedAt = p.CreatedAt.Humanize(),
                Author = new AuthorViewModel
                {
                    Id = p.Author.Id,
                    Name = p.Author.FirstName + ' ' + p.Author.LastName,
                    ProfileImageUrl = p.Author.ProfileImageUrl
                    
                },
                Comments = MapComments(p.Comments),
                Reactions = p.Reactions
                    .GroupBy(r => r.react)
                    .ToDictionary(g => g.Key, g => g.Count()),
                React = p.Reactions.FirstOrDefault(r => r.AuthorId == _authService.GetCurrentUserId())?.react.ToString()
            }).ToList();

            return Ok(postViewModels);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromForm] IFormFile? imageFile, [FromForm] string content)
        {
            if (content == null && imageFile == null)
            {
                return BadRequest("Post model cannot be null.");
            }

            string imageUrl = "";
            if (imageFile != null)
            {
                // Ensure directory exists
                var uploadPath = Path.Combine("wwwroot", "images");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Generate a unique file name to avoid overwriting
                var fileName = Path.GetFileName(imageFile.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Generate the URL to access the image
                imageUrl = $"/images/{uniqueFileName}";
            }
            var newPost = new Post
            {
                Content = content,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = imageUrl,
                AuthorId = _authService.GetCurrentUserId()
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
                CreatedAt = post.CreatedAt.Humanize(),
                ImageUrl = post.ImageUrl,
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
                React = post.Reactions.FirstOrDefault(r => r.AuthorId == _authService.GetCurrentUserId())?.react.ToString()
            };

            return Ok(postViewModel);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }
            var author = _authService.GetCurrentUserId();

            if (post.AuthorId != author)
            {
                throw new UnauthorizedAccessException();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePostViewModel updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }
            var author = _authService.GetCurrentUserId();

            if (post.AuthorId != author)
            {
                throw new UnauthorizedAccessException();
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
                    CreatedAt = c.CreatedAt.Humanize(),
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
                CreatedAt = c.CreatedAt.Humanize(),
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
        
    }
}
