using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class CouponRepository : Repository<Coupon, int>, ICouponRepository
    {
        private readonly MindMissionDbContext _context;

        public CouponRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Coupon> GetCouponByCodeAndCourse(string code, int courseId) =>
            await _context.Coupons.FirstOrDefaultAsync(co => co.Code == code && co.CourseId == courseId)
            ?? throw new KeyNotFoundException($"No coupon of course Id {courseId} with id {code} found.");

        public async Task<Coupon> GetCouponByCode(string code) =>
            await _context.Coupons.FirstOrDefaultAsync(co => co.Code == code);

        public IQueryable<Coupon> GetCouponsByCourse(int courseId, int pageNumber, int pageSize) =>
            _context.Coupons.Where(co => co.CourseId == courseId)
            .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
            ?? throw new KeyNotFoundException($"coupon of course Id {courseId} not found");
    }
}
