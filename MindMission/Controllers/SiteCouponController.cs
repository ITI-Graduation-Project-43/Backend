using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteCouponController : ControllerBase
    {
        private readonly ISiteCouponService _siteCouponService;

        public SiteCouponController(ISiteCouponService siteCouponService)
        {
            _siteCouponService = siteCouponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            return Ok(await _siteCouponService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetSiteCouponByCode([FromRoute] string code)
        {
            if(code == null)
            {
                return BadRequest();
            }

            var coupon = await _siteCouponService.GetByCode(code);
            if(coupon == null)
            {
                var nullResultResponse = ResponseObjectFactory
                    .CreateResponseObject(false, "coupon not found", new List<SiteCoupon>(), 1, 1, 0);
                return BadRequest(nullResultResponse);
            }

            var siteCoupons = new List<SiteCoupon> { coupon };
            var successResponse = ResponseObjectFactory
                    .CreateResponseObject(true, "coupon retrieved successfully", siteCoupons, 1, 1, 1);
            return Ok(successResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddSiteCoupon(SiteCoupon siteCoupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseObjectFactory
                    .CreateResponseObject(false, "The provided data is not valid.",new List<SiteCoupon>(), 1, 1, 1));
            }

            var coupon = await _siteCouponService.AddAsync(siteCoupon);
            var response = (ResponseObjectFactory
                    .CreateResponseObject(true, "coupon added successfully.", new List<SiteCoupon> { coupon }, 1, 1, 1));
            return CreatedAtAction(nameof(GetSiteCouponByCode), new { code = siteCoupon.Code }, response);
        }   
    }
}
