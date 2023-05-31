using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string ResourceNotFound = "{0} not found.";
        public const string BadRequest = "Bad request.";
        public const string UnauthorizedAccess = "You are not authorized to perform this action.";
        public const string ForbiddenAccess = "You are not allowed to access this resource.";
        public const string Conflict = "{0} already exists.";
        public const string ServerError = "An error occurred while processing your request.";
        public const string InvalidData = "The provided data is not valid.";
        public const string ValidationFailed = "Validation failed for one or more entities.";
        public const string IdMismatch = "{0} ID mismatch.";
        public const string NoChanges = "No changes were made to the {0}.";
    }

    public static class SuccessMessages
    {
        public const string RetrievedSuccessfully = "{0} retrieved successfully.";
        public const string CreatedSuccessfully = "{0} created successfully.";
        public const string UpdatedSuccessfully = "{0} updated successfully.";
        public const string DeletedSuccessfully = "{0} deleted successfully.";
    }




}
