using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class QuizMappingService : IMappingService<Quiz, QuizDto>
    {
        public Quiz MapDtoToEntity(QuizDto quizDto)
        {
            return new Quiz
            {
                Id = quizDto.Id,
                LessonId = quizDto.LessonId,
                NoOfQuestions = quizDto.NoOfQuestions
            };
        }

        public async Task<QuizDto> MapEntityToDto(Quiz quiz)
        {
            var quizDto = new QuizDto
            {
                Id = quiz.Id,
                LessonId = quiz.LessonId,
                NoOfQuestions = quiz.NoOfQuestions
            };
            foreach (var question in quiz.Questions)
            {
                quizDto.Questions.Add(new Dictionary<string, string> { { "QuestionText", question.QuestionText }, { "ChoiceB", question.ChoiceB }, { "ChoiceC", question.ChoiceC }, { "ChoiceD", question.ChoiceD }, { "CorrectAnswer", question.CorrectAnswer } });
            }
            return quizDto;
        }
    }
}
