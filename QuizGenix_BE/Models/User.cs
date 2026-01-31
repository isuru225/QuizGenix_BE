using QuizGenix_BE.Models;

namespace QuizGenix_BE.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Grade { get; set; } = 0;
        public UserRole Role { get; set; }
        public DateTime AdmissionDate { get; set; } = DateTime.UtcNow;
        public ICollection<StudentExam>? StudentExams { get; set; }
        public ICollection<StudentAnswer>? StudentAnswer { get; set; }
        public ICollection<ExamComposing>? ExamComposings { get; set; }
        public ICollection<Lesson>? lessons { get; set; }
    }

    public enum UserRole
    {
        Teacher,
        Student
    }
}

//public class User
//{
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public string Username { get; set; }
//    public string Email { get; set; }
//    public string PasswordHash { get; set; }
//    public int Grade { get; set; } = 0;
//    public UserRole Role { get; set; }
//    public DateTime AdmissionDate { get; set; } = DateTime.UtcNow;
//    public ICollection<Exam> Exam { get; set; }
//    public ICollection<StudentExam> StudentExams { get; set; }
//}