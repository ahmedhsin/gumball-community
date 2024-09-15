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
             var checkreact = await dbContext.Reactions
                .FirstOrDefaultAsync(r => r.AuthorId == model.AuthorId && r.PostId == model.PostId);

            if (checkreact != null)
            {
                if (checkreact.ReactionType == model.ReactionType)
                {
                    dbContext.Reactions.Remove(checkreact);
                    await dbContext.SaveChangesAsync();
                    return Ok($"You have deleted The Reaction ");

                }
                else
                {
                    checkreact.ReactionType = model.ReactionType;
                    await dbContext.SaveChangesAsync();
                    return Ok($"You have updated The Reaction ");

                }

            }
            else
            {
                var react = new Reaction
                {

                    AuthorId = model.AuthorId,
                    PostId = model.PostId,
                    ReactionType = model.ReactionType,

                };

                dbContext.Reactions.Add(react);
                await dbContext.SaveChangesAsync();
                return Ok($"You have added a new Reaction {react.Id}");

            }
        }
    }
}