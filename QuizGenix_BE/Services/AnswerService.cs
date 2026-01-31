using QuizGenix_BE.DataLayer;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;
using QuizGenix_BE.Models;

namespace QuizGenix_BE.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly QuizGenixDBContext quizGenixDBContext;
        public AnswerService(QuizGenixDBContext quizGenixDBContext) 
        {
            this.quizGenixDBContext = quizGenixDBContext;
        }

        public async Task<AnswerResponseDto> CreateAnswers(AnswerRequestDto answerRequestDto) 
        {

            List<Answer> answers = new List<Answer>();
            List<StudentAnswer> studentAnswers = new List<StudentAnswer>();
            foreach (QuestionAndAnswer questionAndAnswer in answerRequestDto.questionAndAnswers) 
            {
                //create new answer
                Answer answer = new Answer();
                answer.SelectedAnswer = questionAndAnswer.SelectedAnswer;
                answers.Add(answer);

                //create new studentAnswer
                studentAnswers.Add(new StudentAnswer
                {
                    AnswerId = answer.Id,
                    UserId = answerRequestDto.StudentId,
                    QuestionId = questionAndAnswer.QuestionId,
                    AnsweredAt = questionAndAnswer.AnsweredAt
                });
            }

            // Save answers to the Answers table
            quizGenixDBContext.Answers.AddRange(answers);
            await quizGenixDBContext.SaveChangesAsync();

            // Save answer info to the Student Answer table in the db
            quizGenixDBContext.StudentAnswers.AddRange(studentAnswers);
            await quizGenixDBContext.SaveChangesAsync();

            return new AnswerResponseDto
            {
                isSuccessfullySaved = true
            };
        }
    }
}
