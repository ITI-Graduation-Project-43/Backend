using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.DtoValidator;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;
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
        /*  
          [HttpPost("Add")]
          public async Task<ActionResult<ArticleDto>> AddArticle([FromBody] ArticleDto articleDTO)
          {
              return await AddEntityResponse(_articleService.AddAsync, articleDTO, "Article", nameof(GetArticleById));
          }
          // POST: api/Article
          [HttpPost]
          public async Task<IActionResult> AddArticle([FromBody] ArticleCreateDto postArticleDto)
          {

              if (!ModelState.IsValid)
              {
                  return BadRequest(InvalidDataResponse());

              }
              var lesson = await _lessonService.GetByIdAsync(postArticleDto.LessonId);
              if (lesson == null)
              {
                  return BadRequest(NotFoundResponse("Lesson"));
              }
              if (lesson.Type != LessonType.Article)
              {
                  return BadRequest(ValidationFailedResponse());
              }
              // Map the article create DTO to a article entity.
              var articleToCreate = _postArticleMappingService.MapDtoToEntity(postArticleDto);

              // Use the service to add the article to the database.
              var createdArticle = await _articleService.AddAsync(articleToCreate);

              // Map the created article entity back to a DTO.
              var articleDto = await _postArticleMappingService.MapEntityToDto(createdArticle);

              if (articleDto == null)
                  return NotFound(NotFoundResponse("Article"));
              // Return the created article.

              string message = string.Format(SuccessMessages.CreatedSuccessfully, "Article");
              var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<ArticleCreateDto> { articleDto });

              return CreatedAtAction(nameof(GetArticleById), new { id = articleDto.Id }, response);
          }*/
        #endregion Add

        #region Delete
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            return await Delete(_articleService.GetByIdAsync, _articleService.DeleteAsync, id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDeleteArticle(int id)
        {
            return await Delete(_articleService.GetByIdAsync, _articleService.SoftDeleteAsync, id);
        }

        /* // DELETE: api/Article/Delete/{ArticleId}
         [HttpDelete("Delete/{ArticleId}")]
         public async Task<IActionResult> DeleteArticle(int ArticleId)
         {
             return await DeleteEntityResponse(_articleService.GetByIdAsync, _articleService.DeleteAsync, ArticleId);
         }

         // DELETE: api/Course/{id}

         [HttpDelete("{id}")]
         public async Task<IActionResult> Delete(int id)
         {

             var article = await _articleService.GetByIdAsync(id);

             if (article == null)
                 return NotFound(NotFoundResponse("Article"));
             await _articleService.SoftDeleteAsync(id);
             return NoContent();
         }
 */
        #endregion Delete

        #region Edit Put
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateArticle(int id, [FromBody] ArticleCreateDto articleUpdateDto)
        {
            return await Update(_articleService.GetByIdAsync, _articleService.UpdateAsync, id, articleUpdateDto);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchArticle(int id, [FromBody] JsonPatchDocument<ArticleCreateDto> patchDoc)
        {
            return await Patch(_articleService.GetByIdAsync, _articleService.UpdatePartialAsync, id, patchDoc);
        }
        /* // PUT: api/Article/{ArticleId}
         [HttpPut("Put/{ArticleId}")]
         public async Task<ActionResult> UpdateArticle(int ArticleId, ArticleDto articleDto)
         {
             return await UpdateEntityResponse(_articleService.GetByIdAsync, _articleService.UpdateAsync, ArticleId, articleDto, "Article");
         }



         // PUT: api/Article/{id}
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleCreateDto postArticleDto)
         {
             if (!id.Equals(postArticleDto.Id))
             {
                 return BadRequest(IdMismatchResponse("Article"));
             }
             var articleToUpdate = await _articleService.GetByIdAsync(id);
             if (articleToUpdate == null)
             {
                 return NotFound(NotFoundResponse("Article"));
             }

             if (!ModelState.IsValid)
             {
                 return BadRequest(InvalidDataResponse());
             }

             var originalDto = _postArticleMappingService.MapEntityToDto(articleToUpdate);
             if (originalDto.Equals(postArticleDto))
             {
                 return Ok(NoChangesResponse("Article"));
             }

             var lesson = await _lessonService.GetByIdAsync(postArticleDto.LessonId);
             if (lesson == null)
             {
                 return BadRequest(NotFoundResponse("Lesson"));
             }

             // Map the article update DTO to a article entity.
             var updatedArticleEntity = _postArticleMappingService.MapDtoToEntity(postArticleDto);

             // Use the service to update the article in the database.
             var updatedArticle = await _articleService.UpdateAsync(updatedArticleEntity);

             // Map the updated article entity back to a DTO.
             var articleDto = await _postArticleMappingService.MapEntityToDto(updatedArticle);


             string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Article");
             var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<ArticleCreateDto> { articleDto });

             return Ok(response);
         }


         [HttpPatch("{id}")]
         public async Task<IActionResult> PatchCourse(int id, [FromBody] JsonPatchDocument<ArticleCreateDto> patchDoc)
         {
             if (patchDoc != null)
             {
                 var articleInDb = await _articleService.GetByIdAsync(id);

                 if (articleInDb == null)
                 {
                     return NotFound(NotFoundResponse("Article"));
                 }

                 var articleToUpdateDto = await _postArticleMappingService.MapEntityToDto(articleInDb);
                 if (articleToUpdateDto == null)
                 {
                     return BadRequest(BadRequestResponse("Dto cannot be null."));
                 }

                 patchDoc.ApplyTo(articleToUpdateDto, ModelState);

                 if (!ModelState.IsValid)
                 {
                     return BadRequest(InvalidDataResponse());
                 }

                 try
                 {
                     await _articleDtoValidator.ValidateAsync(articleToUpdateDto);
                 }
                 catch (ValidationException ex)
                 {
                     return BadRequest(ex.Message);
                 }

                 articleInDb = _postArticleMappingService.MapDtoToEntity(articleToUpdateDto);

                 var updatedArticleEntity = await _articleService.UpdatePartialAsync(id, articleInDb);

                 var articleDto = await _postArticleMappingService.MapEntityToDto(updatedArticleEntity);

                 string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Article");
                 var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<ArticleCreateDto> { articleDto });

                 return Ok(response);
             }
             else
             {
                 return BadRequest(ModelState);
             }
         }*/
        #endregion Edit Put
    }
}