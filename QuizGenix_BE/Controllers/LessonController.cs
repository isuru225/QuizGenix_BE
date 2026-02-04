using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizGenix_BE.DTOs;
using QuizGenix_BE.IServices;
using QuizGenix_BE.Models;

namespace QuizGenix_BE.Controllers
{
    [ApiController]
    [Route("/api/lesson")]
    [Authorize]
    public class LessonController : Controller
    {
        private ILessonService lessonService;
        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }
        [HttpPost("{teacherId}/add")]
        [Authorize(Roles = "Teacher")]
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
        [Authorize(Roles = "Teacher")]
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

        [HttpGet("{lessonId}/getbylessonid")]
        public async Task<IActionResult> GetLessonByLessonId(Guid lessonId)
        {
            try
            {
                var result = await lessonService.GetLessonById(lessonId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{lessonId}/updatelesson")]
        public async Task<IActionResult> UpdateLessonById([FromRoute] Guid lessonId, [FromBody] CreateLessonDto createLessonDto)
        {
            try
            {
                var result = await lessonService.UpdateLesson(lessonId, createLessonDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
