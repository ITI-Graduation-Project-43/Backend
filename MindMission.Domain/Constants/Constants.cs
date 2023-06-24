namespace MindMission.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string BadRequest = "Bad request.";
        public const string UnauthorizedAccess = "You are not authorized to perform this action.";
        public const string ForbiddenAccess = "You are not allowed to access this resource.";
        public const string ServerError = "An error occurred while processing your request.";
        public const string DatabaseError = "An error occurred while processing the {0}. Please try again later.";
        public const string InvalidData = "The provided data is not valid.";
        public const string ValidationFailed = "Validation failed for one or more entities.";

        public const string ResourceNotFound = "{0} not found.";
        public const string Conflict = "{0} already exists.";
        public const string IdMismatch = "{0} ID mismatch.";
        public const string NoChanges = "No changes were made to the {0}.";

        public const string Required = "{0} is required.";
        public const string InvalidId = "Provided {0} Id is invalid. It should be a positive integer.";
        public const string InvalidType = "Provided {0} Type is invalid. It should be '{1}'.";
        public const string CannotBeNull = "{0} cannot be null.";
        public const string InvalidAlphabeticCharacters = "Name should contain only alphabetic characters.";
        public const string InvalidEmailFormat = "Invalid email format.";
        public const string InvalidImageFileFormat = "Profile picture should be a .png or .jpg file.";
        public const string PasswordShouldBeStrong = "Password should contain at least 8 characters, including uppercase and lowercase letters, and at least one digit.";


        public const string AnswerRequired = "Correct Answer is required.";
        public const string AnswerTooLong = "Correct Answer must be a single character.";
        public const string InvalidAnswerFormat = "Correct Answer must be a single character between A and D.";

        public const string InvalidContentLength = $"Content length exceeds the limit of {{0}} characters.";
        public const string LengthAboveMaximum = "The length of the field is above the maximum length of {0} characters.";
        public const string LengthBelowMinimum = "The length of the field is below the minimum length of {0} characters.";
        public const string LengthOutOfRange = "The length of the field is out of the range [{0}, {1}] characters.";
        public const string ValueAboveMaximum = "The value of the field is above the maximum value of {0}.";
        public const string ValueBelowMinimum = "The value of the field is below the minimum value of {0}.";
        public const string RangeValueExceeded = "The value must be between {0} and {1}.";
        public const string IncorrectListCount = "The list must contain at least {0} item(s).";
        public const string IncorrectRangeListCount = "The list must contain at least {0} item(s) and atmost {1} item(s).";





    }

    public static class SuccessMessages
    {
        public const string RetrievedSuccessfully = "{0} retrieved successfully.";
        public const string CreatedSuccessfully = "{0} created successfully.";
        public const string UploadedSuccessfully = "{0} uploaded successfully.";
        public const string UpdatedSuccessfully = "{0} updated successfully.";
        public const string DeletedSuccessfully = "{0} deleted successfully.";
    }
}