using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTO;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Mapping;
using MindMission.Application.Responses;
using MindMission.Domain.Constants;


namespace MindMission.API.Controllers.Base
{
    public abstract class BaseController<TEntity, TDto> : ControllerBase where TEntity : class where TDto : class
    {
        private readonly IMappingService<TEntity, TDto> _entityMappingService;

        protected BaseController(IMappingService<TEntity, TDto> entityMappingService)
        {
            _entityMappingService = entityMappingService;
        }


        protected async Task<List<TDto>> MapEntitiesToDTOs(IEnumerable<TEntity> entities)
        {
            return (await Task.WhenAll(entities.Select(entity => _entityMappingService.MapEntityToDto(entity)))).ToList();
        }

        protected async Task<TDto> MapEntityToDTO(TEntity entity)
        {
            return await _entityMappingService.MapEntityToDto(entity);
        }



        protected ActionResult NotFoundResponse(string entityName)
        {
            return NotFound(string.Format(ErrorMessages.ResourceNotFound, entityName));
        }

        protected ResponseObject<TDto> CreateResponse(List<TDto> dtos, PaginationDto pagination, string entityType)
        {

            ResponseObject<TDto> response = ResponseObjectFactory.CreateResponseObject<TDto>(true, string.Format(SuccessMessages.RetrievedSuccessfully, entityType), dtos, pagination.PageNumber, pagination.PageSize);

            return response;
        }

        protected ResponseObject<TDto> CreateResponse(TDto dtos, PaginationDto pagination, string entityType)
        {

            ResponseObject<TDto> response = ResponseObjectFactory.CreateResponseObject<TDto>(true, string.Format(SuccessMessages.RetrievedSuccessfully, entityType), new List<TDto> { dtos }, pagination.PageNumber, pagination.PageSize);

            return response;
        }

        /*protected async Task<ActionResult<TDto>> GetEntity(Func<Task<TEntity>> getEntityFunc, string entityName)
        {
            var entity = await getEntityFunc();
            if (entity == null) return NotFoundResponse(entityName);

            var dto = await _entityMappingService.MapEntityToDto(entity);
            var response = CreateResponse(new List<TDto> { dto }, string.Format(SuccessMessages.RetrievedSuccessfully, entityName), 1, 1);

            return Ok(response);
        }*/

































    }

}
