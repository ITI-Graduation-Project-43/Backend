using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : BaseController<Article, ArticleDto, ArticleCreateDto, int>
    {
        private readonly IArticleService _articleService;


        public ArticleController(IArticleService articleService,
                                IValidatorService<ArticleCreateDto> _validatorService,
                                IMapper mapper)
                                                                            : base(mapper, _validatorService, "Article", "Articles")
        {
            _articleService = articleService;

        }



        #region GET

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult> GetAllArticle([FromQuery] PaginationDto pagination)
        {
            return await GetAll(_articleService.GetAllAsync, pagination, article => article.Lesson);
        }
        // GET: api/Article/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetArticleById(int id)
        {
            return await GetById(() => _articleService.GetByIdAsync(id, article => article.Lesson));
        }

        #endregion GET

        #region Add
        // POST: api/Article/Add
        [HttpPost]
        public async Task<ActionResult> AddArticle([FromBody] ArticleCreateDto articleCreateDto)
        {
            return await Create(_articleService.AddAsync, articleCreateDto, nameof(GetArticleById));
        }

        #endregion Add

        #region Delete
        // DELETE: api/Article/Delete/{id}

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            return await Delete(_articleService.GetByIdAsync, _articleService.DeleteAsync, id);
        }
        // DELETE: api/Article/{id}

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDeleteArticle(int id)
        {
            return await Delete(_articleService.GetByIdAsync, _articleService.SoftDeleteAsync, id);
        }


        #endregion Delete

        #region Edit Put

        // PUT: api/Article/{id}
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateArticle(int id, [FromBody] ArticleCreateDto articleUpdateDto)
        {
            return await Update(_articleService.GetByIdAsync, _articleService.UpdateAsync, id, articleUpdateDto);
        }


        // PATCH: api/Article/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchArticle(int id, [FromBody] JsonPatchDocument<ArticleCreateDto> patchDoc)
        {
            return await Patch(_articleService.GetByIdAsync, _articleService.UpdatePartialAsync, id, patchDoc);
        }

        #endregion Edit Put
    }
}