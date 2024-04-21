using Week4Lab.Models;

namespace Week4Lab.Utilities
{
    public class VideoGameResponseBuilder
    {
        private static object responseData;

        public static object BuildVideoGameResponseData(VideoGame game)
        {
            // Prepare response data for JSON:API format
            responseData = new
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

            return responseData;
        }

        public static object BuildVideoGameResponse(object responseData)
        {
            // Construct the response object
            var response = new
            {
                data = responseData
            };

            // Return the response
            return response;
        }
    }
}
