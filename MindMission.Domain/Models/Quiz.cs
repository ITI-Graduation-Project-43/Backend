using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(LessonId), Name = "idx_quizzes_lessonid")]
    public partial class Quiz : IEntity<int>
    {
        public Quiz()
        {
        }

        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }
        public int NoOfQuestions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Quizzes")]
        public virtual Lesson Lesson { get; set; }

        [InverseProperty(nameof(Question.Quiz))]
        public virtual ICollection<Question> Questions { get; set; }
    }
}