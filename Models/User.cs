using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Week4Lab.Models
{
    public class User
    {
        private static int _nextId = 1;

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public string? Role { get; set; }

        public User()
        {
            Id = _nextId++;
        }
    }
}