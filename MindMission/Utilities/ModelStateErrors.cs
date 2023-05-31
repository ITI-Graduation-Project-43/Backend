using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MindMission.API.Utilities
{
    public static class ModelStateErrors
    {
        public static string BadRequestError(ModelStateDictionary ModelStateErrors)
        {
            string Errors = string.Empty;
            foreach (var ModelState in ModelStateErrors.Values)
            {
                foreach (var Error in ModelState.Errors)
                {
                    Errors += Error.ErrorMessage + ", ";
                }
            }
            return Errors.Substring(0, Errors.Length - 2);
        }
    }
}