using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Stripe.StripeModels;
using MindMission.Domain.Stripe.CustomValidationAttributes;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using System.Net;
using MindMission.Domain.Models;

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
        //Adding Customer to stripe by checking card details
        //////
        private async Task<StripeCustomer> AddStripeCustomer(AddStripeCustomer customer)
        {
            StripeCustomer stripeCustomer = await _stripeService.AddStripeCustomerAsync(customer);
            ReturnedCutomerId.CustomerId = stripeCustomer.CustomerId;
            return stripeCustomer;        
        }

        //////
        //Adding Charge to stripe by added customer Id
        //////
        private async Task<StripePayment> AddStripePayment(AddStripePayment payment)
        {
            StripePayment stripePayment = await _stripeService.AddStripePaymentAsync(payment);
            return stripePayment;
        }


        //////
        //User API to return the Id of a success payment process  
        //////
        [HttpPost]
        public async Task<IActionResult> StripePayment(PaymentDto paymentDto)
        {
            Course EnrolledCourse = await _stripeService.GetEnrolledCourse(paymentDto.CourseId);

            if (EnrolledCourse != null)
            {
                try
                {
                    AddStripeCustomer customer = _paymentMappingService
                        .MapDtoToAddStripeCustomer(paymentDto);
                    StripeCustomer stripeCustomer = await AddStripeCustomer(customer);

                    AddStripePayment addedStripePayment = _paymentMappingService
                        .MapDtoToAddStripePayment(stripeCustomer, paymentDto, EnrolledCourse.Price);

                    return Ok(await AddStripePayment(addedStripePayment));
                }
                catch
                {
                    string ErrorMsg = "An unexpected error occured while processing your request";
                    return StatusCode(500, ErrorMsg);
                }
            }
            return BadRequest("Non-Existed Course");
        }
    }
}
