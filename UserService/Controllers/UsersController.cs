using Microsoft.AspNetCore.Mvc;
using UserService.Enums;
using UserService.Interfaces;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation("Fetching user with ID {Id}", id);
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _logger.LogInformation("Creating user with ID {Id}", user.Id);
            try
            {
                await _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with ID {Id}", user.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id) return BadRequest();
            _logger.LogInformation("Updating user with ID {Id}", id);
            try
            {
                var updated = await _userService.UpdateUser(id, user);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user with ID {Id}", id);
            try
            {
                var deleted = await _userService.DeleteUser(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("subscriptionType/{subscriptionType}")]
        public async Task<IActionResult> GetUsersBySubscriptionType(string subscriptionType)
        {
            if (!Enum.TryParse(subscriptionType, true, out SubscriptionType parsedSubscriptionType))
            {
                return BadRequest("Invalid subscription type");
            }

            var userIds = await _userService.GetUserIdsBySubscriptionTypeAsync(parsedSubscriptionType.ToString());
            if (userIds == null || !userIds.Any())
            {
                return NotFound();
            }

            return Ok(userIds);
        }

    }
}


