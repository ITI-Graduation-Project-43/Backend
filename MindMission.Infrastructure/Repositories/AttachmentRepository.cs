using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Enums;
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

        public async Task PostAttachmentAsync(Attachment Attachment)
        {
            _context.Attachments.Add(Attachment);
            await _context.SaveChangesAsync();
        }
    }
}
