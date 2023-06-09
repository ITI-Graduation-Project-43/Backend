using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _context;

        public StudentService(IStudentRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Student>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync(params Expression<Func<Student, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Student> GetByIdAsync(string id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Student> GetByIdAsync(string id, params Expression<Func<Student, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        public Task<Student> AddAsync(Student entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Student entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(string id)
        {
            return _context.DeleteAsync(id);
        }

        public Task<IQueryable<Student>> GetRecentStudentEnrollmentAsync(int recentNumber, int courseId)
        {
            return _context.GetRecentStudentEnrollmentAsync(recentNumber, courseId);
        }
    }
}