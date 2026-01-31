using Microsoft.AspNetCore.Mvc;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;

namespace QuizGenix_BE.Controllers
{
    [ApiController]
    [Route("/api/lesson")]
    public class LessonController : Controller
    {
        private ILessonService lessonService;
        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }
        [HttpPost("{teacherId}/add")]
        public async Task<IActionResult> CreateLessonContent([FromRoute] Guid teacherId, [FromBody] CreateLessonDto createLessonDto) 
        {
            try 
            {
                var result = await lessonService.CreateLesson(teacherId,createLessonDto);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{teacherId}/get")]
        public async Task<IActionResult> GetLessonByTeacher(Guid teacherId)
        {
            try
            {
                var result = await lessonService.GetLessonsByTeacher(teacherId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
