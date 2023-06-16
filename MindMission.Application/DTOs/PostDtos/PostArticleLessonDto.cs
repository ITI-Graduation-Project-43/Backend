using MindMission.Application.DTOs.PostDtos.Base;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs.PostDtos
{
    public class PostArticleLessonDto : LessonDtoBase
    {
        public LessonType Type { get; } = LessonType.Article;
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;
    }
}
