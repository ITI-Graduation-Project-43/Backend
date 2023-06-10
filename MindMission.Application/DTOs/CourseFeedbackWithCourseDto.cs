using MindMission.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs
{
    public class CourseFeedbackWithCourseDto
    {
        public string CourseName { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentImage { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public decimal InstructorRating { get; set; }
        public decimal CourseRating { get; set; }
        public string FeedbackText { get; set; } = string.Empty;
    }
}
