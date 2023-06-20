using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents an enrollment of a student in a course.
    /// </summary>
    [Index(nameof(CourseId), Name = "idx_enrollments_courseid")]
    public partial class Enrollment : BaseEntity, IEntity<int>, ISoftDeletable
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]

        public string StudentId { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Enrollments")]
        public virtual Course Course { get; set; } = null!;

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Enrollments")]
        public virtual Student Student { get; set; } = null!;
    }
}