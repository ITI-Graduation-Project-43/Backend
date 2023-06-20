using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Exceptions;
using MindMission.Application.Factories;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Constants;
using MindMission.Domain.Common;
using MindMission.Application.DTOs.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Responses;
using System.Linq.Expressions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MindMission.API.Controllers.Base
{
    public abstract class BaseController<TEntity, TDto, TCreateDto, TKey> : ControllerBase
                                                                    where TEntity : class, IEntity<TKey>, new()
                                                                    where TDto : class, IDtoWithId<TKey>
                                                                    where TCreateDto : class, IDtoWithId<TKey>
    {
        protected readonly IMapper _mapper;
        protected readonly IValidatorService<TCreateDto> _validatorService;
        protected readonly string _entityName;
        protected readonly string _entitiesName;
        private static int EntitiesCount { get; set; } = 0;

        protected BaseController(IMapper mapper, IValidatorService<TCreateDto> validatorService, string entityName, string entitiesName)
        {
            _mapper = mapper;
            _validatorService = validatorService;
            _entityName = entityName;
            _entitiesName = entitiesName;
        }

        #region Helpers
        protected ResponseObject<TDto> GenerateResponse(bool success, string message, List<TDto> dtos = null, PaginationDto pagination = null, int entitiesCount = 1)
        {
            if (pagination != null)
            {
                return ResponseObjectFactory.CreateResponseObject(success, message, dtos, pagination.PageNumber, pagination.PageSize, entitiesCount);
            }

            return ResponseObjectFactory.CreateResponseObject(success, message, dtos ?? new List<TDto>());
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
        protected ResponseObject<TDto> BadRequestResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.SelectMany(x => x.Value.Errors.Select(p => p.ErrorMessage)).ToList();
            var errorString = string.Join("; ", errors);

            return GenerateResponse(false, errorString);
        }
        protected ResponseObject<TDto> NullResponse(string entity)
        {
            return GenerateResponse(false, string.Format(ErrorMessages.CannotBeNull, entity));
        }
        protected ResponseObject<TDto> UnauthorizedResponse()
        {
            return GenerateResponse(false, ErrorMessages.UnauthorizedAccess);
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
        protected ResponseObject<TDto> DatabaseErrorResponse(string entityName)
        {
            string message = string.Format(ErrorMessages.DatabaseError, entityName);
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
        protected ResponseObject<TDto> RetrieveSuccessResponse(List<TDto> dtos, string entityName, PaginationDto pagination = null, int entitiesCount = 1)
        {
            string message = string.Format(SuccessMessages.RetrievedSuccessfully, entityName);
            return GenerateResponse(true, message, dtos, pagination, entitiesCount);
        }

        protected ResponseObject<TDto> CreatedSuccessResponse(TDto dto, string entityName, PaginationDto pagination = null)
        {
            string message = string.Format(SuccessMessages.CreatedSuccessfully, entityName);
            return GenerateResponse(true, message, new List<TDto> { dto }, pagination);
        }

        protected ResponseObject<TDto> UpdatedSuccessResponse(TDto dto, string entityName, PaginationDto pagination = null)
        {
            string message = string.Format(SuccessMessages.UpdatedSuccessfully, entityName);
            return GenerateResponse(true, message, new List<TDto> { dto }, pagination);
        }

        #endregion
        protected async Task<ActionResult> GetAll(Func<Expression<Func<TEntity, object>>[], Task<IEnumerable<TEntity>>> serviceMethod, PaginationDto pagination, params Expression<Func<TEntity, object>>[] IncludeProperties)
        {
            try
            {
                var entities = await serviceMethod.Invoke(IncludeProperties);
                if (!entities.Any())
                {
                    return NotFound(NotFoundResponse(_entitiesName));
                }

                EntitiesCount = entities.Count();

                var entitiesPage = entities.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

                var dtos = _mapper.Map<IEnumerable<TDto>>(entitiesPage).ToList();

                return Ok(RetrieveSuccessResponse(dtos, _entitiesName, pagination, EntitiesCount));
            }
            catch (Exception)
            {
                return BadRequest(DatabaseErrorResponse(_entitiesName));
            }
        }

        protected async Task<ActionResult> GetById(Func<Task<TEntity>> serviceMethod)
        {
            try
            {
                var entity = await serviceMethod.Invoke();
                if (entity == null)
                {
                    return NotFound(NotFoundResponse(_entityName));
                }
                var dto = _mapper.Map<TDto>(entity);
                return Ok(RetrieveSuccessResponse(new List<TDto> { dto }, _entityName));
            }
            catch (Exception)
            {
                return BadRequest(DatabaseErrorResponse(_entityName));
            }

        }

        protected async Task<ActionResult> Create(Func<TEntity, Task<TEntity>> serviceAddMethod, TCreateDto createDto, string actionName)
        {
            try
            {
                if (createDto == null)
                {
                    return BadRequest(NullResponse(_entityName));
                }


                if (typeof(TKey) == typeof(int))
                {
                    if (int.TryParse(createDto.Id.ToString(), out int id) && id != 0)
                    {
                        createDto.Id = (TKey)(object)0;
                    }
                }
                else if (typeof(TKey) == typeof(string) && !string.IsNullOrEmpty(createDto.Id as string))
                {
                    createDto.Id = (TKey)(object)string.Empty;
                }
                else if (!string.IsNullOrEmpty(createDto.Id as string))
                {
                    createDto.Id = (TKey)(object)string.Empty;
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(BadRequestResponse(ModelState));
                }

                var validationError = await _validatorService.ValidateAsync(createDto, true);
                if (validationError != null)
                {
                    return BadRequest(BadRequestResponse(validationError));
                }



                var entity = _mapper.Map<TEntity>(createDto);
                var createdEntity = await serviceAddMethod.Invoke(entity);

                var dto = _mapper.Map<TDto>(createdEntity);
                if (dto == null)
                    return NotFound(NotFoundResponse(_entityName));
                return CreatedAtAction(actionName, new { id = dto.Id }, CreatedSuccessResponse(dto, _entityName));
            }
            catch (Exception)
            {
                return BadRequest(DatabaseErrorResponse(_entityName));
            }
        }

        protected async Task<ActionResult> Update(Func<TKey, Task<TEntity>> serviceGetMethod, Func<TEntity, Task<TEntity>> serviceUpdateMethod, TKey id, TCreateDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return BadRequest(NullResponse(_entityName));
                }
                if (!id.Equals(updateDto.Id))
                {
                    return BadRequest(IdMismatchResponse(_entityName));
                }

                var existingEntity = await serviceGetMethod.Invoke(id);
                if (existingEntity == null)
                {
                    return NotFound(NotFoundResponse(_entityName));
                }


                var validationError = await _validatorService.ValidateAsync(updateDto, false);
                if (validationError != null)
                {
                    return BadRequest(BadRequestResponse(validationError));
                }
                _mapper.Map(updateDto, existingEntity);
                var updatedEntity = await serviceUpdateMethod.Invoke(existingEntity);

                var dto = _mapper.Map<TDto>(updatedEntity);
                return Ok(UpdatedSuccessResponse(dto, _entityName));
            }
            catch (Exception)
            {
                return BadRequest(DatabaseErrorResponse(_entityName));
            }
        }

        protected async Task<ActionResult> Patch(Func<TKey, Task<TEntity>> serviceGetMethod, Func<TKey, TEntity, Task<TEntity>> serviceUpdateMethod, TKey id, JsonPatchDocument<TCreateDto> patchDoc)
        {
            try
            {
                if (patchDoc == null || patchDoc.Operations.Count == 0)
                {
                    return BadRequest(BadRequestResponse(ModelState));
                }

                var entityInDb = await serviceGetMethod.Invoke(id);

                if (entityInDb == null)
                {
                    return NotFound(NotFoundResponse(_entityName));
                }

                var entityToUpdateDto = _mapper.Map<TCreateDto>(entityInDb);

                if (entityToUpdateDto == null)
                {
                    return BadRequest(BadRequestResponse(string.Format(ErrorMessages.CannotBeNull, "entity")));
                }

                patchDoc.ApplyTo(entityToUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(BadRequestResponse(ModelState));
                }


                var validationError = await _validatorService.ValidateAsync(entityToUpdateDto, false);
                if (validationError != null)
                {
                    return BadRequest(BadRequestResponse(validationError));
                }
                _mapper.Map(entityToUpdateDto, entityInDb);

                var updatedEntity = await serviceUpdateMethod.Invoke(id, entityInDb);

                var dto = _mapper.Map<TDto>(updatedEntity);

                return Ok(UpdatedSuccessResponse(dto, _entityName));
            }
            catch (Exception)
            {
                return BadRequest(DatabaseErrorResponse(_entityName));
            }
        }
        protected async Task<ActionResult> Delete(Func<TKey, Task<TEntity>> serviceGetMethod, Func<TEntity, Task> serviceDeleteMethod, TKey id)
        {
            try
            {
                var entity = await serviceGetMethod.Invoke(id);
                if (entity == null)
                {
                    return NotFound(NotFoundResponse(_entityName));
                }

                await serviceDeleteMethod.Invoke(entity);
                return Ok(GenerateResponse(true, string.Format(SuccessMessages.DeletedSuccessfully, _entityName)));
            }
            catch (Exception)
            {
                return BadRequest(DatabaseErrorResponse(_entityName));
            }
        }
        protected async Task<ActionResult> Delete(Func<TKey, Task> serviceDeleteMethod, TKey id)
        {

            try
            {
                await serviceDeleteMethod.Invoke(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(NotFoundResponse(_entityName));

            }
            catch
            {
                return BadRequest(DatabaseErrorResponse(_entityName));
            }
            return Ok(GenerateResponse(true, string.Format(SuccessMessages.DeletedSuccessfully, _entityName)));

        }
    }


}
