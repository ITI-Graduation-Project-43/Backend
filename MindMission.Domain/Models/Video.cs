using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using MindMission.Domain.Models.Base;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_videos_lessonid")]
    public partial class Video : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }

        [NotMapped]
        public IFormFile VideoFile { get; set; }

        [Required]
        [StringLength(2048)]
        public string VideoUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Videos")]
        public virtual Lesson Lesson { get; set; }
    }
}