using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public interface IMappingService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<TDto> MapEntityToDto(TEntity entity);
        TEntity MapDtoToEntity(TDto dto);
    }
}
