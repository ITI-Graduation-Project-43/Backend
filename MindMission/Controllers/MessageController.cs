using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseController<Messages, MessageDto, int>
    {
        private readonly IMessageService _messageService;
        private readonly MessageMappingService _messageMappingService;
        public MessageController(IMessageService messageService, MessageMappingService messageMappingService) : base(messageMappingService)
        {
            _messageService = messageService;
            _messageMappingService = messageMappingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscussionDto>>> GetAllMessages([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(_messageService.GetAllAsync, pagination, "Messages");
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<DiscussionDto>> GetMessageById(int Id)
        {
            return await GetEntityResponseWithInclude(() => _messageService.GetByIdAsync(Id), "Message");
        }

        [HttpPost]
        public async Task<ActionResult> AddMessage([FromBody] MessageDto messageDTO)
        {
            return await AddEntityResponse(_messageService.AddAsync, messageDTO, "Message", nameof(GetMessageById));
        }


    }
}
