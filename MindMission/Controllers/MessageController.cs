using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using MindMission.API.Controllers.Base;
using MindMission.API.EmailSettings;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
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
            return await GetEntitiesResponseWithIncludePagination(_messageService.GetAllAsync, _messageService.GetTotalCountAsync, pagination, "Messages");
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

        [HttpPost("reply/{id:int}")]
        public async Task<ActionResult> Replay(int id, MailData mailData)
        {
            if (MailService.SendMail(mailData))
            {
               await _messageService.messageReplyed(id);
                return Ok(ResponseObjectFactory.CreateResponseObject(true,"Email has been sent",new List<bool> { true},0));
            }
            return BadRequest();


        }


    }
}
