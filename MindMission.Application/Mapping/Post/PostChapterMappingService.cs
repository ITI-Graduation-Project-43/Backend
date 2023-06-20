using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapping.Post
{
    public class PostChapterMappingService : IMappingService<Chapter, PostChapterDto>
    {

        public PostChapterMappingService()
        {
        }

        public async Task<PostChapterDto> MapEntityToDto(Chapter chapter)
        {
            var chapterCreateDto = new PostChapterDto
            {
                Id = chapter.Id,
                Title = chapter.Title,
                CourseId = chapter.CourseId

            };

            return chapterCreateDto;
        }

        public Chapter MapDtoToEntity(PostChapterDto chaperCreateDto)
        {
            var chapter = new Chapter
            {
                Id = chaperCreateDto.Id,
                Title = chaperCreateDto.Title,
                CourseId = chaperCreateDto.CourseId
            };


            return chapter;
        }
    }

}
