

using MindMission.Application.DTOs.QuestionDtos;
using MindMission.Application.Service.Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Common;
using MindMission.Domain.Constants;


namespace MindMission.Application.Validator
{

    /// <summary>
    /// Validator service for validating an Question DTO.
    /// </summary>
    public class QuestionValidatorService : IValidatorService<QuestionCreateDto>
    {
        private readonly IQuizService _quizService;

        public QuestionValidatorService(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public async Task<string?> ValidateAsync(QuestionCreateDto questionDto, bool isPost)
        {


            if (questionDto == null)
            {
                return ErrorMessages.InvalidData;
            }
            else
            {
                if (questionDto.QuizId <= 0)
                {
                    return string.Format(ErrorMessages.InvalidId, "Quiz");
                }

                var quiz = await _quizService.GetByIdAsync(questionDto.QuizId);
                if (quiz == null)
                {
                    return string.Format(ErrorMessages.ResourceNotFound, "Quiz");
                }
            }

            return null;
        }


    }
}