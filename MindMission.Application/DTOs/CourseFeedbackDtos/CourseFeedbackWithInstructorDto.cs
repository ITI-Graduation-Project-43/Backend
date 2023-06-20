using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs.CourseFeedbackDtos
{
    public class CourseFeedbackWithInstructorDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentImage { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public string InstructorImage { get; set; } = string.Empty;
        public decimal InstructorRating { get; set; }
        public decimal CourseRating { get; set; }
        public string FeedbackText { get; set; } = string.Empty;
    }
}
