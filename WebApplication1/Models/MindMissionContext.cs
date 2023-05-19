using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class MindMissionContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminPermission> AdminPermissions { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseFeedback> CourseFeedbacks { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserAuthProvider> UserAuthProviders { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<WebsiteFeedback> WebsiteFeedbacks { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        public MindMissionContext(DbContextOptions<MindMissionContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountType).IsUnicode(false);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsUnicode(false);

                entity.Property(e => e.ProfilePicture).IsUnicode(false);
            });

            modelBuilder.Entity<AdminPermission>(entity =>
            {
                entity.HasKey(e => new { e.AdminId, e.PermissionId })
                    .HasName("PK__AdminPer__9F658B3A6B1E0167");

                entity.Property(e => e.GrantedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminPermissions)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AdminPerm__Admin__6EC0713C");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AdminPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AdminPerm__Permi__6FB49575");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Articles__Lesson__395884C4");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attachmen__Lesso__3D2915A8");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__Categorie__Paren__7B5B524B");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chapters__Course__1CBC4616");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Language).IsUnicode(false);

                entity.Property(e => e.Level).IsUnicode(false);

                entity.Property(e => e.Requirements).IsUnicode(false);

                entity.Property(e => e.ShortDescription).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.Property(e => e.WhatWillLearn).IsUnicode(false);

                entity.Property(e => e.WholsFor).IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Courses__Categor__00200768");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Courses__Instruc__01142BA1");
            });

            modelBuilder.Entity<CourseFeedback>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseFeedbacks)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseFee__Cours__4D5F7D71");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.CourseFeedbacks)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseFee__Instr__4F47C5E3");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseFeedbacks)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CourseFee__Stude__4E53A1AA");
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Discussio__Lesso__40F9A68C");

                entity.HasOne(d => d.ParentDiscussion)
                    .WithMany(p => p.InverseParentDiscussion)
                    .HasForeignKey(d => d.ParentDiscussionId)
                    .HasConstraintName("FK__Discussio__Paren__42E1EEFE");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Enrollmen__Cours__55009F39");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Enrollmen__Stude__55F4C372");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ProfilePicture).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type).IsUnicode(false);

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ChapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lessons__Chapter__245D67DE");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.CorrectAnswer)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questions__QuizI__30C33EC3");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quizzes__LessonI__2B0A656D");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ProfilePicture).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsUnicode(false);

                entity.Property(e => e.ProfilePicture).IsUnicode(false);

                entity.Property(e => e.UserType).IsUnicode(false);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.Property(e => e.AccountLink).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccounts_Accounts");
            });

            modelBuilder.Entity<UserAuthProvider>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__UserAuth__349DA5A6CF8D21B4");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Videos__LessonId__3587F3E0");
            });

            modelBuilder.Entity<WebsiteFeedback>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FeedbackText).IsUnicode(false);
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.Property(e => e.AddedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wishlists__Cours__59C55456");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wishlists__Stude__5AB9788F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
