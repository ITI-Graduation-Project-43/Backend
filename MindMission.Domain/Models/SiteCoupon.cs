using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindMission.Domain.Common;

namespace MindMission.Domain.Models
{
    public class SiteCoupon : IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        public string Code { get; set; } = string.Empty;

        [Required, Range(0,100)]
        public decimal? Discount { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
