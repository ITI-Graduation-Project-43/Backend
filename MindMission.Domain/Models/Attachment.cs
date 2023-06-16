using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_attachments_lessonid")]
    public partial class Attachment : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;


        [Required]
        public byte[] FileData { get; set; } = new byte[2048];
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Attachments")]
        public virtual Lesson Lesson { get; set; } = new Lesson();

        [Required]
        [EnumDataType(typeof(FileType))]
        public string FileType { get; set; } = string.Empty;
    }
}