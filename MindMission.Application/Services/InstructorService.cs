using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _context;
        public InstructorService(IInstructorRepository context)
        {
            _context = context;
        }
        public Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Instructor> GetByIdAsync(string id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Instructor> AddAsync(Instructor entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Instructor entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(string id)
        {
            return _context.DeleteAsync(id);
        }
    }
}
