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
                NoOfQuestions = quizDto.NoOfQuestions,
                CreatedAt = quizDto.CreatedAt,
                UpdatedAt = quizDto.UpdatedAt
            };
        }

        public async Task<QuizDto> MapEntityToDto(Quiz quiz)
        {
            var quizDto = new QuizDto
            {
                Id = quiz.Id,
                LessonId = quiz.LessonId,
                NoOfQuestions = quiz.NoOfQuestions,
                CreatedAt = quiz.CreatedAt,
                UpdatedAt = quiz.UpdatedAt
            };
            return quizDto;
        }
    }
}
