using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public interface IAttachmentMappingService
    {
        public Attachment MappingDtoToAttachment(AttachmentDto attachmentDto);

        public List<FileDetailsDto> AttachmentToFileDetailsDto(Attachment attachment);
    }
}