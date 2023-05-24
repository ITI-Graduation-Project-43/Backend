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
    public class QuizController : BaseController<Quiz, QuizDto>
    {
        private readonly IQuizService _quizService;
        private readonly QuizMappingService _quizMappingService;

        public QuizController(IQuizService quizService, QuizMappingService quizMappingService) :
            base(quizMappingService)
        {
            _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
            _quizMappingService = quizMappingService ?? throw new ArgumentNullException(nameof(quizMappingService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetAllQuiz([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_quizService.GetAllAsync, pagination, "Quiz");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> GetQuizById(int id)
        {
            return await GetEntityResponse(() => _quizService.GetByIdAsync(id), "Quiz");
        }


        [HttpPost("Quiz")]
        public async Task<ActionResult<QuizDto>> AddQuiz([FromBody] QuizDto quizDto)
        {
            return await AddEntityResponse(_quizService.AddAsync, quizDto, "Quiz", nameof(GetQuizById));
        }


        [HttpPut("{quizId}")]
        public async Task<ActionResult> UpdateQuiz(int quizId, QuizDto quizDto)
        {
            return await UpdateEntityResponse(_quizService.GetByIdAsync, _quizService.UpdateAsync, quizId, quizDto, "Quiz");
        }

        [HttpDelete("{quizId}")]
        public async Task<IActionResult> DeleteQuiz(int quizId)
        {
            return await DeleteEntityResponse(_quizService.GetByIdAsync, _quizService.DeleteAsync, quizId);
        }
    }


}
