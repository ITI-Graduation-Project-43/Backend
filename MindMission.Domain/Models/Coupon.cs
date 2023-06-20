using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Models
{
	[Index("Code",IsUnique =true)]
	public class Coupon : IEntity<int>, ISoftDeletable
	{
		[Key]
		public int Id { get; set; }

		[Required,StringLength(50,MinimumLength =5)]
		public string Code { get; set; } = string.Empty;

		[Required,Range(0d,100d)]
		public decimal? Discount { get; set; }

		[Required]
		public DateTime ExpiresAt { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		[ForeignKey("Course")]
		public int? CourseId { get; set; }
		public Course? Course { get; set; }
		public bool IsDeleted { get; set; }
	}
}
