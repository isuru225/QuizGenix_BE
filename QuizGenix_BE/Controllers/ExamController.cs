using Microsoft.AspNetCore.Mvc;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Controllers
{
    [ApiController]
    [Route("/api/exam")]
    public class ExamController : Controller
    {
        private IExamService examService;
        private IQuestionService questionService;
        public ExamController(IExamService examService, IQuestionService questionService) 
        {
            this.examService = examService;
            this.questionService = questionService;
        }
        [HttpPost("{teacherId}/add")]
        public async Task<IActionResult> CreateExamSheet(Guid teacherId, [FromBody] CreateExamDto createExamDto) 
        {
            try 
            {
                var result = await examService.CreateExam(createExamDto, teacherId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{examId}/get")]
        public async Task<IActionResult> GetExamById(Guid examId)
        {
            try
            {
                var result = await examService.GetExamById(examId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{examId}/getquestions")]
        public async Task<IActionResult> GetQuestionsByExamId(Guid examId)
        {
            try
            {
                var result = await questionService.GetQuestionsByExamId(examId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
