using MindMission.Application.DTOs.PostDtos.Base;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs.PostDtos
{
    public class PostVideoLessonDto : LessonDtoBase
    {
        public LessonType Type { get; } = LessonType.Video;

        [Required(ErrorMessage = "VideoUrl is required.")]
        [StringLength(2048, ErrorMessage = "VideoUrl cannot exceed 2048 characters.")]
        public string VideoUrl { get; set; } = string.Empty;
    }
}
