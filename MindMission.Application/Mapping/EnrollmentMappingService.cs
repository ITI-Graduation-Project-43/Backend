using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class EnrollmentMappingService : IMappingService<Enrollment, EnrollmentDto>
    {
        private readonly ICourseService _CourseService;
        private readonly IStudentService _StudentService;

        public EnrollmentMappingService(ICourseService courseService, IStudentService studentService)
        {
            _CourseService = courseService;
            _StudentService = studentService;
        }

        public async Task<EnrollmentDto> MapEntityToDto(Enrollment Enrollment)
        {
            var EnrollmentDto = new EnrollmentDto
            {
                Id = Enrollment.Id,
                EnrollmentDate = Enrollment.EnrollmentDate
            };
            var course = await _CourseService.GetByIdAsync(Enrollment.CourseId);
            if (course != null)
            {
                EnrollmentDto.CourseId = course.Id;
                EnrollmentDto.CourseTitle = course.Title;
                EnrollmentDto.CourseShortDescription = course.ShortDescription;
                EnrollmentDto.CourseImageUrl = course.ImageUrl;
                EnrollmentDto.CoursePrice = course.Price;
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
                Id = EnrollmentDto.Id,
                EnrollmentDate = EnrollmentDto.EnrollmentDate,
                CourseId = EnrollmentDto.CourseId,
                StudentId = EnrollmentDto.StudentId
            };
        }
    }
}