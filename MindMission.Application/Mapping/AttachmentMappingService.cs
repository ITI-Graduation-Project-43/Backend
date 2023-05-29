using MindMission.Application.DTOs;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    FileName= attachment.FileName,
                    FileType= attachment.FileType,
                }
            };
        }

        public Attachment MappingDtoToAttachment(AttachmentDto attachmentDto)
        {
            Attachment Attachment = new Attachment()
            {
                FileName = attachmentDto.File.FileName,
                LessonId = attachmentDto.LessonId,
                FileType = $"{attachmentDto.FileType}",
                CreatedAt = DateTime.Now,
            };
            return Attachment;
        }
    }
}
