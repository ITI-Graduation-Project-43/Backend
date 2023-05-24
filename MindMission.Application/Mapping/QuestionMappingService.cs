using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class QuestionMappingService : IMappingService<Question, QuestionDto>
    {
        public Question MapDtoToEntity(QuestionDto questionDto)
        {
            return new Question
            {
                Id = questionDto.Id,
                QuizId = questionDto.QuizId,
                QuestionText = questionDto.QuestionText,
                ChoiceA = questionDto.ChoiceA,
                ChoiceB = questionDto.ChoiceB,
                ChoiceC = questionDto.ChoiceC,
                ChoiceD = questionDto.ChoiceD,
                CorrectAnswer = questionDto.CorrectAnswer,
                CreatedAt = questionDto.CreatedAt,
                UpdatedAt = questionDto.UpdatedAt,
            };
        }

        public async Task<QuestionDto> MapEntityToDto(Question question)
        {
            var questionDto = new QuestionDto
            {
                Id = question.Id,
                QuizId = question.QuizId,
                QuestionText = question.QuestionText,
                ChoiceA = question.ChoiceA,
                ChoiceB = question.ChoiceB,
                ChoiceC = question.ChoiceC,
                ChoiceD = question.ChoiceD,
                CorrectAnswer = question.CorrectAnswer,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
            };
            return questionDto;

        }
    }
}
