using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.Base;
using MindMission.Application.Factories;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Responses;
using MindMission.Domain.Constants;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.API.Controllers.Base
{
    public abstract class BaseController<TEntity, TDto, Type> : ControllerBase where TEntity : class where TDto : class, IDtoWithId<Type>
    {
        private readonly IMappingService<TEntity, TDto> _entityMappingService;
        public static int EntitiesCount { get; set; }

        protected BaseController(IMappingService<TEntity, TDto> entityMappingService)
        {
            _entityMappingService = entityMappingService;
        }

        static BaseController()
        {
            EntitiesCount = 0;
        }

        #region Mapping

        protected async Task<List<TDto>> MapEntitiesToDTOs(IEnumerable<TEntity> entities)
        {
            var dtoResults = new List<TDto>();

            if (typeof(TEntity) == typeof(Student) || typeof(TEntity) == typeof(Instructor) || typeof(TEntity) == typeof(Wishlist) || typeof(TEntity) == typeof(Enrollment) || typeof(TEntity) == typeof(Discussion))
            {
                // Run the mapping sequentially for Student and Instructor types.
                foreach (var entity in entities)
                {
                    var dto = await _entityMappingService.MapEntityToDto(entity);
                    dtoResults.Add(dto);
                }
            }
            else
            {
                var dtoTasks = entities?.Select(async entity => await _entityMappingService.MapEntityToDto(entity));
                var dtoResultsArray = await Task.WhenAll(dtoTasks);
                dtoResults = dtoResultsArray.ToList();
            }

            return dtoResults;

        }

        protected async Task<TDto> MapEntityToDTO(TEntity entity)
        {
            return await _entityMappingService.MapEntityToDto(entity);
        }

        protected TEntity MapDTOToEntity(TDto dto)
        {
            return _entityMappingService.MapDtoToEntity(dto);
        }

        #endregion Mapping

        #region Responses

        protected ResponseObject<TDto> GenerateResponse(bool success, string message, List<TDto> dtos = null, PaginationDto pagination = null, int entitiesCount = 1)
        {
            if (pagination != null)
            {
                return ResponseObjectFactory.CreateResponseObject<TDto>(success, message, dtos, pagination.PageNumber, pagination.PageSize, entitiesCount);
            }

            return ResponseObjectFactory.CreateResponseObject<TDto>(success, message, dtos ?? new List<TDto>());
        }

        protected ResponseObject<TDto> NotFoundResponse(string entityName)
        {
            string message = string.Format(ErrorMessages.ResourceNotFound, entityName);
            return GenerateResponse(false, message);
        }

        protected ResponseObject<TDto> BadRequestResponse()
        {
            return GenerateResponse(false, ErrorMessages.BadRequest);
        }
        protected ResponseObject<TDto> BadRequestResponse(string message)
        {
            return GenerateResponse(false, message);
        }
        protected ResponseObject<TDto> UnauthorizedResponse()
        {
            return GenerateResponse(false, ErrorMessages.UnauthorizedAccess);
        }
        protected ResponseObject<TDto> DatabaseErrorResponse(string entityName)
        {
            string message = string.Format(ErrorMessages.DatabaseError, entityName);
            return GenerateResponse(false, message);
        }
        protected ResponseObject<TDto> ForbiddenResponse()
        {
            return GenerateResponse(false, ErrorMessages.ForbiddenAccess);
        }

        protected ResponseObject<TDto> ConflictResponse(string entityName)
        {
            string message = string.Format(ErrorMessages.Conflict, entityName);
            return GenerateResponse(false, message);
        }

        protected ResponseObject<TDto> ServerErrorResponse()
        {
            return GenerateResponse(false, ErrorMessages.ServerError);
        }

        protected ResponseObject<TDto> InvalidDataResponse()
        {
            return GenerateResponse(false, ErrorMessages.InvalidData);
        }

        protected ResponseObject<TDto> ValidationFailedResponse()
        {
            return GenerateResponse(false, ErrorMessages.ValidationFailed);
        }

        protected ResponseObject<TDto> NoChangesResponse(string entityName)
        {
            string message = string.Format(ErrorMessages.NoChanges, entityName);

            return GenerateResponse(false, message);
        }

        protected ResponseObject<TDto> IdMismatchResponse(string entityName)
        {
            string message = string.Format(ErrorMessages.IdMismatch, entityName);

            return GenerateResponse(false, message);
        }

        protected ResponseObject<TDto> RetrieveSuccessResponse(List<TDto> dtos, PaginationDto pagination, string entityType, int entitiesCount)
        {
            string message = string.Format(SuccessMessages.RetrievedSuccessfully, entityType);
            return GenerateResponse(true, message, dtos, pagination, entitiesCount);
        }

        protected ResponseObject<TDto> CreatedSuccessResponse(TDto dto, PaginationDto pagination, string entityType)
        {
            string message = string.Format(SuccessMessages.CreatedSuccessfully, entityType);
            return GenerateResponse(true, message, new List<TDto> { dto }, pagination);
        }

        protected ResponseObject<TDto> UpdatedSuccessResponse(TDto dto, PaginationDto pagination, string entityType)
        {
            string message = string.Format(SuccessMessages.UpdatedSuccessfully, entityType);
            return GenerateResponse(true, message, new List<TDto> { dto }, pagination);
        }

        #endregion Responses

        #region CRUD Functions

        protected async Task<ActionResult> GetEntitiesResponse(Func<Task<IQueryable<TEntity>>> serviceMethod, PaginationDto pagination, string entityName)
        {
            var entities = await serviceMethod.Invoke();

            if (entities == null)
                return NotFound(NotFoundResponse(entityName));

            EntitiesCount = entities.Count();

            var entitiesPage = entities.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var entityDTOs = await MapEntitiesToDTOs(entitiesPage);

            var response = RetrieveSuccessResponse(entityDTOs, pagination, entityName, EntitiesCount);

            return Ok(response);
        }
        protected async Task<ActionResult> GetEntitiesResponsePagination(Func<IQueryable<TEntity>> serviceMethod, Func<Task<int>> totalCountMethod, PaginationDto pagination, string entityName)
        {
            var entities = serviceMethod.Invoke();

            if (entities == null)
                return NotFound(NotFoundResponse(entityName));

            var totalCount = await totalCountMethod.Invoke();

            var entityDTOs = await MapEntitiesToDTOs(entities);

            var response = RetrieveSuccessResponse(entityDTOs, pagination, entityName, totalCount);

            return Ok(response);
        }


        protected async Task<ActionResult> GetEntitiesResponseEnumerable(Func<Task<IEnumerable<TEntity>>> serviceMethod, PaginationDto pagination, string entityName)
        {
            var entities = await serviceMethod.Invoke();

            if (entities == null)
                return NotFound(NotFoundResponse(entityName));

            EntitiesCount = entities.Count();

            var entitiesPage = entities.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var entityDTOs = await MapEntitiesToDTOs(entitiesPage);

            var response = RetrieveSuccessResponse(entityDTOs, pagination, entityName, EntitiesCount);

            return Ok(response);
        }
        protected async Task<ActionResult> GetEntitiesResponseWithInclude(Func<Expression<Func<TEntity, object>>[], Task<IEnumerable<TEntity>>> serviceMethod, PaginationDto pagination, string entityName, params Expression<Func<TEntity, object>>[] IncludeProperties)
        {
            var entities = await serviceMethod.Invoke(IncludeProperties);

            if (entities == null)
                return NotFound(NotFoundResponse(entityName));

            EntitiesCount = entities.Count();

            var entitiesPage = entities.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var entityDTOs = await MapEntitiesToDTOs(entitiesPage);

            var response = RetrieveSuccessResponse(entityDTOs, pagination, entityName, EntitiesCount);

            return Ok(response);
        }

        protected async Task<ActionResult> GetEntitiesResponseWithIncludePagination(Func<int, int, Expression<Func<TEntity, object>>[], IQueryable<TEntity>> serviceMethod, Func<Task<int>> totalCountMethod, PaginationDto pagination, string entityName, params Expression<Func<TEntity, object>>[] IncludeProperties)
        {
            var entities = serviceMethod.Invoke(pagination.PageNumber, pagination.PageSize, IncludeProperties);
            if (entities == null)
                return NotFound(NotFoundResponse(entityName));

            var totalCount = await totalCountMethod.Invoke();

            var entityDTOs = await MapEntitiesToDTOs(entities);

            var response = RetrieveSuccessResponse(entityDTOs, pagination, entityName, totalCount);

            return Ok(response);
        }


        protected async Task<ActionResult> GetEntityResponse(Func<Task<TEntity>> serviceMethod, string entityName)
        {
            var entity = await serviceMethod.Invoke();

            if (entity == null)
                return NotFound(NotFoundResponse(entityName));

            var entityDto = await MapEntityToDTO(entity);


            var response = RetrieveSuccessResponse(new List<TDto> { entityDto }, new PaginationDto { PageNumber = 1, PageSize = 1 }, entityName, 1);

            return Ok(response);
        }

        protected async Task<ActionResult> GetEntityResponseWithInclude(Func<Task<TEntity>> serviceMethod, string entityName)
        {
            var entity = await serviceMethod.Invoke();

            if (entity == null)
                return NotFound(NotFoundResponse(entityName));

            var entityDto = await MapEntityToDTO(entity);


            var response = RetrieveSuccessResponse(new List<TDto> { entityDto }, new PaginationDto { PageNumber = 1, PageSize = 1 }, entityName, 1);

            return Ok(response);
        }

        protected async Task<ActionResult> DeleteEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<int, Task> serviceDeleteMethod, int id)
        {
            var entity = await serviceGetMethod.Invoke(id);

            if (entity == null)
                return NotFound(NotFoundResponse(entity.GetType().Name));

            await serviceDeleteMethod.Invoke(id);

            return NoContent();
        }

        protected async Task<ActionResult> AddEntityResponse(Func<TEntity, Task<TEntity>> serviceAddMethod, TDto dto, string entityName, string actionName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }

            var entity = MapDTOToEntity(dto);

            var addedEntity = await serviceAddMethod.Invoke(entity);

            var createdDto = await MapEntityToDTO(addedEntity);

            if (createdDto == null)
                return NotFound(NotFoundResponse(entityName));

            var response = CreatedSuccessResponse(createdDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, entityName);

            return CreatedAtAction(actionName, new { id = createdDto.Id }, response);
        }

        protected async Task<ActionResult> PatchEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceUpdateMethod, int id, string entityName, JsonPatchDocument<TDto> patchDocument)
        {
            var entity = await serviceGetMethod.Invoke(id);
            if (entity == null)
            {
                return NotFound(NotFoundResponse(entityName));
            }

            var dto = await MapEntityToDTO(entity);
            if (dto == null)
            {
                return BadRequest(BadRequestResponse("Dto cannot be null."));
            }
            if (patchDocument == null)
            {
                return BadRequest(BadRequestResponse("Patch document cannot be null."));

            }

            patchDocument.ApplyTo(dto, error => ModelState.AddModelError("JsonPatch", error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }
            entity = _entityMappingService.MapDtoToEntity(dto);
            await serviceUpdateMethod.Invoke(entity);


            return NoContent();
        }

        protected async Task<ActionResult> PatchEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceUpdateMethod, int id, string entityName, JsonPatchDocument<TDto> patchDocument, Action<TEntity, TDto> patchOperations)
        {
            var entity = await serviceGetMethod.Invoke(id);
            if (entity == null)
            {
                return NotFound(NotFoundResponse(entityName));
            }

            var dto = await MapEntityToDTO(entity);
            patchDocument.ApplyTo(dto, error => ModelState.AddModelError("JsonPatch", error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }

            patchOperations(entity, dto);
            await serviceUpdateMethod.Invoke(entity);

            return NoContent();
        }

        protected async Task<ActionResult> UpdateEntityResponse(Func<int, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceUpdateMethod, int id, TDto dto, string entityName)
        {
            if (id.Equals(dto.Id))
            {
                var entity = await serviceGetMethod.Invoke(id);

                if (entity == null)
                    return NotFound(NotFoundResponse(entityName));

                var originalDto = MapEntityToDTO(entity);
                if (originalDto.Equals(dto))
                {
                    return Ok(NoChangesResponse(entityName));
                }

                entity = MapDTOToEntity(dto);
                await serviceUpdateMethod.Invoke(entity);

                var response = UpdatedSuccessResponse(dto, new PaginationDto { PageNumber = 1, PageSize = 1 }, entityName);

                return Ok(response);
            }
            else
            {
                return BadRequest(IdMismatchResponse(entityName));
            }
        }
        //overload with string id for patch
        protected async Task<ActionResult> UpdateEntityResponse(Func<string, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceUpdateMethod, string id, TDto dto, string entityName)
        {
            if (id.Equals(dto.Id))
            {
                var entity = await serviceGetMethod.Invoke(id);

                if (entity == null)
                    return NotFound(NotFoundResponse(entityName));

                var originalDto = MapEntityToDTO(entity);
                if (originalDto.Equals(dto))
                {
                    return Ok(NoChangesResponse(entityName));
                }

                entity = MapDTOToEntity(dto);
                await serviceUpdateMethod.Invoke(entity);

                var response = UpdatedSuccessResponse(dto, new PaginationDto { PageNumber = 1, PageSize = 1 }, entityName);

                return Ok(response);
            }
            else
            {
                return BadRequest(IdMismatchResponse(entityName));
            }
        }

        #endregion CRUD Functions


    }
}