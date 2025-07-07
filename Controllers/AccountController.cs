using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ExpensesPlannerAPI.DTO;
using ExpensesPlannerAPI.Models;
using ExpensesPlannerAPI.Services.Interfaces;

namespace ExpensesPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUsersRepository _usersRepository;
        public AccountController(IUsersRepository usersRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _usersRepository = usersRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("users")]
        public async Task<List<UserDetails>> GetAllUsers()
        {
            try
            {
                var users = await Task.FromResult(_userManager.Users.ToList());

                List<UserDetails> userDetailsList = users.Select(user => new UserDetails
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    ProfilePictureUrl = user.ProfilePictureUrl,
                    Role = user.Role
                }).ToList();

                return userDetailsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                return new List<UserDetails>();
            }
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user =await _usersRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user)
        {
            var newUser = new ApplicationUser
            {
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(error => error.Description));
            }

            return Ok(result);
        }

        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDetails userDetails)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.FirstName = userDetails.FirstName;
            user.LastName = userDetails.LastName;
            user.Email = userDetails.Email;
            user.PhoneNumber = userDetails.PhoneNumber;
            user.Address = userDetails.Address;
            user.DateOfBirth = userDetails.DateOfBirth;
            user.ProfilePictureUrl = userDetails.ProfilePictureUrl;
            user.Role = userDetails.Role;
            user.ListExpensesId = userDetails.ListExpensesId;
            
            await _usersRepository.UpdateAsync(user);
            return Ok(user);
        }

        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            if(user is null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            await _usersRepository.DeleteAsync(id);
            return Ok(user);

        }

        /*[HttpPost("upload")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            if(!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var publicUrl = $"/uploads/{fileName}"; // Adjust this based on your server setup   
            return Ok(publicUrl);
        }*/
    }
}
