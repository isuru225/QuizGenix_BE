using System.ComponentModel.DataAnnotations;

namespace QuizGenix_BE.DTOs
{
    public class CreateLessonDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}
