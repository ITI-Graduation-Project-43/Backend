using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Patch;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using Stripe;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : BaseController<Lesson, LessonDto, int>
    {
        private readonly ILessonService _lessonService;
        private readonly IChapterService _chapterService;
        private readonly IMappingService<Lesson, PostArticleLessonDto> _postArticleLessonMappingService;
        private readonly IMappingService<Lesson, PostQuizLessonDto> _postQuizLessonMappingService;
        private readonly IMappingService<Lesson, PostVideoLessonDto> _postVideoLessonMappingService;
        private readonly IArticleLessonPatchValidator _articleLessonPatchValidator;
        private readonly IQuizLessonPatchValidator _quizLessonPatchValidator;
        private readonly IVideoLessonPatchValidator _videoLessonPatchValidator;


        public LessonController(ILessonService lessonService,
                                IChapterService chapterService,
                               IMappingService<Lesson, LessonDto> lessonMappingService,
                               IMappingService<Lesson, PostArticleLessonDto> postArticleLessonMappingService,
                               IMappingService<Lesson, PostQuizLessonDto> postQuizLessonMappingService,
                               IMappingService<Lesson, PostVideoLessonDto> postVideoLessonMappingService,
                               IArticleLessonPatchValidator articleLessonPatchValidator,
                               IQuizLessonPatchValidator quizLessonPatchValidator,
                               IVideoLessonPatchValidator videoLessonPatchValidator) :
            base(lessonMappingService)
        {
            _lessonService = lessonService ?? throw new ArgumentNullException(nameof(lessonService));
            _chapterService = chapterService ?? throw new ArgumentNullException(nameof(chapterService));
            _postArticleLessonMappingService = postArticleLessonMappingService ?? throw new ArgumentNullException(nameof(postArticleLessonMappingService));
            _postQuizLessonMappingService = postQuizLessonMappingService ?? throw new ArgumentNullException(nameof(postQuizLessonMappingService));
            _postVideoLessonMappingService = postVideoLessonMappingService ?? throw new ArgumentNullException(nameof(postVideoLessonMappingService));
            _articleLessonPatchValidator = articleLessonPatchValidator ?? throw new ArgumentNullException(nameof(articleLessonPatchValidator));
            _quizLessonPatchValidator = quizLessonPatchValidator ?? throw new ArgumentNullException(nameof(quizLessonPatchValidator));
            _videoLessonPatchValidator = videoLessonPatchValidator ?? throw new ArgumentNullException(nameof(videoLessonPatchValidator));
        }
        #region Get
        [HttpGet]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetAllLesson([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(
                _lessonService.GetAllAsync,
                pagination,
                "Lessons",
                lesson => lesson.Chapter,
                lesson => lesson.Chapter.Course
            );
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomLessonDto>> GetLesson(int id)
        {
            var lesson = await _lessonService.GetByLessonIdAsync(id);

            if (lesson == null)
            {
                return NotFound(NotFoundResponse("Lesson"));
            }

            var lessonDto = new CustomLessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Type = lesson.Type
            };

            if (lesson.Article != null && lesson.Type == LessonType.Article)
            {
                lessonDto.Article = new CustomArticleDto
                {
                    Id = lesson.Article.Id,
                    Content = lesson.Article.Content
                };
            }
            else if (lesson.Quiz != null && lesson.Type == LessonType.Quiz)
            {
                lessonDto.Quiz = new CustomQuizDto
                {
                    Id = lesson.Quiz.Id,
                    Questions = lesson.Quiz.Questions.Select(q => new CustomQuestionDto
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        ChoiceA = q.ChoiceA,
                        ChoiceB = q.ChoiceB,
                        ChoiceC = q.ChoiceC,
                        ChoiceD = q.ChoiceD,
                        CorrectAnswer = q.CorrectAnswer
                    }).ToList()
                };
            }
            else if (lesson.Video != null && lesson.Type == LessonType.Video)
            {
                lessonDto.Video = new CustomVideoDto
                {
                    Id = lesson.Video.Id,
                    VideoUrl = lesson.Video.VideoUrl
                };
            }



            var response = ResponseObjectFactory.CreateResponseObject(true, string.Format(SuccessMessages.RetrievedSuccessfully, "Lesson"), new List<CustomLessonDto> { lessonDto });
            return Ok(response);
        }

        [HttpGet("byId/{id}")]
        public async Task<ActionResult<LessonDto>> GetLessonById(int id)
        {
            return await GetEntityResponseWithInclude(
                   () => _lessonService.GetByIdAsync(id,
                       lesson => lesson.Chapter,
                       lesson => lesson.Chapter.Course
                   ),
                   "Lesson"
               );
        }


        [HttpGet("byCourse/{courseId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByCourseIdAsync(int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _lessonService.GetByCourseIdAsync(courseId, pagination.PageNumber, pagination.PageSize), _lessonService.GetTotalCountAsync, pagination, "Lessons");
        }

        [HttpGet("byChapter/{chapterId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByChapterIdAsync(int chapterId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _lessonService.GetByChapterIdAsync(chapterId, pagination.PageNumber, pagination.PageSize), _lessonService.GetTotalCountAsync, pagination, "Lessons");
        }

        [HttpGet("byCourseAndChapter/{courseId}/{chapterId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByCourseAndChapterIdAsync(int courseId, int chapterId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _lessonService.GetByCourseAndChapterIdAsync(courseId, chapterId, pagination.PageNumber, pagination.PageSize), _lessonService.GetTotalCountAsync, pagination, "Lessons");
        }

        [HttpGet("freeByCourse/{courseId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetFreeByCourseIdAsync(int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _lessonService.GetFreeByCourseIdAsync(courseId, pagination.PageNumber, pagination.PageSize), _lessonService.GetTotalCountAsync, pagination, "Lessons");
        }

        [HttpGet("byType/{courseId}/{type}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByTypeAsync(int courseId, LessonType type, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _lessonService.GetByTypeAsync(courseId, type, pagination.PageNumber, pagination.PageSize), _lessonService.GetTotalCountAsync, pagination, "Lessons");
        }

        #endregion
        #region Add
        // POST: api/Lesson

        [HttpPost("Lesson")]
        public async Task<ActionResult<LessonDto>> AddLesson([FromBody] LessonDto lessonDto)
        {
            return await AddEntityResponse(_lessonService.AddAsync, lessonDto, "Lesson", nameof(GetLessonById));
        }

        // POST: api/Article
        [HttpPost("Article")]
        public async Task<IActionResult> PostArticleLesson([FromBody] PostArticleLessonDto postArticleLessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }
            var chapter = await _chapterService.GetByIdAsync(postArticleLessonDto.ChapterId);
            if (chapter == null)
            {
                return BadRequest(NotFoundResponse("Chapter"));
            }

            var lessonToCreate = _postArticleLessonMappingService.MapDtoToEntity(postArticleLessonDto);
            var createdLesson = await _lessonService.AddArticleLessonAsync(lessonToCreate);
            var lessonDto = await _postArticleLessonMappingService.MapEntityToDto(createdLesson);

            if (lessonDto == null)
                return NotFound(NotFoundResponse("Article Lesson"));

            string message = string.Format(SuccessMessages.CreatedSuccessfully, "Article Lesson");
            var response = ResponseObjectFactory.CreateResponseObject<PostArticleLessonDto>(true, message, new List<PostArticleLessonDto> { lessonDto });

            return CreatedAtAction(nameof(GetLessonById), new { id = lessonDto.LessonId }, response);

        }

        // POST: api/Quiz
        [HttpPost("Quiz")]
        public async Task<IActionResult> PostQuizLesson([FromBody] PostQuizLessonDto postQuizLessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }

            var chapter = await _chapterService.GetByIdAsync(postQuizLessonDto.ChapterId);
            if (chapter == null)
            {
                return BadRequest(NotFoundResponse("Chapter"));
            }
            var lessonToCreate = _postQuizLessonMappingService.MapDtoToEntity(postQuizLessonDto);
            var createdLesson = await _lessonService.AddQuizLessonAsync(lessonToCreate);
            var lessonDto = await _postQuizLessonMappingService.MapEntityToDto(createdLesson);

            if (lessonDto == null)
                return NotFound(NotFoundResponse("Quiz Lesson"));

            string message = string.Format(SuccessMessages.CreatedSuccessfully, "Quiz Lesson");
            var response = ResponseObjectFactory.CreateResponseObject<PostQuizLessonDto>(true, message, new List<PostQuizLessonDto> { lessonDto });

            return CreatedAtAction(nameof(GetLessonById), new { id = lessonDto.LessonId }, response);

        }
        // POST: api/Video
        [HttpPost("Video")]
        public async Task<IActionResult> PostVideoLesson([FromBody] PostVideoLessonDto postVideoLessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }
            var chapter = await _chapterService.GetByIdAsync(postVideoLessonDto.ChapterId);
            if (chapter == null)
            {
                return BadRequest(NotFoundResponse("Chapter"));
            }

            var lessonToCreate = _postVideoLessonMappingService.MapDtoToEntity(postVideoLessonDto);
            var createdLesson = await _lessonService.AddVideoLessonAsync(lessonToCreate);
            var lessonDto = await _postVideoLessonMappingService.MapEntityToDto(createdLesson);

            if (lessonDto == null)
                return NotFound(NotFoundResponse("Video Lesson"));

            string message = string.Format(SuccessMessages.CreatedSuccessfully, "Video Lesson");
            var response = ResponseObjectFactory.CreateResponseObject<PostVideoLessonDto>(true, message, new List<PostVideoLessonDto> { lessonDto });

            return CreatedAtAction(nameof(GetLessonById), new { id = lessonDto.LessonId }, response);

        }


        #endregion
        #region Update
        [HttpPut("{lessonId}")]
        public async Task<ActionResult> UpdateLesson(int lessonId, LessonDto lessonDto)
        {
            return await UpdateEntityResponse(_lessonService.GetByIdAsync, _lessonService.UpdateAsync, lessonId, lessonDto, "Lesson");
        }

        [HttpPut("Article/{id}")]
        public async Task<IActionResult> PutArticleLesson(int id, [FromBody] PostArticleLessonDto postArticleLessonDto)
        {
            var existingLesson = await _lessonService.GetByIdAsync(id, lesson => lesson.Article);

            if (existingLesson == null)
            {
                return NotFound(NotFoundResponse("Article Lesson"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }
            var chapter = await _chapterService.GetByIdAsync(postArticleLessonDto.ChapterId);
            if (chapter == null)
            {
                return BadRequest(NotFoundResponse("Chapter"));
            }

            postArticleLessonDto.LessonId = id;

            var lessonToUpdate = _postArticleLessonMappingService.MapDtoToEntity(postArticleLessonDto);

            var updatedLesson = await _lessonService.UpdateArticleLessonAsync(id, lessonToUpdate);

            var lessonDto = await _postArticleLessonMappingService.MapEntityToDto(updatedLesson);

            if (lessonDto == null)
                return NotFound(NotFoundResponse("Article Lesson"));

            string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Article Lesson");
            var response = ResponseObjectFactory.CreateResponseObject<PostArticleLessonDto>(true, message, new List<PostArticleLessonDto> { lessonDto });

            return Ok(response);
        }

        [HttpPut("Quiz/{id}")]
        public async Task<IActionResult> PutQuizLesson(int id, [FromBody] PostQuizLessonDto postQuizLessonDto)
        {

            var existingLesson = await _lessonService.GetByIdAsync(id, lesson => lesson.Quiz);

            if (existingLesson == null)
            {
                return NotFound(NotFoundResponse("Quiz Lesson"));
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }
            var chapter = await _chapterService.GetByIdAsync(postQuizLessonDto.ChapterId);
            if (chapter == null)
            {
                return BadRequest(NotFoundResponse("Chapter"));
            }
            if (postQuizLessonDto.Questions == null || !postQuizLessonDto.Questions.Any() || postQuizLessonDto.Questions.Any(item => string.IsNullOrWhiteSpace(item.QuestionText) || string.IsNullOrWhiteSpace(item.ChoiceA) || string.IsNullOrWhiteSpace(item.ChoiceB) || string.IsNullOrWhiteSpace(item.ChoiceC) || string.IsNullOrWhiteSpace(item.ChoiceD) || string.IsNullOrWhiteSpace(item.CorrectAnswer)))
            {
                return BadRequest(ValidationFailedResponse());
            }
            postQuizLessonDto.LessonId = id;

            var lessonToUpdate = _postQuizLessonMappingService.MapDtoToEntity(postQuizLessonDto);

            var updatedLesson = await _lessonService.UpdateQuizLessonAsync(id, lessonToUpdate);

            var lessonDto = await _postQuizLessonMappingService.MapEntityToDto(updatedLesson);

            if (lessonDto == null)
                return NotFound(NotFoundResponse("Quiz Lesson"));

            string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Quiz Lesson");
            var response = ResponseObjectFactory.CreateResponseObject<PostQuizLessonDto>(true, message, new List<PostQuizLessonDto> { lessonDto });

            return Ok(response);
        }

        [HttpPut("Video/{id}")]
        public async Task<IActionResult> PutVideoLesson(int id, [FromBody] PostVideoLessonDto postVideoLessonDto)
        {

            var existingLesson = await _lessonService.GetByIdAsync(id, lesson => lesson.Video);

            if (existingLesson == null)
            {
                return NotFound(NotFoundResponse("Video Lesson"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }
            var chapter = await _chapterService.GetByIdAsync(postVideoLessonDto.ChapterId);
            if (chapter == null)
            {
                return BadRequest(NotFoundResponse("Chapter"));
            }
            postVideoLessonDto.LessonId = id;
            var lessonToUpdate = _postVideoLessonMappingService.MapDtoToEntity(postVideoLessonDto);

            var updatedLesson = await _lessonService.UpdateVideoLessonAsync(id, lessonToUpdate);

            var lessonDto = await _postVideoLessonMappingService.MapEntityToDto(updatedLesson);

            if (lessonDto == null)
                return NotFound(NotFoundResponse("Video Lesson"));

            string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Video Lesson");
            var response = ResponseObjectFactory.CreateResponseObject<PostVideoLessonDto>(true, message, new List<PostVideoLessonDto> { lessonDto });

            return Ok(response);
        }



        #endregion
        #region Patch

        [HttpPatch("Article/{id}")]
        public async Task<IActionResult> PatchArticleLesson(int id, [FromBody] JsonPatchDocument<PostArticleLessonDto> patchDoc)
        {
            if (patchDoc != null)
            {
                var lessonInDb = await _lessonService.GetByIdAsync(id, lesson => lesson.Article);

                if (lessonInDb == null)
                {
                    return NotFound(NotFoundResponse("Lesson"));
                }


                var lessonToUpdateDto = await _postArticleLessonMappingService.MapEntityToDto(lessonInDb);
                if (lessonToUpdateDto == null)
                {
                    return BadRequest(BadRequestResponse("Dto cannot be null."));
                }

                try
                {
                    await _articleLessonPatchValidator.ValidatePatchAsync(patchDoc);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Message);
                }



                patchDoc.ApplyTo(lessonToUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(InvalidDataResponse());
                }

                lessonInDb = _postArticleLessonMappingService.MapDtoToEntity(lessonToUpdateDto);

                var updatedLessonEntity = await _lessonService.UpdateLessonPartialAsync(id, lessonInDb);

                var lessonDto = await _postArticleLessonMappingService.MapEntityToDto(updatedLessonEntity);


                string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Course");
                var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostArticleLessonDto> { lessonDto });

                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPatch("Video/{id}")]
        public async Task<IActionResult> PatchVideoLesson(int id, [FromBody] JsonPatchDocument<PostVideoLessonDto> patchDoc)
        {
            if (patchDoc != null)
            {
                var lessonInDb = await _lessonService.GetByIdAsync(id, lesson => lesson.Video);

                if (lessonInDb == null)
                {
                    return NotFound(NotFoundResponse("Lesson"));
                }


                var lessonToUpdateDto = await _postVideoLessonMappingService.MapEntityToDto(lessonInDb);
                if (lessonToUpdateDto == null)
                {
                    return BadRequest(BadRequestResponse("Dto cannot be null."));
                }

                try
                {
                    await _videoLessonPatchValidator.ValidatePatchAsync(patchDoc);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Message);
                }



                patchDoc.ApplyTo(lessonToUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(InvalidDataResponse());
                }

                lessonInDb = _postVideoLessonMappingService.MapDtoToEntity(lessonToUpdateDto);

                var updatedLessonEntity = await _lessonService.UpdateLessonPartialAsync(id, lessonInDb);

                var lessonDto = await _postVideoLessonMappingService.MapEntityToDto(updatedLessonEntity);


                string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Course");
                var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostVideoLessonDto> { lessonDto });

                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPatch("Quiz/{id}")]
        public async Task<IActionResult> PatchQuizLesson(int id, [FromBody] JsonPatchDocument<PostQuizLessonDto> patchDoc)
        {
            if (patchDoc != null)
            {
                var lessonInDb = await _lessonService.GetByIdAsync(id, lesson => lesson.Article);

                if (lessonInDb == null)
                {
                    return NotFound(NotFoundResponse("Lesson"));
                }


                var lessonToUpdateDto = await _postQuizLessonMappingService.MapEntityToDto(lessonInDb);
                if (lessonToUpdateDto == null)
                {
                    return BadRequest(BadRequestResponse("Dto cannot be null."));
                }

                try
                {
                    await _quizLessonPatchValidator.ValidatePatchAsync(patchDoc);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Message);
                }



                patchDoc.ApplyTo(lessonToUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(InvalidDataResponse());
                }

                lessonInDb = _postQuizLessonMappingService.MapDtoToEntity(lessonToUpdateDto);

                var updatedLessonEntity = await _lessonService.UpdateLessonPartialAsync(id, lessonInDb);

                var lessonDto = await _postQuizLessonMappingService.MapEntityToDto(updatedLessonEntity);


                string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Course");
                var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostQuizLessonDto> { lessonDto });

                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion
        #region Delete

        [HttpDelete("Delete/{lessonId}")]
        public async Task<IActionResult> DeleteLesson(int lessonId)
        {
            return await DeleteEntityResponse(_lessonService.GetByIdAsync, _lessonService.DeleteAsync, lessonId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var lesson = await _lessonService.GetByIdAsync(id);

            if (lesson == null)
                return NotFound(NotFoundResponse("Lesson"));
            await _lessonService.SoftDeleteAsync(id);
            return NoContent();
        }
        #endregion
    }
}