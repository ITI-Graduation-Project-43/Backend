using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_articles_lessonid")]
    public partial class Article : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Articles")]
        public virtual Lesson Lesson { get; set; } = new();
    }
}