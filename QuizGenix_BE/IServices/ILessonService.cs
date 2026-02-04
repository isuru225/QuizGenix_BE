using QuizGenix_BE.DTOs;

namespace QuizGenix_BE.IServices
{
    public interface ILessonService
    {
        public Task<LessonResponseDto> CreateLesson(Guid teacherId,CreateLessonDto createLessonDto);
        public Task<List<LessonResponseDto>> GetLessonsByTeacher(Guid teacherId);
        public Task<LessonResponseDto> GetLessonById(Guid lessonId);
        public Task<bool> DeleteLesson(Guid lessonId, Guid teacherId);
        public Task<LessonResponseDto> UpdateLesson(Guid lessonId, CreateLessonDto createLessonDto);
    }
}
