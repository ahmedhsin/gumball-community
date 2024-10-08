using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorFollowController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly AuthService _authService;

        public AuthorFollowController(SocialMediaContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpGet("followers/{authorId}")]
        public async Task<ActionResult<AuthorFollowViewModel>> GetFollowers(int authorId)
        {
            var followers = await _context.AuthorFollows
                .Where(f => f.FollowingId == authorId)
                .Include(f => f.Follower)
                .Select(f => f.Follower)
                .ToListAsync();

            var result = new AuthorFollowViewModel
            {
                Authors = followers
            };

            return Ok(result);
        }

        [HttpGet("following/{authorId}")]
        public async Task<ActionResult<AuthorFollowViewModel>> GetFollowing(int authorId)
        {
            var following = await _context.AuthorFollows
                .Where(f => f.FollowerId == authorId)
                .Include(f => f.Following)
                .Select(f => f.Following)
                .ToListAsync();

            var result = new AuthorFollowViewModel
            {
                Authors = following
            };

            return Ok(result);
        }
        
        [HttpGet("isfollowing/{authorId}")]
        [Authorize]
        public async Task<ActionResult> IsFollowers(int authorId)
        {
            var currentUserId = _authService.GetCurrentUserId();
            var existingFollow = await _context.AuthorFollows
                .Where(af => af.FollowerId == currentUserId && af.FollowingId == authorId)
                .FirstOrDefaultAsync();
            return Ok(existingFollow);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateFollow([FromBody] CreateAuthorFollowerViewModel model)
        {
            var currentUserId = _authService.GetCurrentUserId(); 

            var existingFollow = await _context.AuthorFollows
                .Where(af => af.FollowerId == currentUserId && af.FollowingId == model.FollowingId)
                .FirstOrDefaultAsync();

            if (existingFollow != null)
            {
                return BadRequest("Already following this user.");
            }

            var follow = new AuthorFollow
            {
                FollowerId = currentUserId,
                FollowingId = model.FollowingId
            };

            _context.AuthorFollows.Add(follow);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFollowers), new { authorId = follow.FollowerId }, follow);
        }

        [HttpDelete("{followingId}")]
        [Authorize]
        public async Task<IActionResult> Unfollow(int followingId)
        {
            var currentUserId = _authService.GetCurrentUserId();

            var follow = await _context.AuthorFollows
                .Where(af => af.FollowerId == currentUserId && af.FollowingId == followingId)
                .FirstOrDefaultAsync();

            if (follow == null)
            {
                return NotFound("You are not following this user.");
            }

            _context.AuthorFollows.Remove(follow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private int GetCurrentUserId()
        {
            return 1;
        }
    }
}
