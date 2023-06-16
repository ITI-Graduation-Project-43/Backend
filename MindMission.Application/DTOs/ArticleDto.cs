using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class ArticleDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string? LessonName { get; set; }

    }
}