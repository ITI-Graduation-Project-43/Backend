using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;


namespace MindMission.Application.Validator
{
    /// <summary>
    /// Validator service for validating an Article DTO.
    /// </summary>
    public class ArticleValidatorService : IValidatorService<ArticleCreateDto>
    {
        private readonly ILessonService _lessonService;
        private const string entity = "Lesson";

        public ArticleValidatorService(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<string?> ValidateAsync(ArticleCreateDto articleDto, bool isPost)
        {
            if (articleDto == null)
            {
                return ErrorMessages.InvalidData;
            }
            if (articleDto.LessonId <= 0)
            {
                return string.Format(ErrorMessages.InvalidId, entity);
            }

            if (string.IsNullOrEmpty(articleDto.Content))
            {
                return string.Format(ErrorMessages.Required, "Content");
            }

            if (articleDto.Content?.Length < 100)
            {
                return string.Format(ErrorMessages.LengthBelowMinimum, 100);
            }

            var lesson = await _lessonService.GetByIdAsync(articleDto.LessonId, lesson => lesson.Article!);
            if (lesson == null)
            {
                return string.Format(ErrorMessages.ResourceNotFound, entity);
            }
            else if (lesson.Type != LessonType.Article)
            {
                return string.Format(ErrorMessages.InvalidType, entity, lesson.Type);
            }
            else if (isPost && lesson.Article != null)
            {
                return string.Format(ErrorMessages.Conflict, entity + " Article");
            }

            return null;
        }
    }
}
