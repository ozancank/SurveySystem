using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SurveySystem.Models
{
    public partial class SurveyContext : DbContext
    {
        public SurveyContext()
        {
        }

        public SurveyContext(DbContextOptions<SurveyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AnswerLine> AnswerLines { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Question> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Survey;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.PersonCode).HasMaxLength(10);

                entity.Property(e => e.PersonName).HasMaxLength(50);

                entity.Property(e => e.Score).HasMaxLength(10);

                entity.Property(e => e.UserCode).HasMaxLength(50);
            });

            modelBuilder.Entity<AnswerLine>(entity =>
            {
                entity.ToTable("AnswerLine");

                entity.Property(e => e.Answer).HasMaxLength(10);

                entity.Property(e => e.Image).HasMaxLength(150);

                entity.Property(e => e.Question).HasMaxLength(250);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.NameSurname).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(10);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.QuestionLine)
                    .HasMaxLength(250)
                    .HasColumnName("QuestionLine");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
