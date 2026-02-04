using QuizGenix_BE.DataLayer;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.Models;
using Microsoft.EntityFrameworkCore;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Services
{
    public class LessonService : ILessonService
    {
        private readonly QuizGenixDBContext quizGenixDBContext;

        public LessonService(QuizGenixDBContext quizGenixDBContext)
        {
            this.quizGenixDBContext = quizGenixDBContext;
        }

        public async Task<LessonResponseDto> CreateLesson(Guid teacherId,CreateLessonDto createLessonDto)
        {
            Lesson lesson = new Lesson();
            lesson.Title = createLessonDto.Title;
            lesson.Content = createLessonDto.Content;
            lesson.Subject = createLessonDto.Subject;
            lesson.TeacherId = teacherId;
            lesson.Status = "Published";

            quizGenixDBContext.Lessons.Add(lesson);
            await quizGenixDBContext.SaveChangesAsync();

            return await GetLessonById(lesson.Id);
        }

        public async Task<List<LessonResponseDto>> GetLessonsByTeacher(Guid teacherId)
        {
            return await quizGenixDBContext.Lessons
                .Where(l => l.TeacherId == teacherId)
                .Include(l => l.Teacher)
                .Select(l => new LessonResponseDto
                {
                    Id = l.Id,
                    Title = l.Title,
                    Content = l.Content,
                    Subject = l.Subject,
                    Status = l.Status,
                    TeacherId = l.TeacherId,
                    TeacherName = l.Teacher.Username,
                    CreatedAt = l.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<LessonResponseDto> GetLessonById(Guid lessonId)
        {
            var lesson = await quizGenixDBContext.Lessons
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(l => l.Id == lessonId);

            if (lesson == null)
            {
                throw new Exception("Lesson not found");
            }

            return new LessonResponseDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Content = lesson.Content,
                TeacherId = lesson.TeacherId,
                TeacherName = lesson.Teacher.Username,
                CreatedAt = lesson.CreatedAt,
                Subject = lesson.Subject
            };
        }

        public async Task<LessonResponseDto> UpdateLesson(Guid lessonId, CreateLessonDto createLessonDto)
        {
            var lesson = await quizGenixDBContext.Lessons.Include(e => e.Teacher).FirstOrDefaultAsync(e => e.Id == lessonId);

            if (lesson == null) 
            { 
                throw new Exception("Exam not found");
            }

            lesson.Title = createLessonDto.Title;
            lesson.Subject = createLessonDto.Subject;
            lesson.Content = createLessonDto.Content;

            await quizGenixDBContext.SaveChangesAsync();

            return new LessonResponseDto
            {
                Id = lesson.Id,
                Title = createLessonDto.Title,
                Content = createLessonDto.Content,
                Subject = createLessonDto.Subject,
                Status = lesson.Status,
                TeacherId = lesson.TeacherId,
                TeacherName = lesson.Teacher.Username,
                CreatedAt = lesson.CreatedAt,

            };
        }


        public async Task<bool> DeleteLesson(Guid lessonId, Guid teacherId)
        {
            var lesson = await quizGenixDBContext.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId && l.TeacherId == teacherId);

            if (lesson == null)
            {
                return false;
            }

            quizGenixDBContext.Lessons.Remove(lesson);
            await quizGenixDBContext.SaveChangesAsync();
            return true;
        }
    }
}

