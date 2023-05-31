using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : BaseController<Discussion, DiscussionDto, int>
    {
        private readonly IDiscussionService _discussionService;
        private readonly DiscussionMappingService _discussionMappingService;
        public DiscussionController(IDiscussionService discussionService, DiscussionMappingService discussionMappingService) : base(discussionMappingService)
        {
            _discussionService = discussionService;
            _discussionMappingService = discussionMappingService;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscussionDto>>> GetAllDiscussion([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(_discussionService.GetAllAsync, pagination, "Discussions", d => d.ParentDiscussion);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DiscussionDto>> GetDiscussionById(int Id)
        {
            return await GetEntityResponseWithInclude(() => _discussionService.GetByIdAsync(Id, d => d.ParentDiscussion), "Discussion");
        }

        [HttpGet("Parent/{parentId}")]

        public async Task<ActionResult<Discussion>> GetDiscussionByParentId(int parentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _discussionService.GetAllDiscussionByParentIdAsync(parentId), pagination, "Discussion");
        }

        [HttpGet("Lesson/{lessonId}")]
        public async Task<ActionResult<Discussion>> GetDiscussionByLessonId(int lessonId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _discussionService.GetAllDiscussionByLessonIdAsync(lessonId), pagination, "Discussion");
        }
        #endregion

        #region Delete
        [HttpDelete("{DisussionId}")]
        public async Task<ActionResult> DeleteDiscussion(int DisussionId)
        {
            return await DeleteEntityResponse(_discussionService.GetByIdAsync, _discussionService.DeleteAsync, DisussionId);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> AddDiscussion([FromBody] DiscussionDto discussionDTO)
        {
            return await AddEntityResponse(_discussionService.AddAsync, discussionDTO,"Discussion", nameof(GetDiscussionById));
        }
        #endregion


        #region Update
        [HttpPut("{Id}")]
        public async Task<ActionResult>UpdateDiscussion(int Id, [FromBody] DiscussionDto discussionDTO)
        {
            return await UpdateEntityResponse(_discussionService.GetByIdAsync, _discussionService.UpdateAsync, Id, discussionDTO, "Disuccion");
        }
        #endregion

    }
}
