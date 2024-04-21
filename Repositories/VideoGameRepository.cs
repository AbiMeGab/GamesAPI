using Week4Lab.Models;

namespace Week4Lab.Repositories
{
    //Defines a VideoGame CRUD structure for data persistence in a RESTful API.
    public interface IVideoGameRepository
    {
        IEnumerable<VideoGame> GetAll();
        VideoGame GetById(int id);
        void Add(VideoGame videoGame);
        void Update(VideoGame videoGame);
        void Delete(int id);
    }
}
