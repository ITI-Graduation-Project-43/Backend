using MindMission.Application.DTOs.PostDtos;
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
        public async Task<int> GetTotalCountAsync()
        {
            return await CourseFeedbackRepository.GetTotalCountAsync();
        }
        public Task SoftDeleteAsync(int id)
        {
            return CourseFeedbackRepository.SoftDeleteAsync(id);
        }
        public IQueryable<CourseFeedback> GetFeedbackByCourseId(int courseId, int pageNumber, int pageSize)
        {
            return CourseFeedbackRepository.GetFeedbackByCourseId(courseId, pageNumber, pageSize);
        }
        public IQueryable<CourseFeedback> GetFeedbackByInstructorId(string instructorId, int pageNumber, int pageSize)
        {
            return CourseFeedbackRepository.GetFeedbackByInstructorId(instructorId, pageNumber, pageSize);
        }

        public IQueryable<CourseFeedback> GetFeedbackByCourseIdAndInstructorId(int courseId, string instructorId, int pageNumber, int pageSize)
        {
            return CourseFeedbackRepository.GetFeedbackByCourseIdAndInstructorId(courseId, instructorId, pageNumber, pageSize);
        }

        public IQueryable<CourseFeedback> GetTopCoursesRating(int numberOfCourses, int pageNumber, int pageSize)
        {
            return CourseFeedbackRepository.GetTopCoursesRating(numberOfCourses, pageNumber, pageSize);
        }

        public IQueryable<CourseFeedback> GetTopInstructorsRating(int numberOfInstructor, int pageNumber, int pageSize)
        {
            return CourseFeedbackRepository.GetTopInstructorsRating(numberOfInstructor, pageNumber, pageSize);
        }

        public async Task<CourseFeedback> AddCourseFeedback(CourseFeedback courseFeedback)
        {
            return await CourseFeedbackRepository.AddCourseFeedback(courseFeedback);
        }

        public async Task<CourseFeedback?> UpdateCourseFeedback(int Id, CourseFeedback courseFeedback)
        {
            return await CourseFeedbackRepository.UpdateCourseFeedback(Id, courseFeedback);
        }

        public async Task<AddCourseFeedbackDto> GetFeedbackByCourseIdAndStudentId(int courseId, string studentId)
        {
            return await CourseFeedbackRepository.GetFeedbackByCourseIdAndStudentId(courseId, studentId);
        }
    }
}
