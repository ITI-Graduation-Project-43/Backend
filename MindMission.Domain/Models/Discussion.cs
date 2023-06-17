using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a discussion associated with a lesson, where users can post comments and upvote discussions.
    /// </summary>
    [Index(nameof(LessonId), Name = "idx_discussions_lessonid")]
    public partial class Discussion : BaseEntity, IEntity<int>, ISoftDeletable
    {

        [Key]
        public int Id { get; set; }
        [Required]

        public int LessonId { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [AllowNull]
        public int? ParentDiscussionId { get; set; }

        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string Content { get; set; } = string.Empty;

        public int Upvotes { get; set; }
        public bool IsDeleted { get; set; } = false;



        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Discussions")]
        public virtual Lesson Lesson { get; set; } = null!;

        [ForeignKey(nameof(ParentDiscussionId))]
        [InverseProperty(nameof(InverseParentDiscussion))]
        public virtual Discussion? ParentDiscussion { get; set; }

        [InverseProperty(nameof(ParentDiscussion))]
        public virtual ICollection<Discussion>? InverseParentDiscussion { get; set; }
    }
}