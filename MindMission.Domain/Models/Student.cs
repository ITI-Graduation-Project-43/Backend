using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindMission.Domain.Models
{
    //[Index(nameof(Id), Name = "UQ__Students__1788CC4D11934C53", IsUnique = true)]
    //[Index(nameof(Id), Name = "idx_students_userid")]
    public partial class Student : BaseEntity, IEntity<string>, ISoftDeletable
    {
        public Student()
        {

        }

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

        [StringLength(1000)]
        [Unicode(false)]

        public string Bio { get; set; } = string.Empty;

        [StringLength(500)]
        [Unicode(false)]
        [AllowNull]
        public string? ProfilePicture { get; set; } = string.Empty;

        public int NumCourses { get; set; }
        public int NumWishlist { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; }

        [InverseProperty(nameof(CourseFeedback.Student))]
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; }

        [InverseProperty(nameof(Enrollment.Student))]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [InverseProperty(nameof(Wishlist.Student))]
        public virtual ICollection<Wishlist> Wishlists { get; set; }

        [InverseProperty(nameof(TimeTracking.Student))]
        public ICollection<TimeTracking> TimeTrackings { get; set; }
    }
}