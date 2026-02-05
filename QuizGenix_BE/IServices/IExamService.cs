using QuizGenix_BE.DTOs;

namespace QuizGenix_BE.IServices
{
    public interface IExamService
    {
        public Task<ExamResponseDto> CreateExam(CreateExamDto createExamDto, Guid teacherId);
        public Task<ExamResponseDto> GetExamById(Guid ExamId);
        public Task<List<ExamResponseDto>> GetExamByTeacherId(Guid TeacherId);
        public Task<TeacherDashboardResponseDto> GetDashBoradInfoByTeacherId(Guid TeacherId);
        public Task<ExamResponseDto> UpdateExambyId(Guid ExamId, CreateExamDto createExamDto);
        public Task<List<ExamResponseDto>> GetExamByGrade(int studentGrade);
    }
}
