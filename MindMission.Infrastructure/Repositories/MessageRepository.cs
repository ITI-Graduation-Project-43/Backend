using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Messages, int>, IMessageRepository
    {
        private readonly MindMissionDbContext _context;

        public MessageRepository(MindMissionDbContext context):base(context)
        {
            _context = context;
        }

        public async Task messageReplyed(int id)
        {
            var entity = await _context.Messages.FindAsync(id);
            if (entity != null)
            {
                entity.IsReplied = true;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
