using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(InstructorId), Name = "idx_courses_instructorid")]
    public partial class Course : IEntity<int>
    {
        public Course()
        {
            Chapters = new HashSet<Chapter>();
            CourseFeedbacks = new HashSet<CourseFeedback>();
            Enrollments = new HashSet<Enrollment>();
            Wishlists = new HashSet<Wishlist>();
            Instructor = new Instructor();
            Category = new Category();
        }

        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string InstructorId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string WhatWillLearn { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string Requirements { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string WholsFor { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(Language))]
        [StringLength(50)]
        [Unicode(false)]
        public string Language { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10, 2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(Level))]
        [StringLength(50)]
        [Unicode(false)]
        public string Level { get; set; } = string.Empty;

        [Column(TypeName = "decimal(3, 2)")]
        public decimal? AvgReview { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of reviews must be greater than or equal to 0.")]
        public int NoOfReviews { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of students must be greater than or equal to 0.")]
        public int NoOfStudents { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1.")]
        public decimal? Discount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Chapter count must be greater than or equal to 0.")]
        public int ChapterCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Lesson count must be greater than or equal to 0.")]
        public int LessonCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of videos must be greater than or equal to 0.")]
        public int NoOfVideos { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of articles must be greater than or equal to 0.")]
        public int NoOfArticles { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of attachments must be greater than or equal to 0.")]
        public int NoOfAttachments { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of hours must be greater than or equal to 0.")]
        public int NoOfHours { get; set; }

        public bool Published { get; set; } = false;
        public bool Approved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Courses")]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty("Courses")]
        public virtual Instructor Instructor { get; set; }

        [InverseProperty(nameof(Chapter.Course))]
        public virtual ICollection<Chapter> Chapters { get; set; }

        [InverseProperty(nameof(CourseFeedback.Course))]
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; }

        [InverseProperty(nameof(Enrollment.Course))]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [InverseProperty(nameof(Wishlist.Course))]
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}