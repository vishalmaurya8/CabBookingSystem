using CabBookingSystem.API.Dtos;
using CabBookingSystem.API.Repository;
using CabBookingSystem.API.ScaffoldModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IJWTTokenRepository tokenRepository;

        public UserController(IUserRepository userRepository, IJWTTokenRepository tokenRepository)
        {
            this.userRepository = userRepository;
            this.tokenRepository = tokenRepository;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<LoginDto>), 200)]
        [ProducesResponseType(500)] // Internal Server Error
        public async Task<IActionResult> GetAllUsers()
        {
            // Removed try-catch block as requested.
            // Consider implementing global error handling middleware for production.
            var users = await userRepository.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] AddUserDto addUserDto)
        {
            // Map Dto to Domain Model
            var addUserDomain = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                Phone = addUserDto.Phone,
                PasswordHash = addUserDto.PasswordHash,
                CreatedAt = addUserDto.CreatedAt
            };
            var result = await userRepository.CreateUserAsync(addUserDomain);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var user = await userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.PasswordHash != loginDto.PasswordHash)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = tokenRepository.CreateToken(user);

            return Ok(new
            {
                Token = token,
                User = new
                {
                    user.UserId,
                    user.Name,
                    user.Email,
                    user.Phone
                }
            });
        }





        /*[HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var user = await userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.PasswordHash != loginDto.PasswordHash)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Optionally, generate a token or return user info
            return Ok(new
            {
                user.UserId,
                user.Name,
                user.Email,
                user.Phone,
                user.CreatedAt
            });
        }*/

    }
}
