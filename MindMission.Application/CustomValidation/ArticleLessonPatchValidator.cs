using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Interfaces.Patch;


namespace MindMission.Application.CustomValidation
{
    public class ArticleLessonPatchValidator : LessonPatchValidator, IArticleLessonPatchValidator
    {
        public async Task ValidatePatchAsync(JsonPatchDocument<PostArticleLessonDto> patchDoc)
        {
            foreach (var operation in patchDoc.Operations)
            {
                if (operation.path == "/Content")
                {
                    var content = Convert.ToString(operation.value);
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        throw new ValidationException("Invalid Content");
                    }
                }

            }
        }
    }
}
