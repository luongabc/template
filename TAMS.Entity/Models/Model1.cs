namespace TAMS.Entity.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<CategoryQuestion> CategoryQuestions { get; set; }
        public virtual DbSet<FormTest> FormTests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestOfUser> TestOfUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<QuestionOfTest> QuestionOfTests { get; set; }
        public virtual DbSet<UserResult> UserResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .HasMany(e => e.UserResults)
                .WithOptional(e => e.Answer)
                .HasForeignKey(e => e.IdAnswer);

            modelBuilder.Entity<CategoryQuestion>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.CategoryQuestion)
                .HasForeignKey(e => e.IdCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryQuestion>()
                .HasMany(e => e.QuestionOfTests)
                .WithRequired(e => e.CategoryQuestion)
                .HasForeignKey(e => e.IdCategoryQuestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FormTest>()
                .HasMany(e => e.Tests)
                .WithRequired(e => e.FormTest)
                .HasForeignKey(e => e.IdForm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.CategoryAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .HasForeignKey(e => e.IdQuestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.QuestionOfTests)
                .WithRequired(e => e.Test)
                .HasForeignKey(e => e.IdTest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.TestOfUsers)
                .WithRequired(e => e.Test)
                .HasForeignKey(e => e.IdTest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestOfUser>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TestOfUser>()
                .HasMany(e => e.UserResults)
                .WithRequired(e => e.TestOfUser)
                .HasForeignKey(e => e.IdTest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TestOfUsers)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);
        }
    }
}
