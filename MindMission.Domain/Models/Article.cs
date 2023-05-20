using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_articles_lessonid")]
    public partial class Article : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        [Required]
        [StringLength(2048)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Articles")]
        public virtual Lesson Lesson { get; set; }
    }
}
