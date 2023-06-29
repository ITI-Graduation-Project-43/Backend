using Microsoft.EntityFrameworkCore;
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
        public Task SoftDeleteAsync(int id)
        {
            return CourseFeedbackRepository.SoftDeleteAsync(id);
        }
        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseId(int courseId)
        {
            return await CourseFeedbackRepository.GetFeedbackByCourseId(courseId);
        }
        public async Task<IQueryable<CourseFeedback>> GetFeedbackByInstructorId(string instructorId)
        {
            return await CourseFeedbackRepository.GetFeedbackByInstructorId(instructorId);
        }

        public async Task<IQueryable<CourseFeedback>> GetFeedbackByCourseIdAndInstructorId(int courseId, string instructorId)
        {
            return await CourseFeedbackRepository.GetFeedbackByCourseIdAndInstructorId(courseId, instructorId);
        }

        public async Task<IQueryable<CourseFeedback>> GetTopCoursesRating(int numberOfCourses)
        {
            return await CourseFeedbackRepository.GetTopCoursesRating(numberOfCourses);
        }

        public async Task<IQueryable<CourseFeedback>> GetTopInstructorsRating(int numberOfInstructor)
        {
            return await CourseFeedbackRepository.GetTopInstructorsRating(numberOfInstructor);
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
