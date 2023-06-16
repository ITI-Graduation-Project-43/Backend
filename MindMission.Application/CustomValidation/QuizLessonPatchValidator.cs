using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Interfaces.Patch;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MindMission.Application.CustomValidation
{
    public class QuizLessonPatchValidator : LessonPatchValidator, IQuizLessonPatchValidator
    {
        public async Task ValidatePatchAsync(JsonPatchDocument<PostQuizLessonDto> patchDoc)
        {
            foreach (var operation in patchDoc.Operations)
            {
                if (operation.path == "/Questions")
                {
                    var questions = JsonConvert.DeserializeObject<List<PostQuestionDto>>(operation.value.ToString());
                    if (questions == null || !questions.Any() || questions.Any(q => string.IsNullOrWhiteSpace(q.QuestionText) || q.QuestionText.Length > 500
                        || string.IsNullOrWhiteSpace(q.ChoiceA) || q.ChoiceA.Length > 255
                        || string.IsNullOrWhiteSpace(q.ChoiceB) || q.ChoiceB.Length > 255
                        || string.IsNullOrWhiteSpace(q.ChoiceC) || q.ChoiceC.Length > 255
                        || string.IsNullOrWhiteSpace(q.ChoiceD) || q.ChoiceD.Length > 255
                        || !Regex.IsMatch(q.CorrectAnswer, "^[A-D]$")))
                    {
                        throw new ValidationException("Invalid Questions");
                    }
                }

            }
        }
    }
}
