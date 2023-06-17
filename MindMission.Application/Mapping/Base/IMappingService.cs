namespace MindMission.Application.Mapping.Base
{
    public interface IMappingService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<TDto> MapEntityToDto(TEntity entity);

        TEntity MapDtoToEntity(TDto dto);
    }

    public interface IMapDtoToEntityService<TEntity, TDto> where TEntity : class where TDto : class
    {
        TEntity MapDtoToEntity(TDto dto);
    }
    public interface IMapEntityToDtoService<TEntity, TDto> where TEntity : class where TDto : class
    {
        TDto MapEntityToDto(TEntity entity);
    }
}