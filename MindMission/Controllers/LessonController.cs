using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : BaseController<Lesson, LessonDto, int>
    {
        private readonly ILessonService _lessonService;
        private readonly LessonMappingService _lessonMappingService;

        public LessonController(ILessonService lessonService, LessonMappingService lessonMappingService) :
            base(lessonMappingService)
        {
            _lessonService = lessonService ?? throw new ArgumentNullException(nameof(lessonService));
            _lessonMappingService = lessonMappingService ?? throw new ArgumentNullException(nameof(lessonMappingService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAllLesson([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_lessonService.GetAllAsync, pagination, "Lesson");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLessonById(int id)
        {
            return await GetEntityResponse(() => _lessonService.GetByIdAsync(id), "Lesson");
        }

        [HttpPost("Lesson")]
        public async Task<ActionResult<LessonDto>> AddLesson([FromBody] LessonDto lessonDto)
        {
            return await AddEntityResponse(_lessonService.AddAsync, lessonDto, "Lesson", nameof(GetLessonById));
        }

        [HttpPut("{lessonId}")]
        public async Task<ActionResult> UpdateLesson(int lessonId, LessonDto lessonDto)
        {
            return await UpdateEntityResponse(_lessonService.GetByIdAsync, _lessonService.UpdateAsync, lessonId, lessonDto, "Lesson");
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteLesson(int lessonId)
        {
            return await DeleteEntityResponse(_lessonService.GetByIdAsync, _lessonService.DeleteAsync, lessonId);
        }
    }
}