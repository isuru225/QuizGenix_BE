namespace QuizGenix_BE.DTOs
{
    public class CreateExamDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public int DurationMinutes { get; set; }
        public Guid LessonId { get; set; }
        public Guid TeacherId { get; set; }
        public int StudentGrade { get; set; }
    }
}
