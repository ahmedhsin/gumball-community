using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Enums;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ReactionsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateReact(ReactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var react = new Reaction
            {

                AuthorId = model.AuthorId,
                PostId = model.PostId,
                ReactionType = model.ReactionType,
          
            };

            dbContext.Reactions.Add(react);
            await dbContext.SaveChangesAsync();

            return Ok(react.Id);


        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteReact([FromRoute] int Id)
        {
            var React = await dbContext.Reactions.FindAsync(Id);

            if (React == null)
            {
                return NotFound("No Reactions found.");
            }

            // Use Entity Framework Remove method to handle cascade delete
            dbContext.Reactions.Remove(React);
            await dbContext.SaveChangesAsync(); // This will trigger the cascade delete

            return Ok();
        }
    }
}