using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_videos_lessonid")]
    public partial class Video : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }

        [Required]
        [StringLength(2048)]
        public string VideoUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Videos")]
        public virtual Lesson Lesson { get; set; }
    }
}