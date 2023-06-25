using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;
using MindMission.Application.DTOs.QuestionDtos;

namespace MindMission.Application.DTOs.QuizDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for creating, updating, editing a quiz.
    /// </summary>
    public class QuizCreateDto : IDtoWithId<int>
    {

        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }
        [ListCount(1)]
        public List<QuestionCreateDto> Questions { get; set; } = new List<QuestionCreateDto>();
    }

}
