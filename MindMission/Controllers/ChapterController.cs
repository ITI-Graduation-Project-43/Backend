using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.CourseChapters;
using MindMission.Application.Exceptions;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : BaseController<Chapter, ChapterDto, int>
    {
        private readonly ICourseService _courseService;
        private readonly IChapterService _chapterService;
        private readonly IChapterLessonsService _chapterLessonsService;


        public ChapterController(ICourseService courseService, IChapterService chapterService, IMappingService<Chapter, ChapterDto> chapterMappingService, IChapterLessonsService chapterLessonsService) :
            base(chapterMappingService)
        {
            _courseService = courseService;
            _chapterService = chapterService ?? throw new ArgumentNullException(nameof(chapterService));
            _chapterLessonsService = chapterLessonsService;
        }
        #region Get

        [HttpGet]
        public async Task<ActionResult<IQueryable<ChapterDto>>> GetAllChapter([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithIncludePagination(
                _chapterService.GetAllAsync,
                _chapterService.GetTotalCountAsync,
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
            return await GetEntitiesResponsePagination(() => _chapterService.GetByCourseIdAsync(courseId, pagination.PageNumber, pagination.PageSize), _chapterService.GetTotalCountAsync, pagination, "Chapters");

        }
        #endregion

        #region Add
        [HttpPost("Chapter")]
        public async Task<ActionResult<ChapterDto>> AddChapter([FromBody] ChapterDto chapterDto)
        {
            return await AddEntityResponse(_chapterService.AddAsync, chapterDto, "Chapter", nameof(GetChapterById));
        }

        [HttpPost("ChapterLesson/{id}")]
        public async Task<ActionResult> AddChaptersLesson(int id, [FromBody] List<CreateChapterDto> chapterDtos)
        {

            try
            {
                var course = await _courseService.GetByIdAsync(id);
                if (course == null)
                {
                    return NotFound(NotFoundResponse("Course"));
                }
                for (int i = 0; i < chapterDtos.Count; i++)
                {
                    chapterDtos[i].CourseId = id;
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(InvalidDataResponse());
                }
                await _chapterLessonsService.AddChapters(chapterDtos);
                return Ok(GenerateResponse(true, "Chapters and lessons added successfully"));
            }
            catch (ApiException)
            {
                return BadRequest(ValidationFailedResponse());
            }
            catch
            {
                return BadRequest(DatabaseErrorResponse("Chapter"));
            }
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