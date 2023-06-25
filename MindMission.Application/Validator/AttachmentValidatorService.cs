using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.DTOs.AttachmentDtos;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;


namespace MindMission.Application.Validator
{
    /// <summary>
    /// Validator service for validating an Attachment DTO.
    /// </summary>
    public class AttachmentValidatorService : IValidatorService<AttachmentCreateDto>
    {
        private readonly ILessonService _lessonService;
        private const string entity = "Lesson";

        public AttachmentValidatorService(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<string?> ValidateAsync(AttachmentCreateDto attachmentDto, bool isPost)
        {
            if (attachmentDto == null)
            {
                return ErrorMessages.InvalidData;
            }
            if (attachmentDto.LessonId <= 0)
            {
                return string.Format(ErrorMessages.InvalidId, entity);
            }

            if (string.IsNullOrEmpty(attachmentDto.AttachmentName))
            {
                return string.Format(ErrorMessages.Required, "Attachment Name");
            }
            if (string.IsNullOrEmpty(attachmentDto.AttachmentUrl))
            {
                return string.Format(ErrorMessages.Required, "Attachment Url");
            }
            if (string.IsNullOrEmpty(attachmentDto.AttachmentType))
            {
                return string.Format(ErrorMessages.Required, "Attachment Type");
            }
            if (string.IsNullOrEmpty(attachmentDto.AttachmentSize))
            {
                return string.Format(ErrorMessages.Required, "Attachment Size");
            }


            if (attachmentDto.AttachmentName?.Length > 100 || attachmentDto.AttachmentName?.Length < 5)
            {
                return string.Format(ErrorMessages.LengthOutOfRange, 5, 100);
            }
            if (attachmentDto.AttachmentSize?.Length > 50 || attachmentDto.AttachmentSize?.Length < 2)
            {
                return string.Format(ErrorMessages.LengthOutOfRange, 2, 50);
            }
            if (attachmentDto.AttachmentType?.Length > 10 || attachmentDto.AttachmentType?.Length < 2)
            {
                return string.Format(ErrorMessages.LengthOutOfRange, 2, 10);
            }
            if (attachmentDto.AttachmentUrl?.Length > 2048 || attachmentDto.AttachmentUrl?.Length < 5)
            {
                return string.Format(ErrorMessages.LengthOutOfRange, 5, 2048);
            }
            var lesson = await _lessonService.GetByIdAsync(attachmentDto.LessonId);
            if (lesson == null)
            {
                return string.Format(ErrorMessages.ResourceNotFound, entity);
            }


            return null;
        }
    }
}
