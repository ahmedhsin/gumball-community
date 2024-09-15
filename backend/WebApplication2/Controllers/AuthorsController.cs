using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Models;
using SocialMediaApp.Models.SocialMediaApp.Models;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public AuthorsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor([FromBody] Author model)
        {
        
            dbContext.Authors.Add(model);
            await dbContext.SaveChangesAsync();
            return Ok();

        }



    }
}
