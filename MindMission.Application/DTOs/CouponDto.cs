using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
	public class CouponDto : IDtoWithId<int>
	{
		public int Id { get; set; }

		[Required,StringLength(50,MinimumLength =5)]
		public string Code { get; set; } = string.Empty;

		[Required,Range(0d,100d)]
		public decimal? Discount { get; set; }
		public DateTime ExpiresAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public int? CourseId { get; set; }
		public bool IsDeleted { get; set; }
	}
}
