using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(CourseId), Name = "idx_enrollments_courseid")]
    public partial class Enrollment : BaseEntity, IEntity<int>, ISoftDeletable
    {
        public Enrollment()
        {

        }

        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Enrollments")]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Enrollments")]
        public virtual Student Student { get; set; }
    }
}