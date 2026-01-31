using QuizGenix_BE.Models;

namespace QuizGenix_BE.Models
{
    public class StudentExam
    {
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }
        public Guid StudentId { get; set; }
        public User Student { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public ExamStatus Status { get; set; } = ExamStatus.NotStarted;
        public int? Score { get; set; }
        public int? TotalQuestions { get; set; }
    }

    public enum ExamStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Expired
    }
}

//public class StudentExam
//{
//    public Guid ExamId { get; set; }
//    public Exam Exam { get; set; }
//    public Guid StudentId { get; set; }
//    public User Student { get; set; }
//    public DateTime? StartedAt { get; set; }
//    public DateTime? CompletedAt { get; set; }
//    public ExamStatus Status { get; set; } = ExamStatus.NotStarted;
//    public int? Score { get; set; }
//    public int? TotalQuestions { get; set; }
//    public ICollection<StudentAnswer> StudentAnswers { get; set; }
//}
