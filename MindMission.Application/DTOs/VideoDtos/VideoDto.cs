using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.VideoDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a video.
    /// </summary>
    public class VideoDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; } = string.Empty;
        public string? LessonName { get; set; }
        public string? LessonDescription { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



    }
}