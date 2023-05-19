using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    [Index(nameof(QuizId), Name = "idx_questions_quizid")]
    public partial class Question
    {
        [Key]
        public int Id { get; set; }
        public int QuizId { get; set; }
        [Required]
        [StringLength(255)]
        public string QuestionText { get; set; }
        [Required]
        [StringLength(255)]
        public string ChoiceA { get; set; }
        [Required]
        [StringLength(255)]
        public string ChoiceB { get; set; }
        [Required]
        [StringLength(255)]
        public string ChoiceC { get; set; }
        [Required]
        [StringLength(255)]
        public string ChoiceD { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string CorrectAnswer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(QuizId))]
        [InverseProperty("Questions")]
        public virtual Quiz Quiz { get; set; }
    }
}
