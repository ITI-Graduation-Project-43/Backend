using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;


namespace MindMission.Application.Mapping.Post
{
    public class PostQuestionMappingService : IMappingService<PostQuestionDto, QuestionDto>
    {
        public async Task<QuestionDto> MapEntityToDto(PostQuestionDto postQuestion)
        {
            var questionDto = new QuestionDto
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

        public PostQuestionDto MapDtoToEntity(QuestionDto question)
        {
            var postQuestionDto = new PostQuestionDto
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
