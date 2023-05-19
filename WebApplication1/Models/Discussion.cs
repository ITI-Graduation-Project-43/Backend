using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    [Index(nameof(LessonId), Name = "idx_discussions_lessonid")]
    public partial class Discussion
    {
        public Discussion()
        {
            InverseParentDiscussion = new HashSet<Discussion>();
        }

        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int UserId { get; set; }
        public int? ParentDiscussionId { get; set; }
        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string Content { get; set; }
        public int Upvotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Discussions")]
        public virtual Lesson Lesson { get; set; }
        [ForeignKey(nameof(ParentDiscussionId))]
        [InverseProperty(nameof(Discussion.InverseParentDiscussion))]
        public virtual Discussion ParentDiscussion { get; set; }
        [InverseProperty(nameof(Discussion.ParentDiscussion))]
        public virtual ICollection<Discussion> InverseParentDiscussion { get; set; }
    }
}
