using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.ArticleDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for creating, updating, editing an article.
    /// </summary>
    public class ArticleCreateDto : IDtoWithId<int>
    {
        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }

        [RequiredField("Content")]
        [MinStringLength(100)]
        public string Content { get; set; } = string.Empty;
    }
}
