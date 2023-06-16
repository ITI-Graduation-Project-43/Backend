using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Table("WebsiteFeedback")]
    [Index(nameof(UserId), Name = "idx_websitefeedback_userid")]
    public partial class WebsiteFeedback : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal Rating { get; set; }
        public bool IsDeleted { get; set; } = false;

        [StringLength(2048)]
        [Unicode(false)]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string FeedbackText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}