using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MindMission.API.Controllers.Base;
using MindMission.API.EmailSettings;
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
        private readonly IMailService MailService;

        public MessageController(IMessageService messageService, MessageMappingService messageMappingService, IMailService _MailService) : base(messageMappingService)
        {
            _messageService = messageService;
            _messageMappingService = messageMappingService;
            MailService = _MailService;
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

        [HttpPost("replay/{id:int}")]
        public async Task<ActionResult> Replay(int id, MailData mailData)
        {
            if (MailService.SendMail(mailData))
            {
                //modify in IsReplayed column to be true
                return Ok("Send");
            }
            return BadRequest();


        }


    }
}
