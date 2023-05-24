using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _context;
        public PermissionService(IPermissionRepository context)
        {
            _context = context;
        }
        public Task<IEnumerable<Permission>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Permission> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Permission> AddAsync(Permission entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Permission entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync(params Expression<Func<Permission, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }
    }
}
