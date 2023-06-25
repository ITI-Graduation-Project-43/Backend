
using MindMission.Application.DTOs.QuizDtos;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;

namespace MindMission.Application.Validator
{
    /// <summary>
    /// Validator service for validating an Quiz DTO.
    /// </summary>
    public class QuizValidatorService : IValidatorService<QuizCreateDto>
    {
        private readonly ILessonService _lessonService;
        private const string entity = "Lesson";

        public QuizValidatorService(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<string?> ValidateAsync(QuizCreateDto quizDto, bool isPost)
        {

            if (quizDto == null)
            {
                return ErrorMessages.InvalidData;
            }
            else
            {
                if (quizDto.LessonId <= 0)
                {
                    return string.Format(ErrorMessages.InvalidId, entity);
                }

                if (quizDto.Questions == null || !quizDto.Questions.Any())
                {
                    return string.Format(ErrorMessages.Required, "Questions");
                }
                else if (quizDto.Questions.Count < 1)
                {
                    return string.Format(ErrorMessages.IncorrectListCount, 1);
                }


                var lesson = await _lessonService.GetByIdAsync(quizDto.LessonId, lesson => lesson.Quiz!);
                if (lesson == null)
                {
                    return string.Format(ErrorMessages.ResourceNotFound, entity);
                }
                else if (lesson.Type != LessonType.Quiz)
                {
                    return string.Format(ErrorMessages.InvalidType, entity, lesson.Type);
                }
                else if (isPost && lesson.Quiz != null)
                {
                    return string.Format(ErrorMessages.Conflict, entity + " Quiz");
                }
            }

            return null;
        }
    }
}
