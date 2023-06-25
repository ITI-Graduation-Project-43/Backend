using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IAttachmentRepository : IRepository<Attachment, int>
    {
        Task PostAttachmentAsync(Lesson lesson, Attachment attachment);

        public Task<Attachment?> GetAttachmentByIdAsync(int id);
    }
}