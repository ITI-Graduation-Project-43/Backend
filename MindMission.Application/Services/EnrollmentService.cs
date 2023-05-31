using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _context;

        public EnrollmentService(IEnrollmentRepository context)
        {
            _context = context;
        }

        public Task<Enrollment> AddAsync(Enrollment entity)
        {
            return _context.AddAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        public Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync(params Expression<Func<Enrollment, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<IEnumerable<Enrollment>> GetAllByCourseIdAsync(int courseId)
        {
            return _context.GetAllByCourseIdAsync(courseId);
        }

        public Task<IEnumerable<Enrollment>> GetAllByStudentIdAsync(string studentId)
        {
            return _context.GetAllByStudentIdAsync(studentId);
        }

        public Task<Enrollment> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public async Task<Enrollment> GetByIdAsync(int id, params Expression<Func<Enrollment, object>>[] IncludeProperties)
        {
            return await _context.GetByIdAsync(id, IncludeProperties);
        }

        public Task UpdateAsync(Enrollment entity)
        {
            return _context.UpdateAsync(entity);
        }
    }
}