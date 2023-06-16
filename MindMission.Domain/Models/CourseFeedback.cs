using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Table("CourseFeedback")]
    [Index(nameof(CourseId), Name = "idx_coursefeedback_courseid")]
    public partial class CourseFeedback : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public string InstructorId { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal InstructorRating { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal CourseRating { get; set; }

        [StringLength(2048)]
        public string FeedbackText { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Instructor Instructor { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Student Student { get; set; }
    }
}