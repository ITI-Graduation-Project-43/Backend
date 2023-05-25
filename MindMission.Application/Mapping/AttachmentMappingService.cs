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
        public Attachment MappingDtoToAttachment(AttachmentDto attachmentDto)
        {
            Attachment Attachment = new Attachment()
            {
                FileName = attachmentDto.File.FileName,
                LessonId = attachmentDto.LessonId,
                FileUrl = attachmentDto.File.FileName,
                FileType = $"{attachmentDto.FileType}",
                CreatedAt = DateTime.Now,
            };
            return Attachment;
        }
    }
}
