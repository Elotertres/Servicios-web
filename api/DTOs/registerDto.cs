using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

    public class RegisterRequest
    {
        [Required]
            public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;
    }
    public class LoginRequest
    {
    public required string Username { get; set; }
    public required string Password { get; set;}
    
    }
