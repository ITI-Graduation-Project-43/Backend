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
        public MessageService(IMessageRepository context):base(context) 
        {
            
        }
    }
}
