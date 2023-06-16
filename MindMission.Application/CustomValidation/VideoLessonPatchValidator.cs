using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Interfaces.Patch;


namespace MindMission.Application.CustomValidation
{
    public class VideoLessonPatchValidator : LessonPatchValidator, IVideoLessonPatchValidator
    {
        public async Task ValidatePatchAsync(JsonPatchDocument<PostVideoLessonDto> patchDoc)
        {
            foreach (var operation in patchDoc.Operations)
            {
                if (operation.path == "/VideoUrl")
                {
                    var videoUrl = Convert.ToString(operation.value);
                    if (string.IsNullOrWhiteSpace(videoUrl) || videoUrl.Length > 2048)
                    {
                        throw new ValidationException("Invalid VideoUrl");
                    }
                }

            }

        }
    }
}
