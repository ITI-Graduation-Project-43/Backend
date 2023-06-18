using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly MindMissionDbContext _context;

        public AttachmentRepository(MindMissionDbContext context)
        {
            _context = context;
        }

        public async Task PostAttachmentAsync(Lesson lesson, Attachment attachment)
        {
            lesson.Attachment = attachment;
            await _context.SaveChangesAsync();
        }

        public async Task<Attachment?> GetAttachmentByIdAsync(int id)
        {
            return await _context.Attachments
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();
        }
    }
}