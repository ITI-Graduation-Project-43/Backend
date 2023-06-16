using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Patch;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Application.Exceptions;
using Newtonsoft.Json;


namespace MindMission.Infrastructure
{
    public class CoursePatchValidator : ICoursePatchValidator
    {
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;

        public CoursePatchValidator(ICategoryService categoryService, IInstructorService instructorService)
        {
            _categoryService = categoryService;
            _instructorService = instructorService;
        }

        public async Task ValidatePatchAsync(JsonPatchDocument<CourseCreateDto> patchDoc)
        {
            foreach (var operation in patchDoc.Operations)
            {
                if (operation.path == "/CategoryId")
                {
                    var categoryId = Convert.ToInt32(operation.value);
                    var category = await _categoryService.GetByIdAsync(categoryId) ?? throw new ValidationException("Invalid CategoryId");
                    if (category.Type != CategoryType.Topic)
                    {
                        throw new ValidationException("Category Type is not Topic");
                    }
                }

                if (operation.path == "/InstructorId")
                {
                    var instructorId = Convert.ToString(operation.value);
                    var instructor = await _instructorService.GetByIdAsync(instructorId) ?? throw new ValidationException("Invalid InstructorId");
                }

                if (operation.path == "/Language")
                {
                    if (!Enum.TryParse<Language>(operation.value.ToString(), out _))
                    {
                        throw new ValidationException("Invalid Language");
                    }
                }

                if (operation.path == "/Level")
                {
                    if (!Enum.TryParse<Level>(operation.value.ToString(), out _))
                    {
                        throw new ValidationException("Invalid Level");
                    }
                }

                if (operation.path == "/Price")
                {
                    var price = Convert.ToDecimal(operation.value);

                    if (price < 0 || price > 5000)
                    {
                        throw new ValidationException("Invalid Price");
                    }
                }

                if (operation.path == "/LearningItems")
                {
                    var learningItems = JsonConvert.DeserializeObject<List<LearningItemCreateDto>>(operation.value.ToString());

                    if (learningItems == null || !learningItems.Any() || learningItems.Any(item => string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Description)))
                    {
                        throw new ValidationException("Invalid LearningItems");
                    }
                }

                if (operation.path == "/EnrollmentItems")
                {
                    var enrollmentItems = JsonConvert.DeserializeObject<List<EnrollmentItemCreateDto>>(operation.value.ToString());

                    if (enrollmentItems == null || !enrollmentItems.Any() || enrollmentItems.Any(item => string.IsNullOrWhiteSpace(item.Title)))
                    {
                        throw new ValidationException("Invalid EnrollmentItems");
                    }
                }

                if (operation.path == "/CourseRequirements")
                {
                    var courseRequirements = JsonConvert.DeserializeObject<List<CourseRequirementCreateDto>>(operation.value.ToString());

                    if (courseRequirements != null && courseRequirements.Any(item => string.IsNullOrWhiteSpace(item.Title)))
                    {
                        throw new ValidationException("Invalid CourseRequirements");
                    }
                }
            }
        }


    }

}
