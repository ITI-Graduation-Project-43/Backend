using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs.PostDtos.Base;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Interfaces.Patch;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using MindMission.Application.Exceptions;

namespace MindMission.Application.CustomValidation
{
    public class LessonPatchValidator : ILessonPatchValidator
    {
        public async Task ValidatePatchAsync(JsonPatchDocument<LessonDtoBase> patchDoc)
        {
            foreach (var operation in patchDoc.Operations)
            {
                if (operation.path == "/Title")
                {
                    var title = Convert.ToString(operation.value);
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        throw new ValidationException("Invalid Title");
                    }
                }

                if (operation.path == "/Description")
                {
                    var description = Convert.ToString(operation.value);
                    if (description.Length > 2048)
                    {
                        throw new ValidationException("Description cannot exceed 2048 characters");
                    }
                }

                if (operation.path == "/NoOfHours")
                {
                    var noOfHours = Convert.ToSingle(operation.value);
                    if (noOfHours < 0)
                    {
                        throw new ValidationException("NoOfHours must be a positive float");
                    }
                }

                if (operation.path == "/IsFree")
                {
                    if (!Boolean.TryParse(operation.value.ToString(), out _))
                    {
                        throw new ValidationException("Invalid IsFree");
                    }
                }

            }
        }
    }
}
