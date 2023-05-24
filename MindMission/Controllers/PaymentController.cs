using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Stripe.StripeModels;
using MindMission.Domain.Stripe.CustomValidationAttributes;
using MindMission.Application.DTOs;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        public PaymentController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        private async Task<StripeCustomer> AddStripeCustomer(AddStripeCustomer customer)
        {
            StripeCustomer stripeCustomer = await _stripeService.AddStripeCustomerAsync(customer);
            ReturnedCutomerId.CustomerId = stripeCustomer.CustomerId;
            return stripeCustomer;        
        }

        private async Task<StripePayment> AddStripePayment(AddStripePayment payment)
        {
                StripePayment stripePayment = await _stripeService.AddStripePaymentAsync(payment);
                return stripePayment;
        }

        [HttpPost]
        public async Task<IActionResult> StripePayment(PaymentDto paymentDto)
        {
            AddStripeCustomer customer = new AddStripeCustomer
            (
                paymentDto.Name,
                paymentDto.Email,
                new StripeCard
                (
                    paymentDto.Name,
                    paymentDto.CardNumber,
                    paymentDto.ExpirationYear,
                    paymentDto.ExpirationMonth,
                    paymentDto.CVC
                )
            );
            StripeCustomer stripeCustomer = await AddStripeCustomer(customer);
            AddStripePayment addedStripePayment = new AddStripePayment
                (
                stripeCustomer.CustomerId,
                paymentDto.Email,
                paymentDto.Description,
                "usd",
                70
                );
            return Ok(await AddStripePayment(addedStripePayment));
        }
    }
}
