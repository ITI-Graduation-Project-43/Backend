using Microsoft.AspNetCore.Http;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Services
{
    public interface IAttachmentService
    {
        public Task<Attachment> AddAttachmentAsync(Attachment attachment, IFormFile file, Lesson lesson);

        public Task DownloadAttachmentAsync(Attachment attachment);

        public Task<Lesson> GetAttachmentLessonByIdAsync(int id);

        public Task<Attachment> GetAttachmentByIdAsync(int id);
    }
}