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
    public class CouponService : Service<Coupon, int>, ICouponService
    {
        private readonly ICouponRepository _context;

        public CouponService(ICouponRepository context) : base(context)
        {
            _context = context;
        }

        public async Task<Coupon> GetCouponByCode(string code)
        {
            return await _context.GetCouponByCode(code);
        }

        public Task<Coupon> GetCouponByCodeAndCourse(string code, int courseId)
        {
            return _context.GetCouponByCodeAndCourse(code, courseId);
        }

        public IQueryable<Coupon> GetCouponsByCourse(int courseId, int pageNumber, int pageSize)
        {
            return _context.GetCouponsByCourse(courseId, pageNumber, pageSize);
        }


    }
}
