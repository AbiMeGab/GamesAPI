using Week4Lab.Models;
using Week4Lab.Repositories;

namespace Week4Lab.Utilities
{
    public class UserValidator
    {
        private readonly IUserRepository userRepository;

        public UserValidator(IUserRepository repository)
        {
            userRepository = repository;
        }

        public bool IsValidUser(User user)
        {
            // Check if username is already taken
            if (userRepository.GetByUsername(user.Username) != null)
            {
                return false;
            }

            // Check password requirements
            if (user.Password.Length < 8 || !user.Password.Any(char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}
