using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;

namespace MindMission.Application.Interfaces.Patch
{
    public interface IPatchValidator<TClass> where TClass : class
    {
        Task ValidatePatchAsync(JsonPatchDocument<TClass> patchDoc);

    }
}
