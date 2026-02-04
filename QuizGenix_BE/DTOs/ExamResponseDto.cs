using QuizGenix_BE.Models;

namespace QuizGenix_BE.DTOs
{
    public class ExamResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int DurationMinutes { get; set; }
        public Guid? LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public Guid? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StudentGrade { get; set; }
        public ICollection<QuestionResult> Questions { get; set; } = new List<QuestionResult>();
    }

    public class QuestionResult 
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CorrectAnswer { get; set; }
        public int? OrderNumber { get; set; }
        public bool IsAIGenerated { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
