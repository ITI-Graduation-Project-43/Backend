using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IAttachmentRepository
    {
        Task PostAttachmentAsync(Lesson lesson, Attachment attachment);

        public Task<Attachment?> GetAttachmentByIdAsync(int id);
    }
}