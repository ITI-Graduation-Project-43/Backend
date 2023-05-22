using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_attachments_lessonid")]
    public partial class Attachment : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        [Required]
        [StringLength(2048)]
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Attachments")]
        public virtual Lesson Lesson { get; set; }
    }
}
