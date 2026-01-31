using QuizGenix_BE.DTOs;

namespace QuizGenix_BE.IServices
{
    public interface IAnswerService
    {
        public Task<AnswerResponseDto> CreateAnswers(AnswerRequestDto answerRequestDto);
    }
}
