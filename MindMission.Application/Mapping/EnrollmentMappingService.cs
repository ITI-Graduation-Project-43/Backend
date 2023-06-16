using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class EnrollmentMappingService : IMappingService<Enrollment, EnrollmentDto>
    {
        private readonly ICourseService _CourseService;
        private readonly IStudentService _StudentService;

        public EnrollmentMappingService(ICourseService courseService, IStudentService studentService)
        {
            _CourseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _StudentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        public async Task<EnrollmentDto> MapEntityToDto(Enrollment Enrollment)
        {
            var EnrollmentDto = new EnrollmentDto
            {
                Id = Enrollment.Id,
                EnrollmentDate = Enrollment.EnrollmentDate
            };
            var course = await _CourseService.GetByIdAsync(Enrollment.CourseId,en=>en.Category);
            if (course != null)
            {
                EnrollmentDto.CourseId = course.Id;
                EnrollmentDto.CourseTitle = course.Title;
                EnrollmentDto.CourseDescription = course.Description;
                EnrollmentDto.CourseImageUrl = course.ImageUrl;
                EnrollmentDto.CoursePrice = course.Price;
                EnrollmentDto.CourseAvgReview = course.AvgReview;
                EnrollmentDto.CategoryName = course.Category.Name;
            }
            var student = await _StudentService.GetByIdAsync(Enrollment.StudentId);
            if (student != null)
            {
                EnrollmentDto.StudentId = student.Id;
                EnrollmentDto.StudentName = student.FirstName + " " + student.LastName;
            }
            return EnrollmentDto;
        }

        public Enrollment MapDtoToEntity(EnrollmentDto EnrollmentDto)
        {
            return new Enrollment
            {
                EnrollmentDate = EnrollmentDto.EnrollmentDate,
                CourseId = EnrollmentDto.CourseId,
                StudentId = EnrollmentDto.StudentId
            };
        }
    }
}