using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.QuestionDtos;
using MindMission.Application.Service.Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : BaseController<Question, QuestionDto, QuestionCreateDto, int>
    {
        private readonly IQuestionService _questionService;


        public QuestionController(IQuestionService questionService,
                                IValidatorService<QuestionCreateDto> _validatorService,
                                IMapper mapper)
                                                                            : base(mapper, _validatorService, "Question", "Questions")
        {
            _questionService = questionService;

        }



        #region GET

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult> GetAllQuestion([FromQuery] PaginationDto pagination)
        {
            return await GetAll(_questionService.GetAllAsync, pagination, question => question.Quiz);
        }
        // GET: api/Question/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuestionById(int id)
        {
            return await GetById(() => _questionService.GetByIdAsync(id, question => question.Quiz));
        }

        #endregion GET

        #region Add
        // POST: api/Question/Add
        [HttpPost]
        public async Task<ActionResult> AddQuestion([FromBody] QuestionCreateDto questionCreateDto)
        {
            return await Create(_questionService.AddAsync, questionCreateDto, nameof(GetQuestionById));
        }

        #endregion Add

        #region Delete
        // DELETE: api/Question/Delete/{id}

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            return await Delete(_questionService.GetByIdAsync, _questionService.DeleteAsync, id);
        }
        // DELETE: api/Question/{id}

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDeleteQuestion(int id)
        {
            return await Delete(_questionService.GetByIdAsync, _questionService.SoftDeleteAsync, id);
        }


        #endregion Delete

        #region Edit Put

        // PUT: api/Question/{id}
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateQuestion(int id, [FromBody] QuestionCreateDto questionUpdateDto)
        {
            return await Update(_questionService.GetByIdAsync, _questionService.UpdateAsync, id, questionUpdateDto);
        }


        // PATCH: api/Question/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchQuestion(int id, [FromBody] JsonPatchDocument<QuestionCreateDto> patchDoc)
        {
            return await Patch(_questionService.GetByIdAsync, _questionService.UpdatePartialAsync, id, patchDoc);
        }

        #endregion Edit Put
    }
}