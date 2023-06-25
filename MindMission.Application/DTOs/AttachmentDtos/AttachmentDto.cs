using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs.AttachmentDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for an attachment.
    /// </summary>
    public class AttachmentDto : IDtoWithId<int>
    {
        public int Id { get; set; }

        public string AttachmentUrl { get; set; } = string.Empty;
        public string AttachmentName { get; set; } = string.Empty;
        public string AttachmentType { get; set; } = string.Empty;
        public string? LessonName { get; set; }
        public string? LessonDescription { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



    }
}