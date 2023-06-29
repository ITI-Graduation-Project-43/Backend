using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;


namespace MindMission.Application.Interfaces.Repository
{
    public interface ICouponRepository : IRepository<Coupon, int>
    {
        Task<Coupon> GetCouponByCode(string code);
        IQueryable<Coupon> GetCouponsByCourse(int courseId, int pageNumber, int pageSize);
        Task<Coupon> GetCouponByCodeAndCourse(string code, int courseId);
    }
}
