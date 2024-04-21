using Week4Lab.Models;

namespace Week4Lab.Repositories
{
    public class UserPersistence : IUserRepository
    {
        private static List<User> _users = new List<User>
        {
             new User { Id = 1, Username = "admin", Password = "pa$$w0rd", Role = "admin" },
             new User { Id = 2, Username = "usuario1", Password = "contraseña", Role = "user" },
             new User { Id = 3, Username = "usuario2", Password = "123456!", Role = "user" }
        };

        public void Add(User user)
        {
            // Find available spaces in the IDs sequence.
            var availableId = Enumerable.Range(1, _users.Count + 1).Except(_users.Select(usser => usser.Id)).FirstOrDefault();
            // Assign the available ID to the new user.
            user.Id = availableId != 0 ? availableId : _users.Count + 1;

            _users.Add(user);
        }

        public User GetByUsername(string username)
        {
            return _users.FirstOrDefault(usser => usser.Username == username);
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return _users.FirstOrDefault(usser => usser.Username == username && usser.Password == password);
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(usser => usser.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

    }
}
