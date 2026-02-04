using Microsoft.AspNetCore.Http.HttpResults;
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
        private IQuestionService questionService;
        private IUserManagementService userManagementService;

        public ExamService(QuizGenixDBContext quizGenixDBContext, ILessonService lessonService, IQuestionService questionService, IUserManagementService userManagementService)
        {
            this.quizGenixDBContext = quizGenixDBContext;
            this.lessonService = lessonService;
            this.questionService = questionService;
            this.userManagementService = userManagementService;
        }
        public async Task<ExamResponseDto> CreateExam(CreateExamDto createExamDto, Guid teacherId)
        {
            var exam = new Exam
            {
                Title = createExamDto.Title,
                Description = createExamDto.Description,
                ScheduledStartTime = DateTime.SpecifyKind(createExamDto.ScheduledStartTime, DateTimeKind.Utc),
                ScheduledEndTime = DateTime.SpecifyKind(createExamDto.ScheduledEndTime, DateTimeKind.Utc),
                DurationMinutes = createExamDto.DurationMinutes,
                StudentGrade = createExamDto.StudentGrade
            };

            quizGenixDBContext.Exams.Add(exam);
            await quizGenixDBContext.SaveChangesAsync();

            var examComposing = new ExamComposing
            {
                LessonId = createExamDto.LessonId,
                TeacherId = teacherId,
                ExamId = exam.Id
            };
            //add records to the examcomposing
            quizGenixDBContext.ExamComposings.Add(examComposing);
            await quizGenixDBContext.SaveChangesAsync();

            //add questions to the db
            await questionService.CreateMultipleQuestions(createExamDto.Questions, exam.Id);

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
            var result = await quizGenixDBContext.
                ExamComposings
                .Include(e => e.Lesson)
                .Include(e => e.Exam)
                .ThenInclude(q => q.Questions)
                .FirstOrDefaultAsync(e => e.ExamId == ExamId);

            if (result == null)
            {
                throw new Exception("Can not found any exam by the provided exam id");
            }

            List<QuestionResult> questionList = new List<QuestionResult>();

            foreach (var question in result.Exam.Questions)
            {
                questionList.Add(new QuestionResult
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectAnswer = question.CorrectAnswer,
                    IsAIGenerated = question.IsAIGenerated,
                    CreatedAt = question.CreatedAt
                });
            }

            return new ExamResponseDto
            {
                Id = result.Exam.Id,
                Title = result.Exam.Title,
                Description = result.Exam.Description,
                ScheduledEndTime = result.Exam.ScheduledEndTime,
                ScheduledStartTime = result.Exam.ScheduledStartTime,
                DurationMinutes = result.Exam.DurationMinutes,
                CreatedAt = result.Exam.CreatedAt,
                Questions = questionList,
                StudentGrade = result.Exam.StudentGrade,
                LessonId = result.Lesson.Id,
                LessonTitle = result.Lesson.Title
            };
        }

        public async Task<List<ExamResponseDto>> GetExamByTeacherId(Guid TeacherId)
        {
            var result = await quizGenixDBContext.
                ExamComposings
                .Include(e => e.Teacher)
                .Include(e => e.Lesson)
                .Include(e => e.Exam)
                .ThenInclude(q => q.Questions)
                .Where(e => e.TeacherId == TeacherId).ToListAsync();

            if (result == null)
            {
                throw new Exception("Can not found any exam by Teacher id");
            }
            
            List<ExamResponseDto> examResponseDtos = new List<ExamResponseDto>();
            foreach (var exam in result) 
            {
                ExamResponseDto examResponseDto = new ExamResponseDto();
                examResponseDto.Id = exam.ExamId;
                examResponseDto.Title = exam.Exam.Title;
                examResponseDto.Description = exam.Exam.Description;
                examResponseDto.ScheduledEndTime = exam.Exam.ScheduledEndTime;
                examResponseDto.ScheduledStartTime = exam.Exam.ScheduledStartTime;
                examResponseDto.DurationMinutes = exam.Exam.DurationMinutes;
                examResponseDto.CreatedAt = exam.Exam.CreatedAt;
                examResponseDto.StudentGrade = exam.Exam.StudentGrade;
                examResponseDto.LessonId = exam.LessonId;
                examResponseDto.LessonTitle = exam.Lesson.Title;
                examResponseDto.TeacherId = exam.TeacherId;
                examResponseDto.TeacherName = exam.Teacher.Username;

                foreach (var question in exam.Exam.Questions) 
                {
                    examResponseDto.Questions.Add(new QuestionResult 
                    {
                        Id = question.Id,
                        QuestionText = question.QuestionText,
                        OptionA = question.OptionA,
                        OptionB = question.OptionB,
                        OptionC = question.OptionC,
                        OptionD = question.OptionD,
                        CorrectAnswer = question.CorrectAnswer,
                        IsAIGenerated = question.IsAIGenerated,
                        CreatedAt = question.CreatedAt,
                    });
                }

                examResponseDtos.Add(examResponseDto);
            }

            return examResponseDtos;

        }

        public async Task<TeacherDashboardResponseDto> GetDashBoradInfoByTeacherId(Guid TeacherId)
        {
            var results = await quizGenixDBContext.
                ExamComposings
                .Include(e => e.Lesson)
                .Include(e => e.Exam)
                .Where(e => e.TeacherId == TeacherId).ToListAsync();

            List<ExamLessonPairs> examLessonPairs = new List<ExamLessonPairs> ();

            foreach (var result in results) 
            {
                examLessonPairs.Add(new ExamLessonPairs
                {
                    Lesson = new LessonResponseDto
                    {
                        Id = result.Lesson.Id,
                        Title = result.Lesson.Title,
                        CreatedAt = result.Lesson.CreatedAt
                    }
                    ,
                    Exam = new ExamResponseDto 
                    {
                        Id = result.Exam.Id,
                        Title = result.Exam.Title,
                        ScheduledEndTime = result.Exam.ScheduledEndTime,
                        ScheduledStartTime = result.Exam.ScheduledStartTime,
                        CreatedAt = result.Exam.CreatedAt
                    }
                }) ;
            }

            var studentResult = await userManagementService.GetAllStudents(TeacherId);

            return new TeacherDashboardResponseDto 
            {
                ExamLessonPairs = examLessonPairs,
                userInfoDtos = studentResult
            };

        }

        public async Task<ExamResponseDto> UpdateExambyId(Guid ExamId, CreateExamDto createExamDto)
        {
            var exam = await quizGenixDBContext.Exams.FirstOrDefaultAsync(e => e.Id == ExamId);

            if (exam == null) 
            {
                throw new Exception("Exam not found based on the given ID");
            }

            exam.Title = createExamDto.Title;
            exam.Description = createExamDto.Description;
            exam.ScheduledEndTime = createExamDto.ScheduledEndTime;
            exam.ScheduledStartTime = createExamDto.ScheduledStartTime;
            exam.StudentGrade = createExamDto.StudentGrade;
            exam.DurationMinutes = createExamDto.DurationMinutes;
            
            await quizGenixDBContext.SaveChangesAsync();
            
            //add questions to the db
            await questionService.CreateMultipleQuestions(createExamDto.Questions, exam.Id);

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
                CreatedAt = exam.CreatedAt,
                LessonTitle = lessonInfo.Title,
                TeacherName = lessonInfo.TeacherName
            };

        }

    }

  

}

