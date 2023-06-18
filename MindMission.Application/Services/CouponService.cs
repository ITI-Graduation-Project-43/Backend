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
	public class CouponService : ICouponService
	{
		private readonly ICouponRepository _context;

		public CouponService(ICouponRepository context)
		{
			_context = context;
		}

		public async Task<Coupon> AddAsync(Coupon entity)
		{
			await _context.AddAsync(entity);
			return entity;
		}

		public async Task DeleteAsync(int id)
		{
			await _context.DeleteAsync(id);
		}

		public async Task<IQueryable<Coupon>> GetAllAsync()
		{
			return await _context.GetAllAsync();
		}

		public async Task<IEnumerable<Coupon>> GetAllAsync(params Expression<Func<Coupon, object>>[] IncludeProperties)
		{
			return await _context.GetAllAsync(IncludeProperties);
		}

		public async Task<Coupon> GetByIdAsync(int id)
		{
			return await _context.GetByIdAsync(id);
		}

		public async Task<Coupon> GetByIdAsync(int id, params Expression<Func<Coupon, object>>[] IncludeProperties)
		{
			return await _context.GetByIdAsync(id, IncludeProperties);
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

		public async Task SoftDeleteAsync(int id)
		{
			await _context.SoftDeleteAsync(id);
		}

		public async Task<Coupon> UpdateAsync(Coupon entity)
		{
			return await _context.UpdateAsync(entity);
		}

		public async Task<Coupon> UpdatePartialAsync(int id, Coupon entity)
		{
			return await _context.UpdatePartialAsync(id, entity);
		}
	}
}
