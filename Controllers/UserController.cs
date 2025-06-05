using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Data; // Make sure this is included
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("User ID mismatch.");
            }
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }
            await _userRepository.UpdateUserAsync(updatedUser);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }
        
    }

}
