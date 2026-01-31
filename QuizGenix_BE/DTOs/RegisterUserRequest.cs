using QuizGenix_BE.Models;
using System.ComponentModel.DataAnnotations;

namespace QuizGenix_BE.DTOs
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "User name is required!")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [RegularExpression(
        @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!#%$]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase, one uppercase, one digit, and one special character (!, #, %, $)."
        )]
        public string Password { get; set; }
        public int Grade { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }
}
