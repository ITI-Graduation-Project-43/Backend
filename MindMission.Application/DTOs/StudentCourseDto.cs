using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs
{
    public class StudentCourseDto
    {

        public int CourseId { get; set; }
        public decimal Price { get; set; }

        public string CourseTitle { get; set; } = string.Empty;
        public string CourseImageUrl { get; set; } = string.Empty;
        public decimal? CourseAvgReview { get; set; }

        public int CourseNoOfStudents { get; set; }
        public decimal? CourseDiscount { get; set; }

        public string CourseCategoryName { get; set; } = string.Empty;

        public string InstructorId { get; set; } = string.Empty;
        public string InstructorFirstName { get; set; } = string.Empty;
        public string InstructorLastName { get; set; } = string.Empty;

        public string InstructorProfilePicture { get; set; } = string.Empty;

        public IList<CustomStudentDto> Student { get; set; }


    }
    public class CustomStudentDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentFirstName { get; set; } = string.Empty;
        public string StudentLastName { get; set; } = string.Empty;

        public string? StudentProfilePicture { get; set; } = string.Empty;

    }
}
