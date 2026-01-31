namespace QuizGenix_BE.DTOs
{
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
        public Boolean IsAIGenerated { get; set; }
        public Guid ExamID { get; set; }
    }
}
