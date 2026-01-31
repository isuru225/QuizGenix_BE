using Microsoft.EntityFrameworkCore;
using QuizGenix_BE.DataLayer;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;
using QuizGenix_BE.Models;

namespace QuizGenix_BE.Services
{
    public class ExamService : IExamService
    {
        private readonly QuizGenixDBContext quizGenixDBContext;
        private ILessonService lessonService;

        public ExamService(QuizGenixDBContext quizGenixDBContext, ILessonService lessonService)
        {
            this.quizGenixDBContext = quizGenixDBContext;
            this.lessonService = lessonService;
        }
        public async Task<ExamResponseDto> CreateExam(CreateExamDto createExamDto, Guid teacherId)
        {
            var exam = new Exam
            {
                Title = createExamDto.Title,
                Description = createExamDto.Description,
                ScheduledStartTime = createExamDto.ScheduledStartTime,
                ScheduledEndTime = createExamDto.ScheduledEndTime,
                DurationMinutes = createExamDto.DurationMinutes,
                StudentGrade = createExamDto.StudentGrade
            };

            quizGenixDBContext.Exams.Add(exam);
            await quizGenixDBContext.SaveChangesAsync();

            var examComposing = new ExamComposing
            {
                LessonId = createExamDto.LessonId,
                TeacherId = createExamDto.TeacherId,
                ExamId = exam.Id
            };

            quizGenixDBContext.ExamComposings.Add(examComposing);
            await quizGenixDBContext.SaveChangesAsync();

            //Get lesson info with teacher info
            var lessonInfo = await lessonService.GetLessonById(createExamDto.LessonId);
            return new ExamResponseDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Description = exam.Description,
                ScheduledStartTime = exam.ScheduledStartTime,
                ScheduledEndTime = exam.ScheduledEndTime,
                DurationMinutes = exam.DurationMinutes,
                LessonTitle = lessonInfo.Title,
                TeacherName = lessonInfo.TeacherName,
                CreatedAt = examComposing.CreatedAt,
            };
        }

        public async Task<ExamResponseDto> GetExamById(Guid ExamId) 
        {
            var result = await quizGenixDBContext.Exams.FindAsync(ExamId);

            if (result == null) 
            {
                throw new Exception("Exam not found");
            }

            return new ExamResponseDto 
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                ScheduledEndTime = result.ScheduledEndTime,
                ScheduledStartTime = result.ScheduledStartTime,
                DurationMinutes = result.DurationMinutes,
                CreatedAt = result.CreatedAt,
            };
        }

    }
}

