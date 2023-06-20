using MindMission.Domain.Common;

namespace MindMission.Application.Validator.Base
{
    /// <summary>
    /// Interface for a validator service that validates a DTO.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO to validate.</typeparam>
    public interface IValidatorService<TDto>
    {
        /// <summary>
        /// Validates the specified DTO asynchronously.
        /// </summary>
        /// <param name="dto">The DTO to validate.</param>
        /// <returns>A task representing the asynchronous validation operation.</returns>

        Task<string?> ValidateAsync(TDto dto, bool isPost);

    }

}
