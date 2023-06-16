using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping.Post
{
    public class LearningItemMappingService : IMappingService<LearningItem, LearningItemCreateDto>
    {
        public Task<LearningItemCreateDto> MapEntityToDto(LearningItem entity)
        {
            var dto = new LearningItemCreateDto
            {
                Title = entity.Title,
                Description = entity.Description
            };
            return Task.FromResult(dto);
        }

        public LearningItem MapDtoToEntity(LearningItemCreateDto dto)
        {
            var entity = new LearningItem
            {
                Title = dto.Title,
                Description = dto.Description
            };
            return entity;
        }
    }
}
