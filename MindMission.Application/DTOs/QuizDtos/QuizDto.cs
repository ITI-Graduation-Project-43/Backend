using MindMission.Application.DTOs.Base;
using MindMission.Application.DTOs.QuestionDtos;

namespace MindMission.Application.DTOs.QuizDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a quiz.
    /// </summary>
    public class QuizDto : IDtoWithId<int>
    {
        public int Id { get; set; }

        public int NoOfQuestions { get { return Questions?.Count ?? 0; } }
        public List<QuizQuestionDto> Questions { get; set; } = new List<QuizQuestionDto>();

        public string? LessonName { get; set; }
        public string? LessonDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }

}