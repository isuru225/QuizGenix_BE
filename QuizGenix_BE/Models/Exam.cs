using QuizGenix_BE.Models;

namespace QuizGenix_BE.Models
{
    public class Exam
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int StudentGrade { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
        public ICollection<ExamComposing> ExamComposings { get; set; }
    }
}

//public class Exam
//{
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public string Title { get; set; }
//    public string? Description { get; set; }
//    public DateTime ScheduledStartTime { get; set; }
//    public DateTime ScheduledEndTime { get; set; }
//    public int DurationMinutes { get; set; }
//    public Guid LessonId { get; set; }
//    public Lesson Lesson { get; set; }
//    public Guid TeacherId { get; set; }
//    public User Teacher { get; set; }
//    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//    public ICollection<ExamQuestion> ExamQuestions { get; set; }
//    public int StudentGrade { get; set; }
//    public ICollection<User> Students { get; set; }
//    public ICollection<StudentExam> StudentExams { get; set; }
//}