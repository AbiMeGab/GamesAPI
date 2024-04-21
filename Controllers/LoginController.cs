using Microsoft.AspNetCore.Mvc;
using Week4Lab.Models;
using Week4Lab.Repositories;

namespace Week4Lab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IUserRepository userRepository;
        private AuthController authController;

        public LoginController(IUserRepository userRepository, AuthController authController)
        {
            this.userRepository = userRepository;
            this.authController = authController;
        }

        // Handles user login and generates JWT token
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Authenticate user
            User userLogin = userRepository.GetByUsernameAndPassword(loginRequest.Username, loginRequest.Password);

            // Return Unauthorized if authentication fails
            if (userLogin == null)
            {
                return Unauthorized("User not found, check your credentials.");
            }

            // Generate JWT token upon successful authentication
            var token = authController.GenerateJwtToken(userLogin);
            return Ok(new { token, userLogin.Role });
        }
    }
}
