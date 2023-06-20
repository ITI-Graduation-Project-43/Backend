using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services
{
    public class SiteCouponService : ISiteCouponService
    {
        private readonly ISiteCouponRepository _context;

        public SiteCouponService(ISiteCouponRepository context)
        {
            _context = context;
        }
        public async Task<SiteCoupon> AddAsync(SiteCoupon entity)
        {
            return await _context.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.DeleteAsync(id);
        }

        public async Task<IQueryable<SiteCoupon>> GetAllAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task<IEnumerable<SiteCoupon>> GetAllAsync(params Expression<Func<SiteCoupon, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public async Task<SiteCoupon> GetByCode(string code)
        {
            return await _context.GetByCode(code);
        }

        public async Task<SiteCoupon> GetByIdAsync(int id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task<SiteCoupon> GetByIdAsync(int id, params Expression<Func<SiteCoupon, object>>[] IncludeProperties)
        {
            return await _context.GetByIdAsync(id, IncludeProperties);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _context.SoftDeleteAsync(id);
        }

        public async Task<SiteCoupon> UpdateAsync(SiteCoupon entity)
        {
            return await _context.UpdateAsync(entity);
        }

        public async Task<SiteCoupon> UpdatePartialAsync(int id, SiteCoupon entity)
        {
            return await _context.UpdatePartialAsync(id, entity);
        }
    }
}
