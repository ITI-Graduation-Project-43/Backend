using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents a question entity associated with a quiz.
    /// </summary>
    [Index(nameof(QuizId), Name = "idx_questions_quizid")]
    public partial class Question : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int QuizId { get; set; }

        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ChoiceA { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ChoiceB { get; set; } = string.Empty;


        [StringLength(255)]
        public string? ChoiceC { get; set; } = string.Empty;


        [StringLength(255)]
        public string? ChoiceD { get; set; } = string.Empty;

        [Required]
        [StringLength(1)]
        [RegularExpression("^[A-D]$")]

        [Unicode(false)]
        public string CorrectAnswer { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(QuizId))]
        [InverseProperty("Questions")]
        public virtual Quiz Quiz { get; set; } = null!;
    }
}