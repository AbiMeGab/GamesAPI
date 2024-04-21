using Microsoft.AspNetCore.Mvc;
using Week4Lab.Models;
using Week4Lab.Repositories;

namespace Week4Lab.Controllers
{
    /// <summary>
    /// Controller responsible for handling user login and generating JWT token.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IUserRepository userRepository;
        private AuthController authController;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="authController">The authentication controller.</param>
        public LoginController(IUserRepository userRepository, AuthController authController)
        {
            this.userRepository = userRepository;
            this.authController = authController;
        }

        /// <summary>
        /// Handles user login and generates JWT token.
        /// </summary>
        /// <param name="loginRequest">The login request containing user credentials.</param>
        /// <returns>An IActionResult representing the result of the login operation.</returns>
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