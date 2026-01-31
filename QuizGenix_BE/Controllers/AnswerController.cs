using Microsoft.AspNetCore.Mvc;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Controllers
{
    [ApiController]
    [Route("/api/answers")]
    public class AnswerController : Controller
    {
        private IAnswerService answerService;
        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }
        [HttpPost("{studentId}/add")]
        public async Task<IActionResult> AddAnswer(Guid studentId, [FromBody] AnswerRequestDto answerRequestDto)
        {
            try
            {
                var result = await answerService.CreateAnswers(answerRequestDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
