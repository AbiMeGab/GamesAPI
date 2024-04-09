namespace Week4Lab.Models
{
    public class VideoGamePersistence : IVideoGameRepository
    {
        //Initializes a List to store all the Games
        private static List<VideoGame> _videoGames = new();

        //Retrieves all video games.
        public IEnumerable<VideoGame> GetAll()
        {
            return _videoGames;
        }

        //Retrieves a specific video game by its ID.
        public VideoGame GetById(int id)
        {
            return _videoGames.FirstOrDefault(vGame => vGame.Id == id);
        }

        //Adds a new video game.
        public void Add(VideoGame videoGame)
        {
            _videoGames.Add(videoGame);
        }

        //Updates an existing video game.
        public void Update(VideoGame videoGame)
        {
            var existingGame = _videoGames.FirstOrDefault(vGame => vGame.Id == videoGame.Id);
            if (existingGame != null)
            {
                existingGame.Id = videoGame.Id;
                existingGame.Name = videoGame.Name;
                existingGame.Platform = videoGame.Platform;
                existingGame.Price = videoGame.Price;
            }
        }

        //Deletes a video game by its ID.
        public void Delete(int id)
        {
            var gameToDelete = _videoGames.FirstOrDefault(vGame => vGame.Id == id);
            if (gameToDelete != null)
            {
                _videoGames.Remove(gameToDelete);
            }
        }
    }
}
