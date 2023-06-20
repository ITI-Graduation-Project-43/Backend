using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.Interfaces.Patch;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Application.Exceptions;
using Newtonsoft.Json;
using MindMission.Application.DTOs.PostDtos;

namespace MindMission.Application.CustomValidation
{
    public class ChapterPatchValidator : IChaperPatchValidator
    {
        private readonly ICourseService _courseService;

        public ChapterPatchValidator(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task ValidatePatchAsync(JsonPatchDocument<PostChapterDto> patchDoc)
        {
            foreach (var operation in patchDoc.Operations)
            {
                if (operation.path == "/CourseId")
                {
                    var courseId = Convert.ToInt32(operation.value);
                    var _ = await _courseService.GetByIdAsync(courseId) ?? throw new ValidationException("Invalid courseId");

                }



                if (operation.path == "/Title")
                {
                    var title = operation.value?.ToString();

                    if (string.IsNullOrEmpty(title))
                    {
                        throw new ArgumentException("Title is required.");
                    }

                    if (title.Length > 250)
                    {
                        throw new ArgumentException("Title cannot exceed 250 characters.");
                    }

                }

            }
        }

    }

}
