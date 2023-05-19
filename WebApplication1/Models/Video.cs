using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    [Index(nameof(LessonId), Name = "idx_videos_lessonid")]
    public partial class Video
    {
        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        [Required]
        [StringLength(2048)]
        public string VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Videos")]
        public virtual Lesson Lesson { get; set; }
    }
}
