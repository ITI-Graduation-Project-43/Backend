using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;



namespace MindMission.Application.DTOs.VideoDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for creating, updating, editing a video.
    /// </summary>
    public class VideoCreateDto : IDtoWithId<int>
    {

        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }

        [RequiredField("VideoUrl")]
        [MaxStringLength(2048)]
        public string VideoUrl { get; set; } = string.Empty;
    }
}
