using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using MindMission.Domain.Models.Base;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a video associated with a lesson.
    /// </summary>
    [Index(nameof(LessonId), Name = "idx_videos_lessonid")]
    public partial class Video : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LessonId { get; set; }


        [Required]
        [StringLength(2048)]
        public string VideoUrl { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Video")]
        public virtual Lesson Lesson { get; set; } = null!;
    }
}