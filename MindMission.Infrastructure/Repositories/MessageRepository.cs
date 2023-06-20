using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Messages, int>, IMessageRepository
    {
        public MessageRepository(MindMissionDbContext context):base(context)
        {
            
        }
    }
}
