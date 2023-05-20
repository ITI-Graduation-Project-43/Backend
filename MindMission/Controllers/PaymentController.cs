using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Stripe;

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

        [HttpPost("Customer/add")]
        public async Task<IActionResult> AddStripeCustomer(AddStripeCustomer customer, CancellationToken cancellationToken)
        {
            StripeCustomer stripeCustomer = await _stripeService.AddStripeCustomerAsync(customer, cancellationToken);
            return Ok(stripeCustomer);
        }

        [HttpPost("payment/add")]
        public async Task<IActionResult> AddStripePayment(AddStripePayment payment, CancellationToken cancellationToken)
        {
            StripePayment stripePayment = await _stripeService.AddStripePaymentAsync(payment, cancellationToken);
            return Ok(stripePayment);
        }
    }
}
