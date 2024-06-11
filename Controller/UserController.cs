using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Data;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Controller
{
    public class UserController : ControllerBase
    {
        private readonly ConversationContextDb _context;
        public UserController(ConversationContextDb context ) { 
        
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validate and respond
            if (request.UserName == "admin" && request.Password == "admin_password")
            {
                var User = _context.Users.FirstOrDefault();
                return Ok(User);
            }
            return Unauthorized();
        }

        public class LoginRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }


        [HttpPost("User/login")]
        public async Task<ActionResult<User>> Login(User loginModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);

            if (user == null)
            {
                return NotFound("Invalid username or password");
            }

            return Ok(user);
        }

    }
}
