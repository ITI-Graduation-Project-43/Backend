using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.ArticleDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for an article.
    /// </summary>
    public class ArticleDto : IDtoWithId<int>
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;
        public string? LessonName { get; set; }
        public string? LessonDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}