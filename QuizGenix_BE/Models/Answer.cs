using QuizGenix_BE.Models;

namespace QuizGenix_BE.Models
{
    public class Answer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SelectedAnswer { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}

//public class StudentAnswer
//{
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public Guid StudentExamId { get; set; }
//    public StudentExam StudentExam { get; set; }
//    public Guid QuestionId { get; set; }
//    public Question Question { get; set; }
//    public string SelectedAnswer { get; set; }
//    public bool IsCorrect { get; set; }
//    public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
//}