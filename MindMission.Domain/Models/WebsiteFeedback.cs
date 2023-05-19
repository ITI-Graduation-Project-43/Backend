using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MindMission.Domain.Models
{
    [Table("WebsiteFeedback")]
    [Index(nameof(UserId), Name = "idx_websitefeedback_userid")]
    public partial class WebsiteFeedback
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Rating { get; set; }
        [StringLength(2048)]
        [Unicode(false)]

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string FeedbackText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
