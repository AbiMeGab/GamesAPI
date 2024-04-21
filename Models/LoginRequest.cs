using System.ComponentModel.DataAnnotations;

namespace Week4Lab.Models
{
    public class LoginRequest
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}