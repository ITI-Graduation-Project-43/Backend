using Microsoft.AspNetCore.JsonPatch;

namespace MindMission.Application.Interfaces.DtoValidator
{
    public interface IDtoValidator<TDto>
    {
        Task ValidateAsync(TDto dto);
    }
}
