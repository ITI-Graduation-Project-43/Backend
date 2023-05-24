using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class LessonMappingService : IMappingService<Lesson, LessonDto>
    {
        public Lesson MapDtoToEntity(LessonDto lessonDto)
        {
            return new Lesson
            {
                Id = lessonDto.Id,
                ChapterId = lessonDto.ChapterId,
                Title = lessonDto.Title,
                Description = lessonDto.Description,
                Type = lessonDto.Description,
                NoOfHours = lessonDto.NoOfHours,
                IsFree = lessonDto.IsFree,
                CreatedAt = lessonDto.CreatedAt,
                UpdatedAt = lessonDto.UpdatedAt
            };
        }

        public async Task<LessonDto> MapEntityToDto(Lesson lesson)
        {
            var lessonDto = new LessonDto
            {
                Id = lesson.Id,
                ChapterId = lesson.ChapterId,
                Title = lesson.Title,
                Description = lesson.Description,
                Type = lesson.Type,
                NoOfHours = lesson.NoOfHours,
                IsFree = lesson.IsFree,
                CreatedAt = lesson.CreatedAt,
                UpdatedAt = lesson.UpdatedAt
            };
            return lessonDto;
        }
    }
}
