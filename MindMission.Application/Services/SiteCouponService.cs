using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services
{
    public class SiteCouponService : Service<SiteCoupon, int>, ISiteCouponService
    {
        private readonly ISiteCouponRepository _context;

        public SiteCouponService(ISiteCouponRepository context) : base(context)
        {
            _context = context;
        }

        public async Task<SiteCoupon> GetByCode(string code)
        {
            return await _context.GetByCode(code);
        }

    }
}
