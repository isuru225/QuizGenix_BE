using QuizGenix_BE.DTOs;

namespace QuizGenix_BE.IServices
{
    public interface IExamService
    {
        public Task<ExamResponseDto> CreateExam(CreateExamDto createExamDto, Guid teacherId);
        public Task<ExamResponseDto> GetExamById(Guid ExamId);
    }
}
