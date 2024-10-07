using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

    public class RegisterRequest
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
