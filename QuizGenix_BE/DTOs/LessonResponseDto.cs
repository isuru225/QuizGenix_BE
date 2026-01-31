namespace QuizGenix_BE.DTOs
{
    public class LessonResponseDto
    {   
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? FilePath { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int QuestionCount { get; set; }
    }
}
