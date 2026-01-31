using QuizGenix_BE.Models;

namespace QuizGenix_BE.DTOs
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Grade { get; set; } = 0;
        public UserRole Role { get; set; }
        public DateTime AdmissionDate { get; set; }
    }
}
