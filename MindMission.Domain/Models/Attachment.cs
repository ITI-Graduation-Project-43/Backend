using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string FileName { get; set; } = string.Empty;
        [Required]
        [StringLength(2048)]
        public string FileUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public byte[] FileData { get; set; } = new byte[2048];

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Attachments")]
        public virtual Lesson Lesson { get; set; } = new Lesson();

        [Required]
        [EnumDataType(typeof(FileType))]
        public string FileType { get; set; } = string.Empty;
    }
}
