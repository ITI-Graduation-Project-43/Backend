using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : BaseController<Coupon, CouponDto, int>
    {
        private readonly ICouponService _couponService;
        private readonly ICourseService _courseService;

        public CouponController(ICouponService couponService,
            IMappingService<Coupon, CouponDto> mappingService,
            ICourseService courseService) : base(mappingService)
        {
            _couponService = couponService;
            _courseService = courseService;
        }

        [HttpGet("course/{code}")]
        public async Task<IActionResult> GetCouponByCodeAndCourse(string code, [FromQuery] int courseId)
        {
            return await GetEntityResponse(() => _couponService.GetCouponByCodeAndCourse(code, courseId), "Coupon");
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetCouponByCode(string code)
        {
            return await GetEntityResponse(() => _couponService.GetCouponByCode(code), "Coupon");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            return await GetEntityResponse(() => _couponService.GetByIdAsync(id), "Coupon");
        }

        [HttpPost]
        public async Task<IActionResult> AddCoupon(CouponDto couponDto)
        {
            var course = await _courseService.GetByIdAsync(couponDto.CourseId.Value);
            return await AddEntityResponse(_couponService.AddAsync, couponDto, "Coupon", nameof(GetCouponById));
        }
    }
}
