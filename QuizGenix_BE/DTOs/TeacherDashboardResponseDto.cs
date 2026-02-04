using QuizGenix_BE.Models;

namespace QuizGenix_BE.DTOs
{
    public class TeacherDashboardResponseDto
    {
        public List<UserInfoDto> userInfoDtos { get; set; } = new List<UserInfoDto>();
        public List<ExamLessonPairs> ExamLessonPairs { get; set; } = new List<ExamLessonPairs>();
    }

    public class ExamLessonPairs 
    {
        public LessonResponseDto Lesson { get; set; }
        public ExamResponseDto Exam { get; set; }
    }
}
