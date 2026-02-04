namespace QuizGenix_BE.Models
{
    public class Teaching
    {
        public Guid TeacherId { get; set; }
        public User Teacher { get; set; }
        public int Grade { get; set; }
    }
}
