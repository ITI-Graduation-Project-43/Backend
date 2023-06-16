using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(CourseId), Name = "idx_wishlists_courseid")]
    public partial class Wishlist : BaseEntity, IEntity<int>, ISoftDeletable
    {
        public Wishlist()
        {

        }

        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Wishlists")]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Wishlists")]
        public virtual Student Student { get; set; }
    }
}