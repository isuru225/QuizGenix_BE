using QuizGenix_BE.DTOs;
using QuizGenix_BE.Models;

namespace QuizGenix_BE.IServices
{
    public interface IQuestionService
    {
        //public Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto);
        //public Task<QuestionResponseDto> UpdateQuestion(Guid questionId, UpdateQuestionDto updateQuestionDto);
        public Task<ExamQuestions> GetQuestionsByExamId(Guid Examid);
        public Task<QuestionResponseDto> CreateMultipleQuestions(List<CreateQuestionDto> createQuestionDtos, Guid eaxmId);
        public Task<bool> DeleteQuestion(Guid questionId);
        public Task<bool> DeleteMultipleQuestions(List<Question> deletedQuestion);
    }
}
