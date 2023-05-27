using Microsoft.AspNetCore.Http;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _context;
        private readonly ILessonRepository _lessonRepository;

        public AttachmentService(IAttachmentRepository context, ILessonRepository lessonRepository)
        {
            _context = context;
            _lessonRepository = lessonRepository;
        }

        public async Task<Attachment> AddAttachmentAsync(AttachmentDto attachmentDto)
        {
            Attachment Attachment = new Attachment()
            {
                FileName = attachmentDto.File.FileName,
                LessonId = attachmentDto.LessonId,
                FileType = $"{attachmentDto.FileType}",
                CreatedAt = DateTime.Now,
            };

            using (var Stream = new MemoryStream())
            {
                attachmentDto.File.CopyTo(Stream);
                Attachment.FileData = Stream.ToArray();
            }

            await _context.PostAttachmentAsync(Attachment);
            return Attachment;
        }

        public async Task<bool> IsExistedLesson(int id)
        {
            Lesson Lesson = await _lessonRepository.GetByIdAsync(id);
            return (Lesson != null);
        }
    }
}
