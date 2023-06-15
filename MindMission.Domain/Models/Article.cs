using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_articles_lessonid")]
    public partial class Article : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Articles")]
        public virtual Lesson Lesson { get; set; } = new();
    }
}