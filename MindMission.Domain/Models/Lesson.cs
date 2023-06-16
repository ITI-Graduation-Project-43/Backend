using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(ChapterId), Name = "idx_lessons_chapterid")]
    public partial class Lesson : BaseEntity, IEntity<int>, ISoftDeletable
    {
        public Lesson()
        {

        }

        [Key]
        public int Id { get; set; }

        public int ChapterId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(2048)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public LessonType Type { get; set; }

        public float NoOfHours { get; set; }
        public bool IsFree { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(ChapterId))]
        [InverseProperty("Lessons")]
        public virtual Chapter Chapter { get; set; }

        [InverseProperty(nameof(Article.Lesson))]
        public virtual ICollection<Article> Articles { get; set; }

        [InverseProperty(nameof(Attachment.Lesson))]
        public virtual ICollection<Attachment> Attachments { get; set; }

        [InverseProperty(nameof(Discussion.Lesson))]
        public virtual ICollection<Discussion> Discussions { get; set; }

        [InverseProperty(nameof(Quiz.Lesson))]
        public virtual ICollection<Quiz> Quizzes { get; set; }

        [InverseProperty(nameof(Video.Lesson))]
        public virtual ICollection<Video> Videos { get; set; }
    }
}