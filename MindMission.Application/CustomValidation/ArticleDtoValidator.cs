using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Interfaces.DtoValidator;
using MindMission.Application.Service_Interfaces;

namespace MindMission.Application.CustomValidation
{
    public class ArticleDtoValidator : IArticleDtoValidator
    {
        private readonly ILessonService _lessonService;

        public ArticleDtoValidator(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task ValidateAsync(ArticleCreateDto dto)
        {
            if (dto.LessonId <= 0 || await _lessonService.GetByIdAsync(dto.LessonId) == null)
                throw new ValidationException("Invalid LessonId");

            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ValidationException("Invalid Content");

            if (dto.Content.Length > 30000)
                throw new ValidationException("Content cannot exceed 30000 characters");

            if (dto.Content.Length < 100)
                throw new ValidationException("Content cannot be less than 100 characters");
        }
    }

}
