using Week4Lab.Models;

namespace Week4Lab.NewFolder
{
    public class UserResponseBuilder
    {
        private static object responseData;

        public static object BuildUserResponseData(User user) {

            // Prepare response data for JSON:API format
            responseData = new
            {
                type = "users",
                id = user.Id.ToString(),
                attributes = new
                {
                    username = user.Username,
                    role = user.Role
                }
            };

            return responseData;
        }

        public static object BuildUserResponse (object responseData)
        {
            // Construct the response object
            var response = new
            {
                data = responseData
            };

            return response;
        }
    }
}
