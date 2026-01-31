namespace QuizGenix_BE.Models
{
    public class StudentAnswer
    {
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid UserId { get; set; }
        public User Student { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
}
