using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs.PostDtos
{
    public class PostArticleDto
    {
        [Required(ErrorMessage = "Article Id is required.")]

        public int Id { get; set; }

        [Required(ErrorMessage = "Lesson Id is required.")]
        public int LessonId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(30000, ErrorMessage = "Content cannot exceed 30000 characters.")]
        public string? Content { get; set; }
    }


}
