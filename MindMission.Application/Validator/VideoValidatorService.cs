using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.DTOs.VideoDtos;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Common;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;

namespace MindMission.Application.Validator
{
    /// <summary>
    /// Validator service for validating an Video DTO.
    /// </summary>
    public class VideoValidatorService : IValidatorService<VideoCreateDto>
    {
        private readonly ILessonService _lessonService;
        private const string entity = "Lesson";

        public VideoValidatorService(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<string?> ValidateAsync(VideoCreateDto VideoDto, bool isPost)
        {
            if (VideoDto == null)
            {
                return ErrorMessages.InvalidData;
            }
            else
            {
                if (VideoDto.LessonId <= 0)
                {
                    return string.Format(ErrorMessages.InvalidId, entity);
                }

                if (string.IsNullOrEmpty(VideoDto.VideoUrl))
                {
                    return string.Format(ErrorMessages.Required, "VideoUrl");
                }

                if (VideoDto.VideoUrl?.Length > 2048)
                {
                    return string.Format(ErrorMessages.LengthAboveMaximum, 2048);
                }

                var lesson = await _lessonService.GetByIdAsync(VideoDto.LessonId, lesson => lesson.Video!);
                if (lesson == null)
                {
                    return string.Format(ErrorMessages.ResourceNotFound, entity);
                }
                else if (lesson.Type != LessonType.Video)
                {
                    return string.Format(ErrorMessages.InvalidType, entity, lesson.Type);
                }
                else if (isPost && lesson.Video != null)
                {
                    return string.Format(ErrorMessages.Conflict, entity + " Video");
                }
            }

            return null;
        }
    }
}
