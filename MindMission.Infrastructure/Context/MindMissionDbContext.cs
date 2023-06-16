using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using MindMission.Domain.Models.Base;
using System.Reflection.Emit;

namespace MindMission.Infrastructure.Context
{
    public class MindMissionDbContext : IdentityDbContext<User>
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
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<WebsiteFeedback> WebsiteFeedbacks { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        public MindMissionDbContext(DbContextOptions<MindMissionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType).Property("CreatedAt").HasDefaultValueSql("getdate()");
                    builder.Entity(entityType.ClrType).Property("UpdatedAt").HasDefaultValueSql("getdate()");
                }
            }
            base.OnModelCreating(builder);
            builder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountType).IsUnicode(false);
            });

            builder.Entity<Admin>(entity =>
            {

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ProfilePicture).IsUnicode(false);
            });

            builder.Entity<AdminPermission>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.PermissionId })
                    .HasName("PK__AdminPer__9F658B3A6B1E0167");



                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminPermissions)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AdminPerm__Admin__6EC0713C");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AdminPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AdminPerm__Permi__6FB49575");
            });

            builder.Entity<Article>(entity =>
            {


                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Articles__Lesson__395884C4");
            });

            builder.Entity<Attachment>(entity =>
            {


                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attachmen__Lesso__3D2915A8");
            });

            builder.Entity<Category>(entity =>
            {

                entity.Property(e => e.Name).IsUnicode(false);
                entity.Property(e => e.Type).HasConversion(new EnumToStringConverter<CategoryType>());

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__Categorie__Paren__7B5B524B");

                entity.Property(e => e.ParentId).IsRequired(false);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<Chapter>(entity =>
            {

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chapters__Course__1CBC4616");
            });

            builder.Entity<Course>(entity =>
            {


                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Language).IsUnicode(false);

                entity.Property(e => e.Level).IsUnicode(false);

                entity.Property(e => e.ShortDescription).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

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

            builder.Entity<CourseFeedback>(entity =>
            {

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

            builder.Entity<Discussion>(entity =>
            {
                entity.Property(e => e.Content).IsUnicode(false);

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

            builder.Entity<Enrollment>(entity =>
            {

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

            builder.Entity<Instructor>(entity =>
            {
                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ProfilePicture).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);
            });

            builder.Entity<Lesson>(entity =>
            {

                entity.Property(e => e.Type).HasConversion(new EnumToStringConverter<LessonType>());

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ChapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lessons__Chapter__245D67DE");
            });

            builder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            builder.Entity<Question>(entity =>
            {
                entity.Property(e => e.CorrectAnswer)
                    .IsUnicode(false)
                    .IsFixedLength();


                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questions__QuizI__30C33EC3");
            });

            builder.Entity<Quiz>(entity =>
            {


                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quizzes__LessonI__2B0A656D");
            });

            builder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Bio).IsUnicode(false).IsRequired(false);


                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ProfilePicture).IsUnicode(false).IsRequired(false);
            });

            builder.Entity<User>(entity =>
            {
                entity.Ignore(e => e.UserName);

                entity.Ignore(e => e.NormalizedUserName);

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PasswordHash).IsUnicode(false);
            });

            builder.Entity<UserAccount>(entity =>
            {
                entity.Property(e => e.AccountLink).IsUnicode(false);


                entity.HasOne(d => d.Account)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccounts_Accounts");
            });

            builder.Entity<Video>(entity =>
            {

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Videos__LessonId__3587F3E0");
            });

            builder.Entity<WebsiteFeedback>(entity =>
            {

                entity.Property(e => e.FeedbackText).IsUnicode(false);
            });

            builder.Entity<Wishlist>(entity =>
            {
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
        }
    }
}