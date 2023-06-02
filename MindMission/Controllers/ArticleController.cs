using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : BaseController<Article, ArticleDto, int>
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService, ArticleMappingService articleMappingService) : base(articleMappingService)
        {
            _articleService = articleService;
        }

        #region GET

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAllArticle([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_articleService.GetAllAsync, pagination, "Articles");
        }

        // GET: api/Article/{ArticleId}
        [HttpGet("{ArticleId}")]
        public async Task<ActionResult<ArticleDto>> GetArticleById(int ArticleId)
        {
            return await GetEntityResponse(() => _articleService.GetByIdAsync(ArticleId), "Article");
        }

        #endregion GET

        #region Add

        // POST: api/Article
        [HttpPost]
        public async Task<ActionResult<ArticleDto>> AddArticle([FromBody] ArticleDto articleDTO)
        {
            return await AddEntityResponse(_articleService.AddAsync, articleDTO, "Article", nameof(GetArticleById));
        }

        #endregion Add

        #region Delete

        // DELETE: api/Article/{ArticleId}
        [HttpDelete("{ArticleId}")]
        public async Task<IActionResult> DeleteArticle(int ArticleId)
        {
            return await DeleteEntityResponse(_articleService.GetByIdAsync, _articleService.DeleteAsync, ArticleId);
        }

        #endregion Delete

        #region Edit Put

        // PUT: api/Article/{ArticleId}
        [HttpPut("{ArticleId}")]
        public async Task<ActionResult> UpdateArticle(int ArticleId, ArticleDto articleDto)
        {
            return await UpdateEntityResponse(_articleService.GetByIdAsync, _articleService.UpdateAsync, ArticleId, articleDto, "Article");
        }

        #endregion Edit Put
    }
}