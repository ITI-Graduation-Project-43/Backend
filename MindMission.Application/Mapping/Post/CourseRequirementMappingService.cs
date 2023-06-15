using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping.Post
{
    public class CourseRequirementMappingService : IMappingService<CourseRequirement, CourseRequirementCreateDto>
    {
        public Task<CourseRequirementCreateDto> MapEntityToDto(CourseRequirement entity)
        {
            var dto = new CourseRequirementCreateDto
            {
                Title = entity.Title,
                Description = entity.Description
            };
            return Task.FromResult(dto);
        }

        public CourseRequirement MapDtoToEntity(CourseRequirementCreateDto dto)
        {
            var entity = new CourseRequirement
            {
                Title = dto.Title,
                Description = dto.Description
            };
            return entity;
        }
    }
}
