using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindMission.Domain.Models
{
    //[Index(nameof(Id), Name = "UQ__Students__1788CC4D11934C53", IsUnique = true)]
    //[Index(nameof(Id), Name = "idx_students_userid")]
    public partial class Student : IEntity<string>
    {
        public Student()
        {
            CourseFeedbacks = new HashSet<CourseFeedback>();
            Enrollments = new HashSet<Enrollment>();
            Wishlists = new HashSet<Wishlist>();
        }

        [Key]
        public string Id { get; set; }

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

        public int NumCourses { get; set; }
        public int NumWishlist { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; }

        [InverseProperty(nameof(CourseFeedback.Student))]
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; }

        [InverseProperty(nameof(Enrollment.Student))]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [InverseProperty(nameof(Wishlist.Student))]
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}