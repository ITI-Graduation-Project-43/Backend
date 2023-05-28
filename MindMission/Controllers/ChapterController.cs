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
    public class ChapterController : BaseController<Chapter, ChapterDto,int>
    {
        private readonly IChapterService _chapterService;
        private readonly ChapterMappingService _chapterMappingService;

        public ChapterController(IChapterService chapterService, ChapterMappingService chapterMappingService) :
            base(chapterMappingService)
        {
            _chapterService = chapterService ?? throw new ArgumentNullException(nameof(chapterService));
            _chapterMappingService = chapterMappingService ?? throw new ArgumentNullException(nameof(chapterMappingService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetAllChapter([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_chapterService.GetAllAsync, pagination, "Chapter");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDto>> GetChapterById(int id)
        {
            return await GetEntityResponse(() => _chapterService.GetByIdAsync(id), "Chapter");
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
