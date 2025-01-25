using App_web_book.Data;
using Microsoft.AspNetCore.Mvc;
using App_web_book.Entities.VSDCEntities;
using App_web_book.Data.VSDC;
using App_web_book.Entities;

namespace App_web_book.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly AtefContext _context;

        public UserController(AtefContext context)
        {
            _context = context;
        }

        // Retrieve all users
        [HttpGet("all")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _context.Users.ToList();
            if (!users.Any())
            {
                return NoContent();
            }
            return Ok(users);
        }

        // Retrieve a specific user by ID
        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {userId} not found." });
            }
            return Ok(user);
        }

        // Create a new user
        [HttpPost("add")]
        public IActionResult AddUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest(new { message = "User data cannot be null." });
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { userId = newUser.Id }, newUser);
        }

        // Update an existing user
        [HttpPut("edit/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] User userUpdates)
        {
            var existingUser = _context.Users.Find(userId);
            if (existingUser == null)
            {
                return NotFound(new { message = $"User with ID {userId} not found." });
            }

            existingUser.Name = userUpdates.Name;
            existingUser.Email = userUpdates.Email;
            existingUser.Password = userUpdates.Password;

            _context.SaveChanges();

            return Ok(new { message = "User details updated successfully." });
        }

        // Delete a user
        [HttpDelete("remove/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var userToDelete = _context.Users.Find(userId);
            if (userToDelete == null)
            {
                return NotFound(new { message = $"User with ID {userId} not found." });
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();

            return Ok(new { message = "User deleted successfully." });
        }
    }
}