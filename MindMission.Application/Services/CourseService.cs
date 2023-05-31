using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _context;

        public CourseService(ICourseRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Course>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync(params Expression<Func<Course, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Course> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Course> GetByIdAsync(int id, params Expression<Func<Course, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
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

        public async Task<IQueryable<Course>> GetAllByCategoryAsync(int categoryId)
        {
            return await _context.GetAllByCategoryAsync(categoryId);
        }

        public async Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId)
        {
            return await _context.GetRelatedCoursesAsync(courseId);
        }

        public async Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            return await _context.GetAllByInstructorAsync(instructorId);
        }

        public async Task<IQueryable<Course>> GetTopRatedCoursesAsync(int topNumber)
        {
            return await _context.GetTopRatedCoursesAsync(topNumber);
        }

        public async Task<IQueryable<Course>> GetRecentCoursesAsync(int recentNumber)
        {
            return await _context.GetRecentCoursesAsync(recentNumber);
        }
    }
}