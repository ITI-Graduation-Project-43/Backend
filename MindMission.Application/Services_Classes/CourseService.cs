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

    }
}
