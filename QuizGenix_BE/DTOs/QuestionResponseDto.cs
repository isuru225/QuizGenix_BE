namespace QuizGenix_BE.DTOs
{
    public class QuestionResponseDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsAIGenerated { get; set; }
        public Guid ExamID { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
