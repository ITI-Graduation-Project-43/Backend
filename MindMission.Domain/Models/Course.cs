using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a course entity that belongs to a specific category (topic) and is taught by an instructor.
    /// </summary>
    [Index(nameof(InstructorId), Name = "idx_courses_instructorid")]
    public partial class Course : BaseEntity, IEntity<int>, ISoftDeletable
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]

        public string InstructorId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [StringLength(10000)]
        public string Description { get; set; } = string.Empty;



        [StringLength(2048)]
        [Unicode(false)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(Language))]
        [StringLength(50)]
        [Unicode(false)]
        public string Language { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10, 2)")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(Level))]
        [StringLength(50)]
        [Unicode(false)]
        public string Level { get; set; } = string.Empty;

        [Column(TypeName = "decimal(3, 2)")]
        [Range(0, 1)]
        public decimal? Discount { get; set; }

        #region calculated fields

        [Column(TypeName = "decimal(3, 2)")]

        public decimal? AvgReview { get; set; }

        [Range(0, int.MaxValue)]

        public int NoOfReviews { get; set; }

        [Range(0, int.MaxValue)]

        public int NoOfStudents { get; set; }


        [Range(0, int.MaxValue)]
        public int ChapterCount { get; set; }


        [Range(0, int.MaxValue)]
        public int LessonCount { get; set; }


        [Range(0, int.MaxValue)]
        public int NoOfVideos { get; set; }


        [Range(0, int.MaxValue)]
        public int NoOfArticles { get; set; }


        [Range(0, int.MaxValue)]
        public int NoOfQuizzes { get; set; }


        [Range(0, int.MaxValue)]
        public int NoOfAttachments { get; set; }


        //[Range(0, int.MaxValue)]
        public int NoOfHours { get; set; }



        #endregion
        public bool Published { get; set; } = false;
        public bool Approved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Courses")]
        public virtual Category Category { get; set; } = null!;

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty("Courses")]
        public virtual Instructor Instructor { get; set; } = null!;

        [InverseProperty(nameof(Chapter.Course))]
        public virtual ICollection<Chapter> Chapters { get; set; } = null!;

        [InverseProperty(nameof(CourseFeedback.Course))]
        public virtual ICollection<CourseFeedback>? CourseFeedbacks { get; set; }

        [InverseProperty(nameof(Enrollment.Course))]
        public virtual ICollection<Enrollment>? Enrollments { get; set; }

        [InverseProperty(nameof(Wishlist.Course))]
        public virtual ICollection<Wishlist>? Wishlists { get; set; }
        [InverseProperty(nameof(TimeTracking.Course))]
        public ICollection<TimeTracking>? TimeTrackings { get; set; }


        public ICollection<LearningItem> LearningItems { get; set; } = null!;
        public ICollection<EnrollmentItem> EnrollmentItems { get; set; } = null!;
        public ICollection<CourseRequirement>? CourseRequirements { get; set; }
        public ICollection<Coupon> Coupons { get; set; }
    }


    /// <summary>
    /// Represents a learning item associated with a course.
    /// </summary>
    public class LearningItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
    /// <summary>
    /// Represents an enrollment item associated with a course.
    /// </summary>
    public class EnrollmentItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
    /// <summary>
    /// Represents a course requirement associated with a course.
    /// </summary>
    public class CourseRequirement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}