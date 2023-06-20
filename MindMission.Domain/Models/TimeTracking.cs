
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a time tracking entity for a specific course and student.
    /// </summary>
    [Index(nameof(CourseId), Name = "idx_TimeTracking_courseid")]
    public class TimeTracking : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string StudentId { get; set; } = string.Empty;
        [Required]
        public int CourseId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [ForeignKey(nameof(CourseId))]
        [InverseProperty("TimeTrackings")]
        public virtual Course Course { get; set; } = null!;

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("TimeTrackings")]
        public virtual Student Student { get; set; } = null!;
    }
}
