using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class EnrollmentMappingService : IMappingService<Enrollment, EnrollmentDto>
    {


        public async Task<EnrollmentDto> MapEntityToDto(Enrollment Enrollment)
        {
            var EnrollmentDto = new EnrollmentDto
            {
                Id = Enrollment.Id,
                EnrollmentDate = Enrollment.EnrollmentDate,
                CourseId = Enrollment.Course.Id,
                CourseTitle = Enrollment.Course.Title,
                CourseDescription = Enrollment.Course.Description,
                CourseImageUrl = Enrollment.Course.ImageUrl,
                CoursePrice = Enrollment.Course.Price,
                CourseAvgReview = Enrollment.Course.AvgReview,
                CategoryId = Enrollment.Course.CategoryId,
                CategoryName = Enrollment.Course.Category.Name,
                InstructorId = Enrollment.Course.Instructor.Id,
                InstructorName = Enrollment.Course?.Instructor?.FirstName + " " + Enrollment.Course?.Instructor?.LastName,
                InstructorProfilePicture = Enrollment.Course?.Instructor?.ProfilePicture,
                CourseNoOfEnrollment = Enrollment.Course?.NoOfStudents,
                StudentId = Enrollment.Student.Id,
                StudentName = Enrollment.Student.FullName
            };

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