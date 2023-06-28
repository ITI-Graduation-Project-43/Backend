using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public class MessageMappingService : IMappingService<Messages, MessageDto>
    {
        public MessageMappingService()
        {
            
        }
        public Messages MapDtoToEntity(MessageDto dto)
        {
            return new Messages { Id=dto.Id,Name=dto.Name,Email=dto.Email,Message=dto.Message};
        }

        public async Task<MessageDto> MapEntityToDto(Messages entity)
        {
            var MessageDTO =new MessageDto { Id=entity.Id,Name=entity.Name,Email=entity.Email,Message=entity.Message,IsReplyed=entity.IsReplyed};
            return MessageDTO;
        }
    }
}
