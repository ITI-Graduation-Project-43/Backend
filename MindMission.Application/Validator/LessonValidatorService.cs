using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.DTOs.CourseChapters;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;


namespace MindMission.Application.Validator
{
    /// <summary>
    /// Validator service for validating an Lesson DTO.
    /// </summary>
    public class LessonValidatorService : IValidatorService<CreateLessonDto>
    {
        private readonly IChapterService _chapterService;
        private const string entity = "Chapter";

        public LessonValidatorService(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        public async Task<string?> ValidateAsync(CreateLessonDto lessonDto, bool isPost)
        {
            if (lessonDto == null)
            {
                return ErrorMessages.InvalidData;
            }
            if (lessonDto.ChapterId <= 0)
            {
                return string.Format(ErrorMessages.InvalidId, entity);
            }

            if (string.IsNullOrEmpty(lessonDto.Title))
            {
                return string.Format(ErrorMessages.Required, "Title");
            }
            if (string.IsNullOrEmpty(lessonDto.Description))
            {
                return string.Format(ErrorMessages.Required, "Description");
            }
            if (lessonDto.NoOfHours <= 0 || lessonDto.NoOfHours > 6)
            {
                return string.Format(ErrorMessages.RangeValueExceeded, 0.01, 6);
            }
            if (lessonDto.Title?.Length < 5 || lessonDto.Title?.Length > 100)
            {
                return string.Format(ErrorMessages.RangeValueExceeded, 5, 100);
            }
            if (lessonDto.Description?.Length < 10 || lessonDto.Description?.Length > 500)
            {
                return string.Format(ErrorMessages.RangeValueExceeded, 10, 500);
            }

            var chapter = await _chapterService.GetByIdAsync(lessonDto.ChapterId);
            if (chapter == null)
            {
                return string.Format(ErrorMessages.ResourceNotFound, entity);
            }


            return null;
        }
    }
}
