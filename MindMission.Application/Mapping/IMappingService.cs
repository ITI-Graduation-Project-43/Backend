namespace MindMission.Application.Mapping
{
    public interface IMappingService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<TDto> MapEntityToDto(TEntity entity);
        TEntity MapDtoToEntity(TDto dto);
    }
}
