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
    }

    public static class SuccessMessages
    {
        public const string RetrievedSuccessfully = "{0} retrieved successfully.";
    }
}
