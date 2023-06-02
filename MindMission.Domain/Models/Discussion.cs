using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_discussions_lessonid")]
    public partial class Discussion : IEntity<int>
    {
        public Discussion()
        {
            InverseParentDiscussion = new HashSet<Discussion>();
        }

        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }
        public string UserId { get; set; }
        [AllowNull]
        public int? ParentDiscussionId { get; set; }

        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string Content { get; set; }

        public int Upvotes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Discussions")]
        public virtual Lesson Lesson { get; set; }

        [ForeignKey(nameof(ParentDiscussionId))]
        [InverseProperty(nameof(InverseParentDiscussion))]
        public virtual Discussion ParentDiscussion { get; set; }

        [InverseProperty(nameof(ParentDiscussion))]
        public virtual ICollection<Discussion> InverseParentDiscussion { get; set; }
    }
}