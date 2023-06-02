using Microsoft.AspNetCore.JsonPatch;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
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
        Task<IQueryable<Admin>> IRepository<Admin, int>.GetAllAsync()
        {
            return _context.GetAllAsync();
        }
        public async Task<IEnumerable<Admin>> GetAllAsync(params Expression<Func<Admin, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }
        Task<Admin> IRepository<Admin, int>.GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }
        public Task<Admin> GetByIdAsync(int id, params Expression<Func<Admin, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        Task<Admin> IRepository<Admin, int>.AddAsync(Admin entity)
        {
            return _context.AddAsync(entity);
        }

        Task IRepository<Admin, int>.DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }



        Task IRepository<Admin, int>.UpdateAsync(Admin entity)
        {
            return _context.UpdateAsync(entity);
        }



    }
}