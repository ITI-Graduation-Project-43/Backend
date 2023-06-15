using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(CourseId), Name = "idx_enrollments_courseid")]
    public partial class Enrollment : IEntity<int>
    {
        public Enrollment()
        {

        }

        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Enrollments")]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Enrollments")]
        public virtual Student Student { get; set; }
    }
}