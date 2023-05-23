using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _context;
        public StudentService(IStudentRepository context)
        {
            _context = context;
        }
        public Task<Student> AddAsync(Student entity)
        {
            return _context.AddAsync(entity);
        }

        public Task DeleteAsync(string id)
        {
            return _context.DeleteAsync(id);
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Student> GetByIdAsync(string id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task UpdateAsync(Student entity)
        {
            return _context.UpdateAsync(entity);
        }
    }
}
