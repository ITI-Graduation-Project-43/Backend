using MindMission.Application.DTOs.Base;
using MindMission.Domain.Constants;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs.ArticleDtos
{
    public class ArticleCreateDto : IDtoWithId<int>
    {
        [Required(ErrorMessage = ErrorMessages.Required)]

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int LessonId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(30000, ErrorMessage = ErrorMessages.LengthExceeded)]
        public string? Content { get; set; }
    }
}
