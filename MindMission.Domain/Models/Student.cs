using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a student entity.
    /// </summary>
    public partial class Student : BaseEntity, IEntity<string>, ISoftDeletable
    {


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
        public string FullName => FirstName + " " + LastName;

        [StringLength(1000)]
        [Unicode(false)]

        public string Bio { get; set; } = string.Empty;

        [StringLength(500)]
        [Unicode(false)]
        [AllowNull]
        public string? ProfilePicture { get; set; } = string.Empty;

        public int NoOfCourses { get; set; }
        public int NoOfWishlist { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; } = null!;

        [InverseProperty(nameof(CourseFeedback.Student))]
        public virtual ICollection<CourseFeedback>? CourseFeedbacks { get; set; }

        [InverseProperty(nameof(Enrollment.Student))]
        public virtual ICollection<Enrollment>? Enrollments { get; set; }

        [InverseProperty(nameof(Wishlist.Student))]
        public virtual ICollection<Wishlist>? Wishlists { get; set; }

        [InverseProperty(nameof(TimeTracking.Student))]
        public ICollection<TimeTracking> TimeTrackings { get; set; } = null!;
    }
}