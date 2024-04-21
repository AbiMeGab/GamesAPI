using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Week4Lab.Models;
using Week4Lab.NewFolder;
using Week4Lab.Repositories;
using Week4Lab.Utilities;

namespace Week4Lab.Controllers
{
    /// <summary>
    /// Controller responsible for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _userValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userRepository">The repository for user data.</param>
        /// <param name="userValidator">The validator for user data.</param>
        public UsersController(IUserRepository userRepository, UserValidator userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        /// <summary>
        /// Creates a new admin user.
        /// </summary>
        /// <param name="user">The user data for the new admin.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize(Roles = "admin")]
        [HttpPost("NewAdmin")]
        public IActionResult CreateAdminUser([FromBody] User user)
        {
            if (!_userValidator.IsValidUser(user))
            {
                return Conflict("The user already exists or password does not meet requirements (8+ characters and at least one number).");
            }

            user.Role = "admin";

            _userRepository.Add(user);

            var data = UserResponseBuilder.BuildUserResponseData(user);
            var response = UserResponseBuilder.BuildUserResponse(data);

            // Return the response
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user data for the new user.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("NewUser")]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (!_userValidator.IsValidUser(user))
            {
                return Conflict("The user already exists or password does not meet requirements (8+ characters and at least one number).");
            }

            user.Role = "user";

            _userRepository.Add(user);

            var data = UserResponseBuilder.BuildUserResponseData(user);
            var response = UserResponseBuilder.BuildUserResponse(data);

            // Return the response
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>The list of users.</returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAll();

            if (users != null)
            {
                var responseData = new List<object>();
                // Prepare response data for JSON:API format
                foreach (var user in users)
                {
                    var data = UserResponseBuilder.BuildUserResponseData(user);
                    responseData.Add(data);
                }

                var response = UserResponseBuilder.BuildUserResponse(responseData);

                // Return the response
                return Ok(response);
            }
            else
            {
                // Return 404 Not Found if the user is not found
                return NotFound("No users were found.");
            }
        }
    }
}
