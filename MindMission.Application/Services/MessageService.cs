using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services
{
    public class MessageService : Service<Messages, int>, IMessageService
    {
        private readonly IMessageRepository _context;
        public MessageService(IMessageRepository context):base(context) 
        {
            _context = context;

        }

        public async Task messageReplyed(int id)
        {
              await _context.messageReplyed(id);
        }
    }
}
