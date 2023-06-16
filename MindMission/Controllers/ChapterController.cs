using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : BaseController<Chapter, ChapterDto, int>
    {
        private readonly IChapterService _chapterService;
        private readonly IMappingService<Chapter, ChapterDto> _chapterMappingService;

        public ChapterController(IChapterService chapterService, IMappingService<Chapter, ChapterDto> chapterMappingService) :
            base(chapterMappingService)
        {
            _chapterService = chapterService ?? throw new ArgumentNullException(nameof(chapterService));
            _chapterMappingService = chapterMappingService ?? throw new ArgumentNullException(nameof(chapterMappingService));
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<ChapterDto>>> GetAllChapter([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(
                _chapterService.GetAllAsync,
                pagination,
                "Chapters",
                chapter => chapter.Lessons
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDto>> GetChapterById(int id)
        {
            return await GetEntityResponseWithInclude(
                  () => _chapterService.GetByIdAsync(id,
                    chapter => chapter.Lessons
                  ),
                  "Chapter"
              );
        }

        [HttpGet("byCourse/{courseId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByCourseIdAsync(int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _chapterService.GetByCourseIdAsync(courseId), pagination, "Chapters");
        }

        [HttpPost("Chapter")]
        public async Task<ActionResult<ChapterDto>> AddChapter([FromBody] ChapterDto chapterDto)
        {
            return await AddEntityResponse(_chapterService.AddAsync, chapterDto, "Chapter", nameof(GetChapterById));
        }

        [HttpPut("{chapterId}")]
        public async Task<ActionResult> UpdateChapter(int chapterId, ChapterDto chapterDto)
        {
            return await UpdateEntityResponse(_chapterService.GetByIdAsync, _chapterService.UpdateAsync, chapterId, chapterDto, "Chapter");
        }

        [HttpDelete("{chapterId}")]
        public async Task<IActionResult> DeleteChapter(int chapterId)
        {
            return await DeleteEntityResponse(_chapterService.GetByIdAsync, _chapterService.DeleteAsync, chapterId);
        }
    }
}