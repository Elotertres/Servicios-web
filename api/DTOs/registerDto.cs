using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

    public class RegisterRequest
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
    public class LoginRequest
    {
    public required string Username { get; set; }
    public required string Password { get; set;}
    
    }
