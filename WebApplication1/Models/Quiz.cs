using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    [Index(nameof(LessonId), Name = "idx_quizzes_lessonid")]
    public partial class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int NoOfQuestions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Quizzes")]
        public virtual Lesson Lesson { get; set; }
        [InverseProperty(nameof(Question.Quiz))]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
