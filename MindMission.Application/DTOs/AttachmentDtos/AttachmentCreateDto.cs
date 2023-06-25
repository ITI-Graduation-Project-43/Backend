using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;



namespace MindMission.Application.DTOs.AttachmentDtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for creating, updating, editing an attachment.
    /// </summary>
    public class AttachmentCreateDto : IDtoWithId<int>
    {

        public int Id { get; set; }

        [RequiredInteger("Lesson Id")]
        public int LessonId { get; set; }

        [RequiredField("AttachmentUrl")]
        [MaxStringLength(2048)]
        public string AttachmentUrl { get; set; } = string.Empty;

        [MaxStringLength(100)]
        public string AttachmentName { get; set; } = string.Empty;

        [MaxStringLength(10)]
        public string AttachmentType { get; set; } = string.Empty;

        [MaxStringLength(50)]
        public string AttachmentSize { get; set; } = string.Empty;
    }
}
