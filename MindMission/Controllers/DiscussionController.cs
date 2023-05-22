using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly IDiscussionService DiscussionService;

        public DiscussionController(IDiscussionService _DiscussionService)
        {
            DiscussionService = _DiscussionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permission = await DiscussionService.GetAllAsync();

            ResponseObject<Discussion> AllDiscussion = new ResponseObject<Discussion>();
            AllDiscussion.ReturnedResponse(true, "All Discussions", permission, 3, 10, permission.Count());

            return Ok(AllDiscussion);
        }
    }
}
