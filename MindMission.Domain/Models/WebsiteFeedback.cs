using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents feedback provided by users regarding the website.
    /// </summary>
    [Table("WebsiteFeedback")]
    [Index(nameof(UserId), Name = "idx_websitefeedback_userid")]
    public partial class WebsiteFeedback : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Column(TypeName = "decimal(3, 2)")]
        [Range(0, 5)]
        public decimal Rating { get; set; }
        public bool IsDeleted { get; set; } = false;

        [StringLength(2048)]
        [Unicode(false)]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public string FeedbackText { get; set; } = string.Empty;
    }
}