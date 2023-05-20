using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        }

        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string InstructorId { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string WhatWillLearn { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string Requirements { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string WholsFor { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Language { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Level { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal? AvgReview { get; set; }
        public int NoOfReviews { get; set; }
        public int NoOfStudents { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal? Discount { get; set; }
        public int ChapterCount { get; set; }
        public int LessonCount { get; set; }
        public int NoOfVideos { get; set; }
        public int NoOfArticles { get; set; }
        public int NoOfAttachments { get; set; }
        public int NoOfHours { get; set; }
        public bool Published { get; set; }
        public bool Approved { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

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
