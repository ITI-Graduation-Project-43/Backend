using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Common;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;


namespace MindMission.Application.Validator
{
    public class ArticleValidatorService : IValidatorService<ArticleCreateDto>
    {
        private readonly ILessonService _lessonService;

        public ArticleValidatorService(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<ValidationResult> ValidateAsync(ArticleCreateDto articleDto)
        {
            var errors = new List<string>();

            if (articleDto == null)
            {
                errors.Add(ErrorMessages.InvalidData);
            }
            else
            {
                if (articleDto.LessonId <= 0)
                {
                    errors.Add(string.Format(ErrorMessages.InvalidId, "Lesson"));
                }

                if (string.IsNullOrEmpty(articleDto.Content))
                {
                    errors.Add(string.Format(ErrorMessages.Required, "Content"));
                }

                if (articleDto.Content?.Length > 30000)
                {
                    errors.Add(string.Format(ErrorMessages.InvalidContentLength, 30000));
                }

                var lesson = await _lessonService.GetByIdAsync(articleDto.LessonId);
                if (lesson == null)
                {
                    errors.Add(string.Format(ErrorMessages.ResourceNotFound, "Lesson"));
                }
                else if (lesson.Type != LessonType.Article)
                {
                    errors.Add(string.Format(ErrorMessages.InvalidType, "Lesson", LessonType.Article));
                }
            }

            return new ValidationResult
            {
                IsValid = !errors.Any(),
                Errors = errors
            };
        }
    }
}
