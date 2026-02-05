namespace QuizGenix_BE.DTOs
{
    public class CreateQuestionDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public int CorrectAnswer { get; set; }
        public Boolean IsAIGenerated { get; set; }
        public Guid ExamID { get; set; }
    }
}
