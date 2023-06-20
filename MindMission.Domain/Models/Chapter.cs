using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a chapter entity within a course.
    /// </summary>
    [Index(nameof(CourseId), Name = "idx_chapters_courseid")]
    public partial class Chapter : BaseEntity, IEntity<int>, ISoftDeletable
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Title { get; set; } = string.Empty;
        public int NoOfLessons { get; set; }
        public float NoOfHours { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Chapters")]
        public virtual Course Course { get; set; } = null!;

        [InverseProperty(nameof(Lesson.Chapter))]
        public virtual ICollection<Lesson> Lessons { get; set; } = null!;
    }
}