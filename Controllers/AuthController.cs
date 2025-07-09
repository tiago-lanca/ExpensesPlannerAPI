using ExpensesPlannerAPI.DTO;
using ExpensesPlannerAPI.Models;
using ExpensesPlannerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpensesPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwt;

        public AuthController(ILogger<AuthController> logger, UserManager<ApplicationUser> userManager, JwtService jwt)
        {
            _logger = logger;
            _userManager = userManager;
            _jwt = jwt;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<ApplicationUser>> GetMe()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId is null)
            {
                return Unauthorized("User not authenticated.");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if(user is null) return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var (token, expiration) = _jwt.GenerateToken(user);
            return Ok(new { token, expiration });
        }
    }
}
