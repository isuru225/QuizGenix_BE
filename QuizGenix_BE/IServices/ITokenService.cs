using QuizGenix_BE.Models;

namespace QuizGenix_BE.IServices
{
    public interface ITokenService
    {
        public string GenerateJwtToken(User user);
    }
}
