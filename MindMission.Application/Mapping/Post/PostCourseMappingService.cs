using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapping.Post
{
    public class PostCourseMappingService : IMappingService<Course, CourseCreateDto>
    {
        private readonly IMappingService<LearningItem, LearningItemCreateDto> _learningItemMappingService;
        private readonly IMappingService<EnrollmentItem, EnrollmentItemCreateDto> _enrollmentItemMappingService;
        private readonly IMappingService<CourseRequirement, CourseRequirementCreateDto> _courseRequirementMappingService;

        public PostCourseMappingService(
            IMappingService<LearningItem, LearningItemCreateDto> learningItemMappingService,
            IMappingService<EnrollmentItem, EnrollmentItemCreateDto> enrollmentItemMappingService,
            IMappingService<CourseRequirement, CourseRequirementCreateDto> courseRequirementMappingService)
        {
            _learningItemMappingService = learningItemMappingService;
            _enrollmentItemMappingService = enrollmentItemMappingService;
            _courseRequirementMappingService = courseRequirementMappingService;
        }

        public async Task<CourseCreateDto> MapEntityToDto(Course course)
        {
            var courseCreateDto = new CourseCreateDto
            {
                Id = course.Id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                Description = course.Description,
                Language = (Language)Enum.Parse(typeof(Language), course.Language),
                Level = (Level)Enum.Parse(typeof(Level), course.Level),
                Price = course.Price,
                InstructorId = course.InstructorId,
                CourseImage = course.ImageUrl,
                LearningItems = course.LearningItems != null ? (await Task.WhenAll(course.LearningItems.Select(i => _learningItemMappingService.MapEntityToDto(i)))).ToList() : new List<LearningItemCreateDto>(),
                EnrollmentItems = course.EnrollmentItems != null ? (await Task.WhenAll(course.EnrollmentItems.Select(i => _enrollmentItemMappingService.MapEntityToDto(i)))).ToList() : new List<EnrollmentItemCreateDto>(),
                CourseRequirements = course.CourseRequirements != null ? (await Task.WhenAll(course.CourseRequirements.Select(i => _courseRequirementMappingService.MapEntityToDto(i)))).ToList() : new List<CourseRequirementCreateDto>(),
                CategoryId = course.CategoryId,

            };

            return courseCreateDto;
        }

        public Course MapDtoToEntity(CourseCreateDto courseCreateDto)
        {
            var course = new Course
            {
                Id = courseCreateDto.Id,
                Title = courseCreateDto.Title,
                ShortDescription = courseCreateDto.ShortDescription,
                Description = courseCreateDto.Description,
                CategoryId = courseCreateDto.CategoryId,
                Language = courseCreateDto.Language.ToString(),
                Level = courseCreateDto.Level.ToString(),
                Price = courseCreateDto.Price,
                InstructorId = courseCreateDto.InstructorId,
                ImageUrl = courseCreateDto.CourseImage,
                LearningItems = courseCreateDto.LearningItems.Select(i => _learningItemMappingService.MapDtoToEntity(i)).ToList(),
                EnrollmentItems = courseCreateDto.EnrollmentItems.Select(i => _enrollmentItemMappingService.MapDtoToEntity(i)).ToList(),
                CourseRequirements = courseCreateDto.CourseRequirements?.Select(i => _courseRequirementMappingService.MapDtoToEntity(i)).ToList(),
            };

            // Assign the course to each item
            foreach (var item in course.LearningItems)
            {
                item.Course = course;
            }

            foreach (var item in course.EnrollmentItems)
            {
                item.Course = course;
            }
            if (course.CourseRequirements != null)
            {
                foreach (var item in course.CourseRequirements)
                {
                    item.Course = course;
                }
            }
            return course;
        }
    }

}
