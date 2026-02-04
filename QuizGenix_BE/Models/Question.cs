using QuizGenix_BE.Models;

namespace QuizGenix_BE.Models
{
    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CorrectAnswer { get; set; }
        public int? OrderNumber { get; set; }
        public bool IsAIGenerated { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}

//public class Question
//{
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public Guid ExamId { get; set; }
//    public Exam Exam { get; set; }
//    public string QuestionText { get; set; }
//    public string OptionA { get; set; }
//    public string OptionB { get; set; }
//    public string OptionC { get; set; }
//    public string OptionD { get; set; }
//    public string CorrectAnswer { get; set; }
//    public int OrderNumber { get; set; }
//    public bool IsAIGenerated { get; set; }
//    public Guid LessonId { get; set; }
//    public Lesson Lesson { get; set; }
//    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//}