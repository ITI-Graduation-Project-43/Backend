using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_quizzes_lessonid")]
    public partial class Quiz : BaseEntity, IEntity<int>, ISoftDeletable
    {
        public Quiz()
        {
        }

        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }
        public int NoOfQuestions { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Quiz")]
        public virtual Lesson Lesson { get; set; }

        [InverseProperty(nameof(Question.Quiz))]
        public virtual ICollection<Question> Questions { get; set; }
    }
}