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
        public async Task<IActionResult> GetAllCoupons([FromQuery] PaginationDto pagination)
        {
            return Ok(_siteCouponService.GetAllAsync(pagination.PageNumber, pagination.PageSize));
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

        [HttpPut]
        public async Task<IActionResult> EditSiteCoupon(SiteCoupon siteCoupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseObjectFactory
                    .CreateResponseObject(false, "The provided data is not valid.", new List<SiteCoupon>(), 1, 1, 1));
            }

            var coupon = await _siteCouponService.UpdateAsync(siteCoupon);

            if(coupon is null)
                return BadRequest(ResponseObjectFactory
                    .CreateResponseObject(false, "coupon not found.", new List<SiteCoupon>(), 1, 1, 1));

            var response = (ResponseObjectFactory
                    .CreateResponseObject(true, "coupon added successfully.", new List<SiteCoupon> { coupon }, 1, 1, 1));
            return CreatedAtAction(nameof(GetSiteCouponByCode), new { code = siteCoupon.Code }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var coupon = await _siteCouponService.GetByIdAsync(id);

            if (coupon is null)
                return BadRequest(ResponseObjectFactory
                    .CreateResponseObject(false, "coupon not found.", new List<SiteCoupon>(), 1, 1, 1));

            await _siteCouponService.DeleteAsync(id);
            return Ok(ResponseObjectFactory
                    .CreateResponseObject(true, "coupon deleted successfully.", new List<SiteCoupon>(), 1, 1, 1));
        }

    }
}
