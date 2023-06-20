using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.QuizDtos;
using MindMission.Application.Service.Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : BaseController<Quiz, QuizDto, QuizCreateDto, int>
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService,
                              IValidatorService<QuizCreateDto> _validatorService,
                              IMapper mapper)
                                                                       : base(mapper, _validatorService, "Quiz", "Quizzes")
        {
            _quizService = quizService;

        }

        #region GET

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult> GetAllQuiz([FromQuery] PaginationDto pagination)
        {
            return await GetAll(_quizService.GetAllAsync, pagination, quiz => quiz.Lesson, Quiz => Quiz.Questions);
        }
        // GET: api/Quiz/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuizById(int id)
        {
            return await GetById(() => _quizService.GetByIdAsync(id, quiz => quiz.Lesson, Quiz => Quiz.Questions));
        }

        #endregion GET

        #region Add
        // POST: api/Quiz/Add
        [HttpPost]
        public async Task<ActionResult> AddQuiz([FromBody] QuizCreateDto quizCreateDto)
        {
            return await Create(_quizService.AddAsync, quizCreateDto, nameof(GetQuizById));
        }

        #endregion Add

        #region Delete
        // DELETE: api/Quiz/Delete/{id}

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            return await Delete(_quizService.GetByIdAsync, _quizService.DeleteAsync, id);
        }
        // DELETE: api/Quiz/{id}

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDeleteQuiz(int id)
        {
            return await Delete(_quizService.GetByIdAsync, _quizService.SoftDeleteAsync, id);
        }


        #endregion Delete

        #region Edit Put
        // PUT: api/Quiz/{id}

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateQuiz(int id, [FromBody] QuizCreateDto quizUpdateDto)
        {
            return await Update(_quizService.GetByIdAsync, _quizService.UpdateAsync, id, quizUpdateDto);
        }
        // PATCH: api/Quiz/{id}

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchQuiz(int id, [FromBody] JsonPatchDocument<QuizCreateDto> patchDoc)
        {
            return await Patch(_quizService.GetByIdAsync, _quizService.UpdatePartialAsync, id, patchDoc);
        }

        #endregion Edit Put
    }
}