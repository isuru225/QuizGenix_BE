using QuizGenix_BE.Models;

namespace QuizGenix_BE.Models
{
    public class Lesson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public Guid TeacherId { get; set; }
        public User Teacher { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ExamComposing> ExamComposings { get; set; }
    }
}

//public class Lesson
//{
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public string Title { get; set; }
//    public string Content { get; set; }
//    public string? FilePath { get; set; }
//    public Guid TeacherId { get; set; }
//    public User Teacher { get; set; }
//    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//    public ICollection<Exam> Exams { get; set; }
//}