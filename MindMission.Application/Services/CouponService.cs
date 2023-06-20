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

		public CouponService(ICouponRepository context):base(context)
		{
			_context = context;
		}

		public async Task<Coupon> getCouponByCode(string code)
		{
			return await _context.getCouponByCode(code);
		}

		public Task<Coupon> getCouponByCodeAndCourse(string code, int courseId)
		{
			return _context.getCouponByCodeAndCourse(code, courseId);
		}

		public async Task<IQueryable<Coupon>> getCouponsByCourse(int courseId)
		{
			return await _context.getCouponsByCourse(courseId);
		}


	}
}
