using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.QuestionDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a quiz questions.
    /// </summary>
    public class QuizQuestionDto
    {
        public string QuestionText { get; set; } = string.Empty;

        public string ChoiceA { get; set; } = string.Empty;

        public string ChoiceB { get; set; } = string.Empty;

        public string ChoiceC { get; set; } = string.Empty;

        public string ChoiceD { get; set; } = string.Empty;

        public string CorrectAnswer { get; set; } = string.Empty;
    }
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a  question.
    /// </summary>
    public class QuestionDto : QuizQuestionDto, IDtoWithId<int>
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
    }
}
