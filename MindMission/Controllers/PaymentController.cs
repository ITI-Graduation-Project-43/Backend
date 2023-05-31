using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTOs;
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

        public PaymentController(IStripeService stripeService, IPaymentMappingService paymentMappingService)
        {
            _stripeService = stripeService;
            _paymentMappingService = paymentMappingService;
        }

        //////
        //User API to return the Id of a success payment process
        //////
        [HttpPost]
        public async Task<IActionResult> StripePayment(PaymentDto paymentDto)
        {
            if (ModelState.IsValid)
            {
                Course EnrolledCourse = await _stripeService.GetEnrolledCourse(paymentDto.CourseId);

                if (EnrolledCourse != null)
                {
                    try
                    {
                        AddStripeCustomer customer = _paymentMappingService
                            .MapDtoToAddStripeCustomer(paymentDto);
                        StripeCustomer stripeCustomer = await _stripeService.GetStripeCustomer(customer);

                        AddStripePayment addedStripePayment = _paymentMappingService
                            .MapDtoToAddStripePayment(stripeCustomer, paymentDto, EnrolledCourse.Price);

                        return Ok(await _stripeService.GetStripePayment(addedStripePayment));
                    }
                    catch
                    {
                        return StatusCode(500, "Internal Server Error");
                    }
                }
                return BadRequest("Non-Existed Course");
            }
            return BadRequest(ModelState);
        }
    }
}