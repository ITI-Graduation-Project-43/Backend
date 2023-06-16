
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(CourseId), Name = "idx_TimeTracking_courseid")]
    public  class TimeTracking: IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [ForeignKey(nameof(CourseId))]
        [InverseProperty("TimeTrackings")]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("TimeTrackings")]
        public virtual Student Student { get; set; }
    }
}
