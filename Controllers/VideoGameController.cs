using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Week4Lab.Models;
using Week4Lab.Repositories;
using Week4Lab.Utilities;

namespace Week4Lab.Controllers
{
    /// <summary>
    /// Controller responsible for managing video game-related operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoGameController"/> class.
        /// </summary>
        /// <param name="repository">The repository for video game data.</param>
        public VideoGameController(IVideoGameRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all video games.
        /// </summary>
        /// <returns>The list of video games.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var videoGames = _repository.GetAll();
            var responseData = new List<object>();

            foreach (var game in videoGames)
            {
                var data = VideoGameResponseBuilder.BuildVideoGameResponseData(game);
                responseData.Add(data);
            }

            var response = VideoGameResponseBuilder.BuildVideoGameResponse(responseData);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific video game by ID.
        /// </summary>
        /// <param name="id">The ID of the video game.</param>
        /// <returns>The video game.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var game = _repository.GetById(id);

            if (game != null)
            {
                var data = VideoGameResponseBuilder.BuildVideoGameResponseData(game);
                var response = VideoGameResponseBuilder.BuildVideoGameResponse(data);
                return Ok(response);
            }
            else
            {
                return NotFound("No videogames were found.");
            }
        }

        /// <summary>
        /// Adds a new video game.
        /// </summary>
        /// <param name="videoGame">The video game to add.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Post([FromBody] VideoGame videoGame)
        {
            if (_repository.GetAll().Any(vGame => vGame.Name.Equals(videoGame.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict("A video game with this name already exists.");
            }

            if (_repository.GetAll().Any(vGame => vGame.Id == videoGame.Id))
            {
                return Conflict("A video game with this ID already exists.");
            }

            if (string.IsNullOrWhiteSpace(videoGame.Name) || string.IsNullOrWhiteSpace(videoGame.Platform))
            {
                return BadRequest("The 'Name' and 'Platform' fields are required.");
            }

            if (videoGame.Price < 0)
            {
                return BadRequest("The price cannot be negative.");
            }

            if (!videoGame.Platform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) &&
                !videoGame.Platform.Equals("Play Station", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The platform must be 'Xbox' or 'Play Station'.");
            }

            _repository.Add(videoGame);

            var data = VideoGameResponseBuilder.BuildVideoGameResponseData(videoGame);
            var response = VideoGameResponseBuilder.BuildVideoGameResponse(data);
            return CreatedAtAction(nameof(GetById), new { id = videoGame.Id }, response);
        }

        /// <summary>
        /// Updates an existing video game.
        /// </summary>
        /// <param name="id">The ID of the video game to update.</param>
        /// <param name="videoGame">The updated video game data.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VideoGame videoGame)
        {
            var existingGame = _repository.GetById(id);

            if (existingGame != null)
            {
                existingGame.Name = videoGame.Name;
                existingGame.Platform = videoGame.Platform;
                existingGame.Price = videoGame.Price;

                _repository.Update(existingGame);

                var data = VideoGameResponseBuilder.BuildVideoGameResponseData(existingGame);
                var response = VideoGameResponseBuilder.BuildVideoGameResponse(data);
                return Ok(response);
            }
            else
            {
                return NotFound("No videogames were found.");
            }
        }

        /// <summary>
        /// Deletes a video game by ID.
        /// </summary>
        /// <param name="id">The ID of the video game to delete.</param>
        /// <returns>The result of the operation.</returns>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingGame = _repository.GetById(id);

            if (existingGame != null)
            {
                _repository.Delete(id);

                var data = VideoGameResponseBuilder.BuildVideoGameResponseData(existingGame);
                var response = VideoGameResponseBuilder.BuildVideoGameResponse(data);
                return Ok(response);
            }
            else
            {
                return NotFound("No videogames were found.");
            }
        }
    }
}
