using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a lesson entity that belongs to a specific chapter.
    /// </summary>
    [Index(nameof(ChapterId), Name = "idx_lessons_chapterid")]
    public partial class Lesson : BaseEntity, IEntity<int>, ISoftDeletable
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public int ChapterId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2048)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public LessonType Type { get; set; }

        public float NoOfHours { get; set; }
        public bool IsFree { get; set; } = false;
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(ChapterId))]
        [InverseProperty("Lessons")]
        public virtual Chapter Chapter { get; set; } = null!;


        [InverseProperty(nameof(Discussion.Lesson))]
        public virtual ICollection<Discussion>? Discussions { get; set; }


        public virtual Attachment? Attachment { get; set; }

        public virtual Article? Article { get; set; }
        public virtual Quiz? Quiz { get; set; }
        public virtual Video? Video { get; set; }
    }
}