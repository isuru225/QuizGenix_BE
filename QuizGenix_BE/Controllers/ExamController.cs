using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Controllers
{

    [ApiController]
    [Route("/api/exam")]
    [Authorize]
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
        [Authorize(Roles = "Teacher")]
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
        [HttpGet("{examId}/getbyexamid")]
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

        [HttpGet("{teacherId}/getbyteacherid")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetExamByTeacherId([FromRoute]Guid teacherId)
        {
            try
            {
                var result = await examService.GetExamByTeacherId(teacherId);
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

        [HttpGet("{teacherId}/dashboard")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetDashboardInfo(Guid teacherId)
        {
            try
            {
                var result = await examService.GetDashBoradInfoByTeacherId(teacherId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{examId}/update")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateExamById(Guid examId, [FromBody] CreateExamDto createExamDto) 
        {
            try 
            {
                var result = await examService.UpdateExambyId(examId,createExamDto);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getexambygrade")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetExamByGrade([FromQuery] int grade)
        {
            try
            {
                var result = await examService.GetExamByGrade(grade);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
