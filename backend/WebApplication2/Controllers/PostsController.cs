using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Models;
using SocialMediaApp.Models.SocialMediaApp.Models;
using SocialMediaApp.ViewModels;
using SocialMediaApp.Controllers;
namespace SocialMediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly AppDbContext dbContext;

        public PostsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]

        public async Task<IActionResult> CreatePost(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Path where images will be saved
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            string uniqueFileName = null;

            // Handle image upload if a file is provided
            if (model.Photo != null)
            {
                // Ensure the uploads folder exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique file name to avoid conflicts // (unique photo name)
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

                // Create the full path to store the image
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
            }
           // model.HumanizedDate = Humanizer.DateHumanizeExtensions.Humanize(DateTime.Now);
            // Create a new Post object with the image path
            var post = new Post
            {

                AuthorId = model.AuthorId,
                Content = model.Content,
                CreatedAt = DateTime.Now,
                MyReaction = model.MyReaction,
                MediaPath = uniqueFileName != null ? "/uploads/" + uniqueFileName : null // Store the relative path
            };

            dbContext.Posts.Add(post);
            await dbContext.SaveChangesAsync();

            return Ok(post.Id);
        }




        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await dbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Reactions)
                .Include(p => p.Comments)
                .ThenInclude(c => c.SubComments)
                .ToListAsync();

            if (posts == null || !posts.Any())
                return NotFound("No posts found.");

            var postViewModels = posts.Select(post => new PostViewModel
            {
                Id = post.Id,
                AuthorId = post.AuthorId,
                AuthorName = $"{post.Author.FirstName} {post.Author.LastName}",
                //AuthorProfileImage = "assets/images/profile.png", // Mocked for now
                //check if the us
                MyReaction = dbContext.Reactions
                            .Where(r => r.PostId == post.Id && r.AuthorId == 1)
                            .Select(r => r.ReactionType)
                            .FirstOrDefault(),
                Content = post.Content,
                HumanizedDate = Humanizer.DateHumanizeExtensions.Humanize(post.CreatedAt),
                MediaPath = post.MediaPath, //?? "assets/images/post-img.png",
                Reactions = post.Reactions
                    .GroupBy(r => r.ReactionType)
                    .ToDictionary(g => (int)g.Key, g => g.Count()), // Reaction count by type
                Comments = post.Comments.Select(c => new CommentViewModel
                {
                    //AuthorProfileImage = "assets/images/profile.png", 
                    Id = c.Id,
                    AuthorId=c.AuthorId,
                    ParentCommentId=c.ParentCommentId,
                    AuthorName = $"{c.Author.FirstName} {c.Author.LastName}",
                    Content = c.Content,
                    HumanizedDate = Humanizer.DateHumanizeExtensions.Humanize(c.CreatedAt),
                    SubComments = c.SubComments.Select(sc => new CommentViewModel
                    {
                        //AuthorProfileImage = "assets/images/profile.png", 
                        Id = sc.Id,
                        AuthorId = sc.AuthorId,
                        ParentCommentId = sc.ParentCommentId,
                        AuthorName = $"{sc.Author.FirstName} {sc.Author.LastName}",
                        Content = sc.Content,
                        HumanizedDate = Humanizer.DateHumanizeExtensions.Humanize(sc.CreatedAt)
                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(postViewModels);
        }







        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePost([FromRoute] int Id)
        {
            var post = await dbContext.Posts
                                      .Include(p => p.Comments)
                                      .ThenInclude(c => c.SubComments)
                                      .Include(p => p.Reactions)
               
                                      .FirstOrDefaultAsync(p => p.Id == Id);


            if (post == null)
            {
                return NotFound("No Posts found.");
            }

            // Manually delete reactions
            if(post.Reactions!=null)
            dbContext.Reactions.RemoveRange(post.Reactions);

            // Manually delete comments and subcomments
            if (post.Comments != null)
            {
                foreach (var comment in post.Comments)
                {
                    await DeleteCommentRecursive(comment);
                }
            }

            // Remove the post after removing related entities
            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
        async Task DeleteCommentRecursive(Comment comment)
        {
            // If the comment has subcomments, recursively delete them
            if (comment.SubComments != null && comment.SubComments.Any())
            {
                foreach (var subComment in comment.SubComments)
                {
                    // Load subcomments of the current subcomment
                    await dbContext.Entry(subComment).Collection(c => c.SubComments).LoadAsync();

                    // Recursively delete each subcomment
                    await DeleteCommentRecursive(subComment);
                }
            }

            // Remove the current comment
            dbContext.Comments.Remove(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] String content)
        {
            var post = await dbContext.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            post.Content = content;
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
