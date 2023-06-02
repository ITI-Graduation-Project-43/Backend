using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class ChapterMappingService : IMappingService<Chapter, ChapterDto>
    {
        public Chapter MapDtoToEntity(ChapterDto chapterDto)
        {
            return new Chapter
            {
                Id = chapterDto.Id,
                CourseId = chapterDto.CourseId,
                Title = chapterDto.Title,
                NoOfLessons = chapterDto.NoOfLessons,
                NoOfHours = chapterDto.NoOfHours
            };
        }

        public async Task<ChapterDto> MapEntityToDto(Chapter chapter)
        {
            var chapterDto = new ChapterDto
            {
                Id = chapter.Id,
                CourseId = chapter.CourseId,
                Title = chapter.Title,
                NoOfLessons = chapter.NoOfLessons,
                NoOfHours = chapter.NoOfHours
            };
            foreach (var lesson in chapter.Lessons)
            {
                chapterDto.Lessons.Add(new Dictionary<string, string>() { { "Title", lesson.Title }, { "Description", lesson.Description }, { "NoOfHours", lesson.NoOfHours.ToString() } });
            }
            return chapterDto;
        }
    }
}