using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;

namespace MindMission.Domain.Models
{
    [Index(nameof(CourseId), Name = "idx_wishlists_courseid")]
    public partial class Wishlist : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public DateTime AddedDate { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Wishlists")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Wishlists")]
        public virtual Student Student { get; set; }
    }
}
