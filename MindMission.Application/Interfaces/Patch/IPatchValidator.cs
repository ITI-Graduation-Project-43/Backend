using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs;
using MindMission.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Patch
{
    public interface IPatchValidator<TClass> where TClass : class
    {
        Task ValidatePatchAsync(JsonPatchDocument<TClass> patchDoc);

    }
}
