using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapping.Post
{
    public class PostQuizLessonMappingService : IMappingService<Lesson, PostQuizLessonDto>
    {
        private readonly IMappingService<Question, PostQuestionDto> _questionMappingService;

        public PostQuizLessonMappingService(IMappingService<Question, PostQuestionDto> questionMappingService)
        {
            _questionMappingService = questionMappingService;
        }

        public async Task<PostQuizLessonDto> MapEntityToDto(Lesson lesson)
        {
            var postQuizLessonDto = new PostQuizLessonDto
            {
                ChapterId = lesson.ChapterId,
                LessonId = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                NoOfHours = lesson.NoOfHours,
                IsFree = lesson.IsFree,
                Questions = lesson.Quiz != null
                    ? (await Task.WhenAll(lesson.Quiz.Questions
                        .Select(q => _questionMappingService.MapEntityToDto(q)))).ToList()
                    : new List<PostQuestionDto>()
            };

            return postQuizLessonDto;
        }

        public Lesson MapDtoToEntity(PostQuizLessonDto postQuizLessonDto)
        {
            var lesson = new Lesson
            {
                ChapterId = postQuizLessonDto.ChapterId,
                Id = postQuizLessonDto.LessonId,
                Title = postQuizLessonDto.Title,
                Description = postQuizLessonDto.Description,
                NoOfHours = postQuizLessonDto.NoOfHours,
                IsFree = postQuizLessonDto.IsFree,
                Type = LessonType.Quiz,
            };

            if (postQuizLessonDto.Questions != null && postQuizLessonDto.Questions.Any())
            {
                var quiz = new Quiz
                {
                    Questions = new List<Question>()
                };

                foreach (var postQuestionDto in postQuizLessonDto.Questions)
                {
                    var question = _questionMappingService.MapDtoToEntity(postQuestionDto);
                    question.Quiz = quiz;
                    quiz.Questions.Add(question);
                }

                lesson.Quiz = quiz;
            }

            return lesson;
        }

    }
}