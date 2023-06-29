using MindMission.Application.DTOs.PostDtos;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface ICourseFeedbackRepository
    {
        Task<IQueryable<CourseFeedback>> GetFeedbackByCourseId(int courseId);
        Task<IQueryable<CourseFeedback>> GetFeedbackByInstructorId(string instructorId);
        Task<IQueryable<CourseFeedback>> GetFeedbackByCourseIdAndInstructorId(int courseId, string instructorId);
        Task<AddCourseFeedbackDto> GetFeedbackByCourseIdAndStudentId(int courseId, string studentId);

        Task<IQueryable<CourseFeedback>> GetTopCoursesRating(int numberOfCourses);
        Task<IQueryable<CourseFeedback>> GetTopInstructorsRating(int numberOfInstructor);
        Task<CourseFeedback> AddCourseFeedback(CourseFeedback courseFeedback);
        Task<CourseFeedback?> UpdateCourseFeedback(int id, CourseFeedback courseFeedback);
        Task SoftDeleteAsync(int id);

    }
}
