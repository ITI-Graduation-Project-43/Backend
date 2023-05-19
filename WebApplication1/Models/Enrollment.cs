using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    [Index(nameof(CourseId), Name = "idx_enrollments_courseid")]
    public partial class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Enrollments")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Enrollments")]
        public virtual Student Student { get; set; }
    }
}
