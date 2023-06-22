using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Domain.Models;
using MindMission.Domain.Stripe.StripeModels;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IStripeService _stripeService;
        private readonly IPaymentMappingService _paymentMappingService;
		private readonly ICouponService _couponService;

		public PaymentController(IStripeService stripeService, 
            IPaymentMappingService paymentMappingService,
            ICouponService couponService)
        {
            _stripeService = stripeService;
            _paymentMappingService = paymentMappingService;
			_couponService = couponService;
		}

        //////
        //User API to return the Id of a success payment process
        //////
        [HttpPost]
        public async Task<IActionResult> StripePayment(PaymentDto paymentDto)
        {
            if (ModelState.IsValid)
            {
                long totalPrice = await _stripeService.GetTotalPrice(paymentDto.CoursesIds, paymentDto.SiteCoupon, paymentDto.CoursesCoupons);

                    try
                    {
                        AddStripeCustomer customer = _paymentMappingService
                            .MapDtoToAddStripeCustomer(paymentDto);
                        StripeCustomer stripeCustomer = await _stripeService.GetStripeCustomer(customer);

                        AddStripePayment addedStripePayment = _paymentMappingService
                            .MapDtoToAddStripePayment(stripeCustomer, paymentDto, totalPrice);

                        var response = ResponseObjectFactory.CreateResponseObject(true, "successfull payment process", new List<StripePayment> { await _stripeService.GetStripePayment(addedStripePayment) }, 1, 1);

                        return Ok(response);
                    }
                    catch
                    {
                        return StatusCode(500, "Internal Server Error");
                    }
                
            }
			return BadRequest(ModelState);
        }
    }
}