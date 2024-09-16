using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly AuthService _authService;
        public AuthorsController(SocialMediaContext context, AuthService authService)
        {
            
            _context = context;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorViewModel>>> GetAll()
        {
            var authors = await _context.Authors
                .Select(a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.FirstName + ' ' + a.LastName,
                    Email = a.Email
                })
                .ToListAsync();

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorViewModel>> GetById(int id)
        {
            var author = await _context.Authors
                .Where(a => a.Id == id)
                .Select(a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.FirstName + ' ' + a.LastName,
                    Email = a.Email
                })
                .FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateAuthorViewModel model)
        {
            
            string imageUrl = "profile.png";
            if (model.ImageFile != null)
            {
                var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                
                imageUrl = $"/images/{model.ImageFile.FileName}";
            }
            var author = new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                ProfileImageUrl = imageUrl,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok("User registered");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (author == null || !BCrypt.Net.BCrypt.Verify(model.Password, author.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _authService.GenerateJwtToken(author);
            return Ok(new { token });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAuthorViewModel model)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            author.FirstName = model.FirstName;
            author.LastName = model.LastName;
            author.Email = model.Email;
            author.Password = BCrypt.Net.BCrypt.HashPassword(model.Password); 

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
