using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _context;
        public CourseService(ICourseRepository context)
        {
            _context = context;
        }
        public Task<IEnumerable<Course>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Course> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Course> AddAsync(Course entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Course entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        public async Task<Course> GetByNameAsync(string name)
        {
            return await _context.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Course>> GetAllByCategoryAsync(int categoryId)
        {
            // Get all courses by category
            var courses = await _context.GetAllAsync();
            return courses.Where(c => c.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Course>> GetRelatedCoursesAsync(int courseId)
        {
            // Get the category of the course
            var course = await _context.GetByIdAsync(courseId) ?? throw new Exception($"Course with id {courseId} not found.");

            // Get all courses in the same category
            var courses = await GetAllByCategoryAsync(course.CategoryId);

            // Exclude the current course
            return courses.Where(c => c.Id != courseId);
        }

        public async Task<IEnumerable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            // Get all courses by instructor
            var courses = await _context.GetAllAsync();
            return courses.Where(c => c.InstructorId == instructorId);
        }

        public async Task<IEnumerable<Course>> GetTopRatedCoursesAsync(int topNumber)
        {
            // Get top rated courses
            var courses = await _context.GetAllAsync();
            return courses.OrderByDescending(c => c.AvgReview).Take(topNumber);
        }

        public async Task<IEnumerable<Course>> GetRecentCoursesAsync(int recentNumber)
        {
            // Get recent courses
            var courses = await _context.GetAllAsync();
            return courses.OrderByDescending(c => c.CreatedAt).Take(recentNumber);
        }

    }
}
