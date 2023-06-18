using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Repository
{
	public interface ICouponRepository : IRepository<Coupon,int>
	{
		Task<Coupon> getCouponByCode(string code);
		Task<IQueryable<Coupon>> getCouponsByCourse(int courseId);
		Task<Coupon> getCouponByCodeAndCourse(string code, int courseId);
	}
}
