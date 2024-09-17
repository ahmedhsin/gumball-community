using Microsoft.AspNetCore.Authorization;
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
                    Email = a.Email,
                    ProfileImageUrl = a.ProfileImageUrl
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
                    Email = a.Email,
                    ProfileImageUrl = a.ProfileImageUrl
                })
                .FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromForm] IFormFile? imageFile,
            [FromForm] string firstName,
            [FromForm] string lastName,
            [FromForm] string email,
            [FromForm] string password)
        {
            
            string imageUrl = "/images/profile.png";
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
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                ProfileImageUrl = imageUrl,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok();
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
            var data = new
            {
                Token = token,
                Author = new AuthorViewModel
                {
                    ProfileImageUrl = author.ProfileImageUrl,
                    Name = author.FirstName + ' ' + author.LastName,
                    Email = author.Email,
                    Id = author.Id
                }

            };
            return Ok(data);
        }

        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CreateAuthorViewModel model)
        {
            var author = await _context.Authors.FindAsync(_authService.GetCurrentUserId());

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
