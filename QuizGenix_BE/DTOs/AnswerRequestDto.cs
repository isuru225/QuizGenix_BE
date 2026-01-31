namespace QuizGenix_BE.DTOs
{
    public class AnswerRequestDto
    {
        public Guid StudentId { get; set; }
        public List<QuestionAndAnswer> questionAndAnswers { get; set; }
    }

    public class QuestionAndAnswer 
    {
        public Guid QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
