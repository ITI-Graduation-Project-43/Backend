using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
	public class CouponMappingService : IMappingService<Coupon, CouponDto>
	{
		public Coupon MapDtoToEntity(CouponDto dto)
		{
			return new Coupon()
			{
				Code = dto.Code,
				Discount= dto.Discount,
				CreatedAt= dto.CreatedAt,
				ExpiresAt= dto.ExpiresAt,
				IsDeleted = dto.IsDeleted,
				CourseId= dto.CourseId,
			};
		}

		public Task<CouponDto> MapEntityToDto(Coupon entity)
		{
			return Task.FromResult(new CouponDto()
			{
				Id= entity.Id,
				Code = entity.Code,
				Discount = entity.Discount,
				CreatedAt = entity.CreatedAt,
				ExpiresAt = entity.ExpiresAt,
				IsDeleted = entity.IsDeleted,
				CourseId = entity.CourseId,
			});
		}
	}
}
