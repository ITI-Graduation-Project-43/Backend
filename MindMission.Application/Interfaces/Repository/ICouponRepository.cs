using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;


namespace MindMission.Application.Interfaces.Repository
{
    public interface ICouponRepository : IRepository<Coupon, int>
    {
        Task<Coupon> getCouponByCode(string code);
        Task<IQueryable<Coupon>> getCouponsByCourse(int courseId);
        Task<Coupon> getCouponByCodeAndCourse(string code, int courseId);
    }
}
