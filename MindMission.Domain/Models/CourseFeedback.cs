using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents the feedback provided by students for a course, including ratings and comments.
    /// </summary>
    [Index(nameof(StudentId), nameof(CourseId), Name = "idx_coursefeedbacks_student_course")]
    [Table("CourseFeedbacks")]
    public partial class CourseFeedback : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]

        public string StudentId { get; set; } = string.Empty;
        [Required]

        public string InstructorId { get; set; } = string.Empty;

        [Column(TypeName = "decimal(3, 2)")]
        [Range(0, 5)]

        public decimal InstructorRating { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        [Range(0, 5)]
        public decimal CourseRating { get; set; }

        [StringLength(2048)]
        public string FeedbackText { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Course Course { get; set; } = null!;

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Instructor Instructor { get; set; } = null!;

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Student Student { get; set; } = null!;
    }
}