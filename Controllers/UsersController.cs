using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Week4Lab.Models;
using Week4Lab.NewFolder;
using Week4Lab.Repositories;
using Week4Lab.Utilities;

namespace Week4Lab.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidator userValidator;

        public UsersController(IUserRepository userRepository, UserValidator userValidator)
        {
            _userRepository = userRepository;
            this.userValidator = userValidator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("NewAdmin")]
        public IActionResult CreateAdminUser([FromBody] User user)
        {
            if (!userValidator.IsValidUser(user))
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


        [HttpPost("NewUser")]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (!userValidator.IsValidUser(user))
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
