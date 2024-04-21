using Week4Lab.Models;

namespace Week4Lab.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByUsername(string username);
        User GetByUsernameAndPassword(string username, string password);
        User GetById(int id);
        IEnumerable<User> GetAll();
    }
}
