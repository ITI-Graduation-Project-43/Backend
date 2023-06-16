using MindMission.Application.DTOs.PostDtos.Base;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs.PostDtos
{
    public class PostQuizLessonDto : LessonDtoBase
    {
        public LessonType Type { get; } = LessonType.Quiz;

        public List<PostQuestionDto> Questions { get; set; } = new List<PostQuestionDto>();
    }

    public class PostQuestionDto
    {
        [Required(ErrorMessage = "QuestionText is required.")]
        [StringLength(500, ErrorMessage = "QuestionText cannot exceed 500 characters.")]
        public string QuestionText { get; set; } = string.Empty;

        [Required(ErrorMessage = "ChoiceA is required.")]
        [StringLength(255, ErrorMessage = "ChoiceA cannot exceed 255 characters.")]
        public string ChoiceA { get; set; } = string.Empty;

        [Required(ErrorMessage = "ChoiceB is required.")]
        [StringLength(255, ErrorMessage = "ChoiceB cannot exceed 255 characters.")]
        public string ChoiceB { get; set; } = string.Empty;

        [Required(ErrorMessage = "ChoiceC is required.")]
        [StringLength(255, ErrorMessage = "ChoiceC cannot exceed 255 characters.")]
        public string ChoiceC { get; set; } = string.Empty;

        [Required(ErrorMessage = "ChoiceD is required.")]
        [StringLength(255, ErrorMessage = "ChoiceD cannot exceed 255 characters.")]
        public string ChoiceD { get; set; } = string.Empty;

        [Required(ErrorMessage = "CorrectAnswer is required.")]
        [RegularExpression("^[A-D]$", ErrorMessage = "CorrectAnswer must be either A, B, C, or D.")]
        public string CorrectAnswer { get; set; } = string.Empty;
    }
}
