using Microsoft.EntityFrameworkCore;
using QuizGenix_BE.DataLayer;
using QuizGenix_BE.Models;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Services
{
    public class QuestionService : IQuestionService
    {
       
        private readonly QuizGenixDBContext quizGenixDBContext;

        public QuestionService(QuizGenixDBContext quizGenixDBContext)
        {
            this.quizGenixDBContext = quizGenixDBContext;
        }

        public async Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            var question = new Question
            {
                QuestionText = createQuestionDto.QuestionText,
                OptionA = createQuestionDto.OptionA,
                OptionB = createQuestionDto.OptionB,
                OptionC = createQuestionDto.OptionC,
                OptionD = createQuestionDto.OptionD,
                CorrectAnswer = createQuestionDto.CorrectAnswer,
                ExamId = createQuestionDto.ExamID,
                IsAIGenerated = createQuestionDto.IsAIGenerated
            };

            quizGenixDBContext.Questions.Add(question);
            await quizGenixDBContext.SaveChangesAsync();

            return MapToDto(question);
        }

        public async Task<QuestionResponseDto> UpdateQuestion(Guid questionId, UpdateQuestionDto updateQuestionDto)
        {
            var question = await quizGenixDBContext.Questions.FindAsync(questionId);

            if (question == null)
            {
                throw new Exception("Question not found");
            }

            question.QuestionText = updateQuestionDto.QuestionText;
            question.OptionA = updateQuestionDto.OptionA;
            question.OptionB = updateQuestionDto.OptionB;
            question.OptionC = updateQuestionDto.OptionC;
            question.OptionD = updateQuestionDto.OptionD;
            question.CorrectAnswer = updateQuestionDto.CorrectAnswer;
            question.IsAIGenerated = updateQuestionDto.IsAIGenerated;

            await quizGenixDBContext.SaveChangesAsync();

            return MapToDto(question);
        }

        public async Task<ExamQuestions> GetQuestionsByExamId(Guid Examid) 
        {
            var result = quizGenixDBContext.Exams.Include(exam => exam.Questions).FirstOrDefault(exam => exam.Id == Examid);

            if (result == null) 
            {
                throw new Exception("Exam is not found");
            }

            return new ExamQuestions
            { 
                ExamId = Examid,
                Questions = result.Questions
            };
        
        }

        //public async Task<List<QuestionResponseDto>> GetQuestionsByLesson(Guid lessonId)
        //{
        //    return await quizGenixDBContext.Questions
        //        .Where(q => q.LessonId == lessonId)
        //        .Select(q => MapToDto(q))
        //        .ToListAsync();
        //}

        public async Task<bool> DeleteQuestion(Guid questionId)
        {
            var question = await quizGenixDBContext.Questions.FindAsync(questionId);

            if (question == null)
            {
                return false;
            }

            quizGenixDBContext.Questions.Remove(question);
            await quizGenixDBContext.SaveChangesAsync();
            return true;
        }
        private QuestionResponseDto MapToDto(Question question)
        {
            return new QuestionResponseDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                OptionA = question.OptionA,
                OptionB = question.OptionB,
                OptionC = question.OptionC,
                OptionD = question.OptionD,
                CorrectAnswer = question.CorrectAnswer,
                IsAIGenerated = question.IsAIGenerated,
                ExamID = question.ExamId,
                CreatedAt = question.CreatedAt
            };
        }


    }
}
