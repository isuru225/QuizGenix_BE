using QuizGenix_BE.Models;

namespace QuizGenix_BE.DTOs
{
    public class ExamQuestions
    {
        public Guid ExamId { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
