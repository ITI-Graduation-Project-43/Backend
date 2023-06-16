using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping.Post
{
    public class PostQuestionMappingService : IMappingService<Question, PostQuestionDto>
    {
        public async Task<PostQuestionDto> MapEntityToDto(Question postQuestion)
        {
            var questionDto = new PostQuestionDto
            {
                QuestionText = postQuestion.QuestionText,
                ChoiceA = postQuestion.ChoiceA,
                ChoiceB = postQuestion.ChoiceB,
                ChoiceC = postQuestion.ChoiceC,
                ChoiceD = postQuestion.ChoiceD,
                CorrectAnswer = postQuestion.CorrectAnswer
            };

            return questionDto;
        }

        public Question MapDtoToEntity(PostQuestionDto question)
        {
            var postQuestionDto = new Question
            {
                QuestionText = question.QuestionText,
                ChoiceA = question.ChoiceA,
                ChoiceB = question.ChoiceB,
                ChoiceC = question.ChoiceC,
                ChoiceD = question.ChoiceD,
                CorrectAnswer = question.CorrectAnswer
            };

            return postQuestionDto;
        }
    }
}
