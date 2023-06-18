using MindMission.Domain.Common;

namespace MindMission.Application.Validator.Base
{
    public interface IValidatorService<TDto>
    {
        Task<ValidationResult> ValidateAsync(TDto dto);
    }
}
