using Microsoft.AspNetCore.Mvc;
using Week4Lab.Models;

namespace Week4Lab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoGameController : ControllerBase
    {

        // Repository for video game data
        private IVideoGameRepository _repository;

        // Constructor to initialize the controller with repository
        public VideoGameController(IVideoGameRepository repository)
        {
            _repository = repository;
        }

        // Get all video games
        [HttpGet]
        public IActionResult Get()
        {
            // Retrieve all video games from the repository
            var videoGames = _repository.GetAll();

            // Prepare response data for JSON:API format
            var responseData = new List<object>();

            foreach (var game in videoGames)
            {
                // Convert each video game to JSON:API format
                responseData.Add(new
                {
                    type = "videoGames",
                    id = game.Id.ToString(),
                    attributes = new
                    {
                        name = game.Name,
                        platform = game.Platform,
                        price = game.Price
                    }
                });
            }

            // Construct the response object
            var response = new
            {
                data = responseData
            };

            // Return the response
            return Ok(response);
        }

        // Get a specific video game by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Retrieve the video game by ID from the repository
            var game = _repository.GetById(id);

            if (game != null)
            {
                // Prepare response data for JSON:API format
                var responseData = new
                {
                    type = "videoGames",
                    id = game.Id.ToString(),
                    attributes = new
                    {
                        name = game.Name,
                        platform = game.Platform,
                        price = game.Price
                    }
                };

                // Construct the response object
                var response = new
                {
                    data = responseData
                };

                // Return the response
                return Ok(response);
            }
            else
            {
                // Return 404 Not Found if the video game is not found
                return NotFound();
            }
        }

        // Add a new video game
        [HttpPost]
        public IActionResult Post([FromBody] VideoGame videoGame)
        {
            // Validation to check for duplicate name or ID
            if (_repository.GetAll().Any(vGame => vGame.Name.Equals(videoGame.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict("A video game with this name already exists.");
            }

            if (_repository.GetAll().Any(vGame => vGame.Id == videoGame.Id))
            {
                return Conflict("A video game with this ID already exists.");
            }

            // Validate required fields and price
            if (string.IsNullOrWhiteSpace(videoGame.Name) || string.IsNullOrWhiteSpace(videoGame.Platform))
            {
                return BadRequest("The 'Name' and 'Platform' fields are required.");
            }

            if (videoGame.Price < 0)
            {
                return BadRequest("The price cannot be negative.");
            }

            // Validate platform restriction
            if (!videoGame.Platform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) &&
                !videoGame.Platform.Equals("Play Station", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The platform must be 'Xbox' or 'Play Station'.");
            }

            // Add the video game to the repository
            _repository.Add(videoGame);

            // Prepare response data for JSON:API format
            var responseData = new
            {
                type = "videoGames",
                id = videoGame.Id.ToString(),
                attributes = new
                {
                    name = videoGame.Name,
                    platform = videoGame.Platform,
                    price = videoGame.Price
                }
            };

            // Construct the response object
            var response = new
            {
                data = responseData
            };

            // Return the response
            return Ok(response);
        }

        // Update an existing video game
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VideoGame videoGame)
        {
            // Retrieve the existing video game by ID from the repository
            var existingGame = _repository.GetById(id);

            if (existingGame != null)
            {
                // Update the properties of the existing video game
                existingGame.Name = videoGame.Name;
                existingGame.Platform = videoGame.Platform;
                existingGame.Price = videoGame.Price;

                // Update the video game in the repository
                _repository.Update(existingGame);

                // Prepare response data for JSON:API format
                var responseData = new
                {
                    type = "videoGames",
                    id = existingGame.Id.ToString(),
                    attributes = new
                    {
                        name = existingGame.Name,
                        platform = existingGame.Platform,
                        price = existingGame.Price
                    }
                };

                // Construct the response object
                var response = new
                {
                    data = responseData
                };

                // Return the response
                return Ok(response);
            }
            else
            {
                // Return 404 Not Found if the video game is not found
                return NotFound();
            }
        }

        // Delete a video game by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Retrieve the existing video game by ID from the repository
            var existingGame = _repository.GetById(id);

            if (existingGame != null)
            {
                // Delete the video game from the repository
                _repository.Delete(id);

                // Prepare response data for JSON:API format
                var responseData = new
                {
                    type = "videoGames",
                    id = existingGame.Id.ToString(),
                    attributes = new
                    {
                        name = existingGame.Name,
                        platform = existingGame.Platform,
                        price = existingGame.Price
                    }
                };

                // Construct the response object
                var response = new
                {
                    data = responseData
                };

                // Return the response
                return Ok(response);
            }
            else
            {
                // Return 404 Not Found if the video game is not found
                return NotFound();
            }
        }
    }
}
