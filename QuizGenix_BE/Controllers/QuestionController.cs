using Microsoft.AspNetCore.Mvc;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Controllers
{
    [ApiController]
    [Route("/api/question")]
    public class QuestionController : Controller
    {
        private IQuestionService questionService;
        public QuestionController(IQuestionService questionService) 
        {
            this.questionService = questionService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateQuestions([FromBody] CreateQuestionDto createQuestionDto) 
        {
            try 
            {
                var result = await questionService.CreateQuestion(createQuestionDto);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{examid}/get")]
        public async Task<IActionResult> GetQuestionsByExamId([FromRoute] Guid examid)
        {
            try
            {
                var result = await questionService.GetQuestionsByExamId(examid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{examid}/update")]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionDto updateQuestionDto, [FromRoute] Guid examid)
        {
            try
            {
                var result = await questionService.UpdateQuestion(examid, updateQuestionDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{examid}/delete")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid questionId)
        {
            try
            {
                var result = await questionService.DeleteQuestion(questionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
