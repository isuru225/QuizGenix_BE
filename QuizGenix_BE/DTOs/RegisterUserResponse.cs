using QuizGenix_BE.Models;

namespace QuizGenix_BE.DTOs
{
    public class RegisterUserResponse
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
