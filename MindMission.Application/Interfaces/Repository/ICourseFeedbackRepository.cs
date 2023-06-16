using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface ICourseFeedbackRepository
    {
        Task<IQueryable<CourseFeedback>> GetFeedbackByCourseId(int CourseId);
        Task<IQueryable<CourseFeedback>> GetFeedbackByInstructorId(string InstructorId);
        Task<IQueryable<CourseFeedback>> GetFeedbackByCourseIdAndInstructorId(int CourseId, string InstructorId);
        Task<IQueryable<CourseFeedback>> GetTopCoursesRating(int NumberOfCourses);
        Task<IQueryable<CourseFeedback>> GetTopInstructorsRating(int NumberOfInstructor);
        Task<CourseFeedback> AddCourseFeedback(CourseFeedback CourseFeedback);
        Task<CourseFeedback?> UpdateCourseFeedback(int Id, CourseFeedback CourseFeedback);
        Task SoftDeleteAsync(int id);

    }
}
