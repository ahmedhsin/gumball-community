using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Models;
using SocialMediaApp.Models.SocialMediaApp.Models;

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
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var Authors = await dbContext.Authors.ToListAsync();
            return Ok(Authors);


        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Author>> GeAuthourByID([FromRoute] int id)
        {
            var Author = await dbContext.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (Author == null)
            {
                return NotFound("Author No found please try another ID ");
            }
            return Ok(Author);
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
