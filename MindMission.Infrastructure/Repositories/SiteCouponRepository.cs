using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class SiteCouponRepository : Repository<SiteCoupon, int>, ISiteCouponRepository
    {
        private readonly MindMissionDbContext _context;

        public SiteCouponRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SiteCoupon> GetByCode(string code)
        {
            var cutOffDate = DateTime.Now;
            return await _context.SiteCoupons.FirstOrDefaultAsync(sc => sc.Code == code && sc.ExpiresAt >= cutOffDate)
                ?? throw new KeyNotFoundException($"No coupon with code {code} found.");
        }
    }
}
