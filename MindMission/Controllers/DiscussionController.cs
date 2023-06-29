using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Responses;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.SignalR;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : BaseController<Discussion, DiscussionDto, int>
    {
        private readonly IDiscussionService _discussionService;
        private readonly DiscussionMappingService _discussionMappingService;
        private readonly IHubContext<DiscussionHub> _discussionHubContext;

        public DiscussionController(IDiscussionService discussionService, DiscussionMappingService discussionMappingService, IHubContext<DiscussionHub> discussionHubContext) : base(discussionMappingService)
        {
            _discussionService = discussionService;
            _discussionMappingService = discussionMappingService;
            _discussionHubContext = discussionHubContext;

        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscussionDto>>> GetAllDiscussion([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _discussionService.GetAllAsync(pagination.PageNumber, pagination.PageSize), _discussionService.GetTotalCountAsync, pagination, "Discussions");
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DiscussionDto>> GetDiscussionById(int Id)
        {
            return await GetEntityResponseWithInclude(() => _discussionService.GetByIdAsync(Id, d => d.ParentDiscussion), "Discussion");
        }

        [HttpGet("Parent/{parentId}")]

        public async Task<ActionResult<Discussion>> GetDiscussionByParentId(int parentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _discussionService.GetAllDiscussionByParentIdAsync(parentId, pagination.PageNumber, pagination.PageSize), _discussionService.GetTotalCountAsync, pagination, "Discussion");
        }

        [HttpGet("Lesson/{lessonId}")]
        public async Task<ActionResult<Discussion>> GetDiscussionByLessonId(int lessonId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _discussionService.GetAllDiscussionByLessonIdAsync(lessonId, pagination.PageNumber, pagination.PageSize), _discussionService.GetTotalCountAsync, pagination, "Discussion");
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{DisussionId}")]
        public async Task<ActionResult> DeleteDiscussion(int DisussionId)
        {
            return await DeleteEntityResponse(_discussionService.GetByIdAsync, _discussionService.DeleteAsync, DisussionId);
        }

        // DELETE: api/Discussion/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var article = await _discussionService.GetByIdAsync(id);

            if (article == null)
                return NotFound(NotFoundResponse("Article"));
            await _discussionService.SoftDeleteAsync(id);
            return NoContent();
        }
        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult> AddDiscussion([FromBody] DiscussionDto discussionDTO)
        {
            var result = await AddEntityResponse(_discussionService.AddAsync, discussionDTO, "Discussion", nameof(GetDiscussionById));

            if (result is CreatedAtActionResult)
            {
                var responseObject = (ResponseObject<DiscussionDto>)((CreatedAtActionResult)result).Value;
                var createdDto = responseObject.Items.First();

                await _discussionHubContext.Clients.All.SendAsync("ReceiveComment", createdDto);
            }

            return result;
        }
        #endregion


        #region Update
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateDiscussion(int Id, [FromBody] DiscussionDto discussionDTO)
        {
            return await UpdateEntityResponse(_discussionService.GetByIdAsync, _discussionService.UpdateAsync, Id, discussionDTO, "Disuccion");
        }
        #endregion

    }
}