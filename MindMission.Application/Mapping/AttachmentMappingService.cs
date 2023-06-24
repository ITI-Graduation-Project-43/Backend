using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class AttachmentMappingService : IAttachmentMappingService
    {
        public List<FileDetailsDto> AttachmentToFileDetailsDto(Attachment attachment)
        {
            return new List<FileDetailsDto>()
            {
                new FileDetailsDto()
                {
                    FileName= attachment.Name,
                    FileType= attachment.Type,
                }
            };
        }

        public Attachment MappingDtoToAttachment(AttachmentDto attachmentDto)
        {
            Attachment Attachment = new Attachment()
            {
                Name = attachmentDto.File.FileName,
                LessonId = attachmentDto.LessonId,
                Type = $"{attachmentDto.FileType}",
                CreatedAt = DateTime.Now,
            };
            return Attachment;
        }
    }
}