using QuizGenix_BE.DTOs;

namespace QuizGenix_BE.IServices
{
    public interface IQuestionService
    {
        //public Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto);
        //public Task<QuestionResponseDto> UpdateQuestion(Guid questionId, UpdateQuestionDto updateQuestionDto);
        public Task<ExamQuestions> GetQuestionsByExamId(Guid Examid);
        public Task<QuestionResponseDto> CreateMultipleQuestions(List<CreateQuestionDto> createQuestionDtos, Guid eaxmId);
        public Task<bool> DeleteQuestion(Guid questionId);
    }
}
