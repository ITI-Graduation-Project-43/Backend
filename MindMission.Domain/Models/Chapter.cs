using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(CourseId), Name = "idx_chapters_courseid")]
    public partial class Chapter : IEntity<int>
    {
        public Chapter()
        {
            Lessons = new HashSet<Lesson>();
            Course = new Course();
        }

        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Title { get; set; } = string.Empty;

        public int NoOfLessons { get; set; }
        public int NoOfHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Chapters")]
        public virtual Course Course { get; set; }

        [InverseProperty(nameof(Lesson.Chapter))]
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}