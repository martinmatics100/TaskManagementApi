using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Implementation;
using TaskManagement.Core.Services;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool userCreated = await _userRepository.CreateUserAsync(userDto);

                if (userCreated)
                {
                    return Ok("User created successfully.");
                }
                else
                {
                    return BadRequest("A user with the same email already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex}");

                return StatusCode(500, "User creation failed. Please try again later.");
            }
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                var userDto = await _userRepository.ReadUserAsync(userId);

                if (userDto == null)
                {
                    return NotFound(); 
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading user: {ex}");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while reading the user.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.ReadAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserDetails(string userId, [FromBody] UserDto updatedUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userRepository.UpdateUserDetailsAsync(userId, updatedUserDto);

                if (result)
                {
                    return Ok("User details updated successfully.");
                }
                else
                {
                    return NotFound($"User with ID {userId} not found. Or User Already Exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user details: {ex}");

                return StatusCode(500, "User details update failed. Please try again later.");
            }
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var result = await _userRepository.DeleteUserByIdAsync(userId);

                if (result)
                {
                    return Ok("User deleted successfully.");
                }
                else
                {
                    return NotFound($"User with ID {userId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex}");

                return StatusCode(500, "User deletion failed. Please try again later.");
            }
        }
    }
}
