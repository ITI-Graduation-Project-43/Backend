using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _context;
        public AdminService(IAdminRepository context)
        {
            _context = context;
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
