using Microsoft.EntityFrameworkCore;
using QuizGenix_BE.Models;

namespace QuizGenix_BE.DataLayer
{
    public class QuizGenixDBContext : DbContext
    {
        public QuizGenixDBContext(DbContextOptions<QuizGenixDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamComposing> ExamComposings { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<Teaching> Teachings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // USER
            // ============================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).IsRequired();
            });

            // ============================
            // EXAM
            // ============================
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

                entity.HasMany(e => e.Questions)
                      .WithOne(q => q.Exam)
                      .HasForeignKey(q => q.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================
            // QUESTION
            // ============================
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.Id);

                entity.Property(q => q.QuestionText).IsRequired();
                entity.Property(q => q.CorrectAnswer).IsRequired();

                entity.HasOne(q => q.Exam)
                      .WithMany(e => e.Questions)
                      .HasForeignKey(q => q.ExamId);
            });

            // ============================
            // ANSWER
            // ============================
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.SelectedAnswer).IsRequired();
            });

            // ============================
            // STUDENT EXAM (Many-to-Many)
            // ============================
            modelBuilder.Entity<StudentExam>(entity =>
            {
                entity.HasKey(se => new { se.ExamId, se.StudentId });

                entity.HasOne(se => se.Exam)
                      .WithMany(e => e.StudentExams)
                      .HasForeignKey(se => se.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(se => se.Student)
                      .WithMany(u => u.StudentExams)
                      .HasForeignKey(se => se.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================
            // STUDENT ANSWER (Ternary)
            // ============================
            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.HasKey(sa => new { sa.UserId, sa.QuestionId, sa.AnswerId });

                entity.HasOne(sa => sa.Student)
                      .WithMany(u => u.StudentAnswer)
                      .HasForeignKey(sa => sa.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sa => sa.Question)
                      .WithMany(q => q.StudentAnswers)
                      .HasForeignKey(sa => sa.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sa => sa.Answer)
                      .WithMany(a => a.StudentAnswers)
                      .HasForeignKey(sa => sa.AnswerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================
            // LESSON
            // ============================
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("lessons");
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Title).IsRequired();
                entity.Property(l => l.Content).IsRequired();
                entity.Property(l => l.Subject).IsRequired();
                entity.Property(l => l.Status).IsRequired();

                entity.HasOne(l => l.Teacher)
                      .WithMany(u => u.lessons)
                      .HasForeignKey(l => l.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================
            // EXAM COMPOSING (Ternary)
            // ============================
            modelBuilder.Entity<ExamComposing>(entity =>
            {
                entity.HasKey(lc => new { lc.ExamId, lc.LessonId, lc.TeacherId });

                entity.HasOne(lc => lc.Exam)
                      .WithMany(e => e.ExamComposings)
                      .HasForeignKey(lc => lc.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(lc => lc.Lesson)
                      .WithMany(l => l.ExamComposings)
                      .HasForeignKey(lc => lc.LessonId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(lc => lc.Teacher)
                      .WithMany(u => u.ExamComposings)
                      .HasForeignKey(lc => lc.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================
            // EXAM COMPOSING (Many-to-Many)
            // ============================

            modelBuilder.Entity<Teaching>(entity =>
            {
                // Composite Primary Key
                entity.HasKey(t => new { t.TeacherId, t.Grade });

                // Teacher relationship
                entity.HasOne(t => t.Teacher)
                      .WithMany(u => u.Teachings)
                      .HasForeignKey(t => t.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(t => t.Grade)
                      .IsRequired();
            });
        }

    }
}
