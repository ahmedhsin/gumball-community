using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaApp.Data;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly AppDbContext dbContext;

        public CommentsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentToPost(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var comment = new Comment
            {
                AuthorId = model.AuthorId,
                PostId = model.PostId == 0 || model.PostId == null ? null : model.PostId,
                ParentCommentId = model.ParentCommentId == 0 || model.ParentCommentId == null ? null : model.ParentCommentId,
                Content = model.Content,
                CreatedAt = DateTime.Now
            };
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();

            return Ok(comment.Id);


        }


        [HttpGet]
        public async Task<ActionResult<List<CommentViewModel>>> GetAll()
        {
            var result = await dbContext.Comments.Select(a => new CommentViewModel
            {
                Id = a.Id,
                AuthorId = a.AuthorId,
                PostId = a.PostId,
                ParentCommentId = a.ParentCommentId,
                Content = a.Content
            }).ToListAsync();

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteComment([FromRoute] int Id)
        {
            // Find the comment including its subcomments
            var comment = await dbContext.Comments.Include(c => c.SubComments).FirstOrDefaultAsync(c => c.Id == Id);

            if (comment == null)
            {
                return NotFound("No Comments found.");
            }

            // Recursive function to delete a comment and its subcomments
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

            // Call the recursive delete function starting with the root comment
            await DeleteCommentRecursive(comment);

            // Save changes after deleting the hierarchy
            await dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}