﻿using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(ChapterId), Name = "idx_lessons_chapterid")]
    public partial class Lesson : IEntity<int>
    {
        public Lesson()
        {
            Articles = new HashSet<Article>();
            Attachments = new HashSet<Attachment>();
            Discussions = new HashSet<Discussion>();
            Quizzes = new HashSet<Quiz>();
            Videos = new HashSet<Video>();
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

        public int NoOfHours { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

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