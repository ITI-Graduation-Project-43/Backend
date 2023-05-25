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

        public async Task<Attachment> AddAttachmentAsync(Attachment attachment, IFormFile file, Lesson lesson)
        {
            using (var Stream = new MemoryStream())
            {
                file.CopyTo(Stream);
                attachment.FileData = Stream.ToArray();
            }

            await _context.PostAttachmentAsync(lesson, attachment);
            return attachment;
        }

        public async Task DownloadAttachmentAsync(int id)
        {
            Attachment? attachment = await _context.GetAttachmentAsync(id);
            if (attachment != null)
            {
                var FileContent = new MemoryStream(attachment.FileData);
                var FilePath = Path.Combine(Directory.GetCurrentDirectory(),attachment.FileName);

                using(var Stream = new FileStream(FilePath,FileMode.Create, FileAccess.Write))
                {
                    FileContent.CopyTo(Stream);
                }
            }
        }

        public async Task<Lesson> GetAttachmentLesson(int id)
        {
            Lesson Lesson = await _lessonRepository.GetByIdAsync(id);
            return Lesson;
        }

    }
}
