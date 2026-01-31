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
        public string? LessonTitle { get; set; }
        public string? TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
