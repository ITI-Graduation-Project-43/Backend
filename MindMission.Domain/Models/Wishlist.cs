using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a wishlist item that includes a course added by a student.
    /// </summary>
    [Index(nameof(CourseId), Name = "idx_wishlists_courseid")]
    public partial class Wishlist : BaseEntity, IEntity<int>, ISoftDeletable
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string StudentId { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Wishlists")]
        public virtual Course Course { get; set; } = null!;

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Wishlists")]
        public virtual Student Student { get; set; } = null!;
    }
}