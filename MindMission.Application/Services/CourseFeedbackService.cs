using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class CourseFeedbackService : ICourseFeedbackService
    {
        private readonly ICourseFeedbackRepository CourseFeedbackRepository;

        public CourseFeedbackService(ICourseFeedbackRepository _CourseFeedbackRepository)
        {
            CourseFeedbackRepository = _CourseFeedbackRepository;
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseId(int CourseId)
        {
            return await CourseFeedbackRepository.GetFeedbackByCourseId(CourseId);
        }
        public async Task<IQueryable<CourseFeedback>> GetFeedbackByInstructorId(string InstructorId)
        {
            return await CourseFeedbackRepository.GetFeedbackByInstructorId(InstructorId);
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseIdAndInstructorId(int CourseId, string InstructorId)
        {
            return await CourseFeedbackRepository.GetFeedbackByCourseIdAndInstructorId(CourseId, InstructorId);
        }

        public async Task<IQueryable<CourseFeedback>> GetTopCoursesRating(int NumberOfCourses)
        {
            return await CourseFeedbackRepository.GetTopCoursesRating(NumberOfCourses);
        }

        public async Task<IQueryable<CourseFeedback>> GetTopInstructorsRating(int NumberOfInstructor)
        {
            return await CourseFeedbackRepository.GetTopInstructorsRating(NumberOfInstructor);
        }

        public async Task<CourseFeedback> AddCourseFeedback(CourseFeedback CourseFeedback)
        {
            return await CourseFeedbackRepository.AddCourseFeedback(CourseFeedback);
        }

        public async Task<CourseFeedback?> UpdateCourseFeedback(int Id, CourseFeedback CourseFeedback)
        {
            return await CourseFeedbackRepository.UpdateCourseFeedback(Id, CourseFeedback);
        }
    }
}
