using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MindMission.Domain.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            CourseFeedbacks = new HashSet<CourseFeedback>();
            Courses = new HashSet<Course>();
        }
        [Required]
        [Key]
        public string UserId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string Bio { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string ProfilePicture { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Title { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; }
        public int NoOfCourses { get; set; }
        public int NoOfStudents { get; set; }
        public double? AvgRating { get; set; }
        public int NoOfRatings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [InverseProperty(nameof(CourseFeedback.Instructor))]
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; }
        [InverseProperty(nameof(Course.Instructor))]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
