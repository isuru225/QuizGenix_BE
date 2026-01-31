namespace QuizGenix_BE.Models
{
    public class ExamComposing
    {
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }
        public Guid LessonId { get; set;}
        public Lesson Lesson { get; set;}
        public Guid TeacherId { get; set; }
        public User Teacher { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
