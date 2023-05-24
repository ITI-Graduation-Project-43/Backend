using MindMission.Application.DTOs;
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
                NoOfHours = chapterDto.NoOfHours,
                CreatedAt = chapterDto.CreatedAt,
                UpdatedAt = chapterDto.UpdatedAt
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
                NoOfHours = chapter.NoOfHours,
                CreatedAt = chapter.CreatedAt,
                UpdatedAt = chapter.UpdatedAt
            };
            return chapterDto;
        }
    }
}
