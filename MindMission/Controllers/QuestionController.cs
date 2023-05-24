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
    public class QuestionController : BaseController<Question, QuestionDto>
    {
        private readonly IQuestionService _questionService;
        private readonly QuestionMappingService _questionMappingService;

        public QuestionController(IQuestionService questionService, QuestionMappingService questionMappingService) :
            base(questionMappingService)
        {
            _questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
            _questionMappingService = questionMappingService ?? throw new ArgumentNullException(nameof(questionMappingService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllQuestions([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_questionService.GetAllAsync, pagination, "Questions");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(int id)
        {
            return await GetEntityResponse(() => _questionService.GetByIdAsync(id), "Question");
        }


        [HttpPost("question")]
        public async Task<ActionResult<QuestionDto>> AddQuestion([FromBody] QuestionDto questionDto)
        {
            return await AddEntityResponse(_questionService.AddAsync, questionDto, "Question", nameof(GetQuestionById));
        }


        [HttpPut("{questionId}")]
        public async Task<ActionResult> UpdateQuestion(int questionId, QuestionDto questionDto)
        {
            return await UpdateEntityResponse(_questionService.GetByIdAsync, _questionService.UpdateAsync, questionId, questionDto, "Question");
        }

        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            return await DeleteEntityResponse(_questionService.GetByIdAsync, _questionService.DeleteAsync, questionId);
        }
    }


}
