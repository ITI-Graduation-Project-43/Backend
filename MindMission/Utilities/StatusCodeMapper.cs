using MindMission.Domain.Constants;

namespace MindMission.API.Utilities
{
    public static class StatusCodeMapper
    {
        public static int GetStatusCode(string message)
        {
            switch (message)
            {
                case ErrorMessages.ResourceNotFound:
                    return StatusCodes.Status404NotFound;

                case ErrorMessages.BadRequest:
                    return StatusCodes.Status400BadRequest;

                case ErrorMessages.UnauthorizedAccess:
                    return StatusCodes.Status401Unauthorized;

                case ErrorMessages.ForbiddenAccess:
                    return StatusCodes.Status403Forbidden;

                case ErrorMessages.Conflict:
                    return StatusCodes.Status409Conflict;

                case ErrorMessages.ServerError:
                    return StatusCodes.Status500InternalServerError;

                case ErrorMessages.InvalidData:
                    return StatusCodes.Status422UnprocessableEntity;

                case ErrorMessages.ValidationFailed:
                    return StatusCodes.Status400BadRequest;

                case SuccessMessages.RetrievedSuccessfully:
                    return StatusCodes.Status200OK;

                case SuccessMessages.CreatedSuccessfully:
                    return StatusCodes.Status201Created;

                case SuccessMessages.UpdatedSuccessfully:
                    return StatusCodes.Status200OK;

                case SuccessMessages.DeletedSuccessfully:
                    return StatusCodes.Status204NoContent;

                default:
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}