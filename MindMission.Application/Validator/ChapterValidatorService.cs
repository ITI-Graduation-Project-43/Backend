using MindMission.Application.DTOs;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.DTOs.CourseChapters;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;


namespace MindMission.Application.Validator
{
    /// <summary>
    /// Validator service for validating an Chapter DTO.
    /// </summary>
    public class ChapterValidatorService : IValidatorService<CreateChapterDto>
    {
        private readonly ICourseService _courseService;
        private const string entity = "Course";

        public ChapterValidatorService(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<string?> ValidateAsync(CreateChapterDto chapterDto, bool isPost)
        {
            if (chapterDto == null)
            {
                return ErrorMessages.InvalidData;
            }
            /*if (chapterDto.CourseId <= 0)
            {
                return string.Format(ErrorMessages.InvalidId, entity);
            }*/

            if (string.IsNullOrEmpty(chapterDto.Title))
            {
                return string.Format(ErrorMessages.Required, "Title");
            }

            if (chapterDto.Title?.Length < 5 || chapterDto.Title?.Length > 100)
            {
                return string.Format(ErrorMessages.RangeValueExceeded, 5, 100);
            }

            /*var course = await _courseService.GetByIdAsync(chapterDto.CourseId);
            if (course == null)
            {
                return string.Format(ErrorMessages.ResourceNotFound, entity);
            }*/


            return null;
        }
    }
}
