using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping.Post
{
    public class EnrollmentItemMappingService : IMappingService<EnrollmentItem, EnrollmentItemCreateDto>
    {
        public Task<EnrollmentItemCreateDto> MapEntityToDto(EnrollmentItem entity)
        {
            var dto = new EnrollmentItemCreateDto
            {
                Title = entity.Description
            };
            return Task.FromResult(dto);
        }

        public EnrollmentItem MapDtoToEntity(EnrollmentItemCreateDto dto)
        {
            var entity = new EnrollmentItem
            {
                Description = dto.Title
            };
            return entity;
        }
    }
}
