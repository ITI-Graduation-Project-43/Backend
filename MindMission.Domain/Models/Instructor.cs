using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents an instructor entity who teaches courses.
    /// </summary>
    public partial class Instructor : BaseEntity, IEntity<string>, ISoftDeletable
    {


        [Required]
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = string.Empty;
        [NotMapped]
        public string FullName => FirstName + " " + LastName;
        [StringLength(1000)]
        [Unicode(false)]
        public string Bio { get; set; } = string.Empty;
        [NotMapped]
        [AllowNull]
        public IFormFile ProfilePictureFile { get; set; }

        [StringLength(500)]
        [Unicode(false)]
        [AllowNull]
        public string? ProfilePicture { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; } = string.Empty;

        public int NoOfCourses { get; set; }
        public int NoOfStudents { get; set; }
        public int NoOfRatings { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        [DefaultValue(0)]
        public double AvgRating { get; set; }

        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; } = null!;

        [InverseProperty(nameof(CourseFeedback.Instructor))]
        public virtual ICollection<CourseFeedback>? CourseFeedbacks { get; set; }

        [InverseProperty(nameof(Course.Instructor))]
        public virtual ICollection<Course> Courses { get; set; } = null!;


    }
}