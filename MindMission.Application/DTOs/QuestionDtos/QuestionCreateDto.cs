using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.QuestionDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for creating, updating, editing a quiz questions.
    /// </summary>
    public class QuizQuestionCreateDto
    {

        [RequiredField("Question Text")]
        [MaxStringLength(500)]
        public string QuestionText { get; set; } = string.Empty;

        [RequiredField("Choice A")]
        [MaxStringLength(255)]
        public string ChoiceA { get; set; } = string.Empty;

        [RequiredField("Choice B")]
        [MaxStringLength(255)]
        public string ChoiceB { get; set; } = string.Empty;

        [RequiredField("Choice C")]
        [MaxStringLength(255)]
        public string ChoiceC { get; set; } = string.Empty;

        [RequiredField("Choice D")]
        [MaxStringLength(255)]
        public string ChoiceD { get; set; } = string.Empty;


        [McqCorrectAnswer]
        public string CorrectAnswer { get; set; } = string.Empty;
    }
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for creating, updating, editing a question.
    /// </summary>

    public class QuestionCreateDto : QuizQuestionCreateDto, IDtoWithId<int>
    {
        public int Id { get; set; }
        [RequiredInteger("Quiz Id")]
        public int QuizId { get; set; }
    }

}