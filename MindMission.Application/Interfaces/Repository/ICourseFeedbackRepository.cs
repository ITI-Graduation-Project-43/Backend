using MindMission.Application.DTOs.PostDtos;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface ICourseFeedbackRepository
    {
        Task<int> GetTotalCountAsync();

        IQueryable<CourseFeedback> GetFeedbackByCourseId(int courseId, int pageNumber, int pageSize);
        IQueryable<CourseFeedback> GetFeedbackByInstructorId(string instructorId, int pageNumber, int pageSize);
        IQueryable<CourseFeedback> GetFeedbackByCourseIdAndInstructorId(int courseId, string instructorId, int pageNumber, int pageSize);
        Task<AddCourseFeedbackDto> GetFeedbackByCourseIdAndStudentId(int courseId, string studentId);

        IQueryable<CourseFeedback> GetTopCoursesRating(int numberOfCourses, int pageNumber, int pageSize);
        IQueryable<CourseFeedback> GetTopInstructorsRating(int numberOfInstructor, int pageNumber, int pageSize);
        Task<CourseFeedback> AddCourseFeedback(CourseFeedback courseFeedback);
        Task<CourseFeedback?> UpdateCourseFeedback(int id, CourseFeedback courseFeedback);
        Task SoftDeleteAsync(int id);

    }
}
