using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a quiz entity associated with a lesson.
    /// </summary>
    [Index(nameof(LessonId), Name = "idx_quizzes_lessonid")]
    public partial class Quiz : BaseEntity, IEntity<int>, ISoftDeletable
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public int LessonId { get; set; }
        [NotMapped]
        public int NoOfQuestions => Questions.Count;


        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Quiz")]
        public virtual Lesson Lesson { get; set; } = null!;

        [InverseProperty(nameof(Question.Quiz))]
        public virtual ICollection<Question> Questions { get; set; } = null!;
    }
}