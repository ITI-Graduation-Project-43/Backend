using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _context;
        public AdminService(IAdminRepository context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> GetAllAsync(params Expression<Func<Admin, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        Task<Admin> IRepository<Admin, int>.AddAsync(Admin entity)
        {
            return _context.AddAsync(entity);
        }

        Task IRepository<Admin, int>.DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        Task<IEnumerable<Admin>> IRepository<Admin, int>.GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        Task<Admin> IRepository<Admin, int>.GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        Task IRepository<Admin, int>.UpdateAsync(Admin entity)
        {
            return _context.UpdateAsync(entity);
        }
    }
}
