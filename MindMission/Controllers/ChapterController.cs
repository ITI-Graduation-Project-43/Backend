using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
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
        #region Get

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
        #endregion

        #region Add
        [HttpPost("Chapter")]
        public async Task<ActionResult<ChapterDto>> AddChapter([FromBody] ChapterDto chapterDto)
        {
            return await AddEntityResponse(_chapterService.AddAsync, chapterDto, "Chapter", nameof(GetChapterById));
        }
        #endregion

        #region Update
        [HttpPut("{chapterId}")]
        public async Task<ActionResult> UpdateChapter(int chapterId, ChapterDto chapterDto)
        {
            return await UpdateEntityResponse(_chapterService.GetByIdAsync, _chapterService.UpdateAsync, chapterId, chapterDto, "Chapter");
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{chapterId}")]
        public async Task<IActionResult> DeleteChapter(int chapterId)
        {
            return await DeleteEntityResponse(_chapterService.GetByIdAsync, _chapterService.DeleteAsync, chapterId);
        }

        // DELETE: api/Chapter/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var article = await _chapterService.GetByIdAsync(id);

            if (article == null)
                return NotFound(NotFoundResponse("Article"));
            await _chapterService.SoftDeleteAsync(id);
            return NoContent();
        }
        #endregion
    }
}