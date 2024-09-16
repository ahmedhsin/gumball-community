using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly AuthService _authService;

        private int GetCurrentUserId()
        {
            
            return 1;
        }
        public ReactController(SocialMediaContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<ReactViewModel>>> GetAll()
        {
            var reacts = await _context.Reactions
                .Include(r => r.Author)
                .Include(r => r.Post)
                .Select(r => new ReactViewModel
                {
                    Id = r.Id,
                    React = r.react,
                    Author = new AuthorViewModel
                    {
                        Id = r.Author.Id,
                        Name = r.Author.FirstName + ' ' + r.Author.LastName,
                        ProfileImageUrl = r.Author.ProfileImageUrl
                    },
                    PostId = r.PostId
                })
                .ToListAsync();

            return Ok(reacts);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ReactViewModel>> GetById(int id)
        {
            var react = await _context.Reactions
                .Include(r => r.Author)
                .Include(r => r.Post)
                .Where(r => r.Id == id)
                .Select(r => new ReactViewModel
                {
                    Id = r.Id,
                    React = r.react,
                    Author = new AuthorViewModel
                    {
                        Id = r.Author.Id,
                        Name = r.Author.FirstName + ' ' + r.Author.LastName,
                        ProfileImageUrl = r.Author.ProfileImageUrl
                    },
                    PostId = r.PostId
                })
                .FirstOrDefaultAsync();

            if (react == null)
            {
                return NotFound();
            }

            return Ok(react);
        }

        
        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CreateReactViewModel model)
        {
            var authorId = _authService.GetCurrentUserId();
            var react = await _context.Reactions
                .FirstOrDefaultAsync(r => r.AuthorId == authorId && r.PostId == model.PostId);
            if (react == null)
            {
                react = new React
                {
                    react = model.React,
                    AuthorId = authorId,
                    PostId = model.PostId
                };
                _context.Reactions.Add(react);
            }else if (react.react == model.React)
            {
                _context.Reactions.Remove(react);
                
            }
            else
            {
                react.react = model.React;    
            }
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
