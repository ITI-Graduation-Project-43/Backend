using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
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
        public async Task<IActionResult> AddStripeCustomer(AddStripeCustomer customer)
        {
            if(_stripeService.CheckSameYearPassedMonth(customer.CreditCard.ExpirationYear, customer.CreditCard.ExpirationMonth))
            {
                return BadRequest("Expired Card");
            }

            if (ModelState.IsValid)
            {
                StripeCustomer stripeCustomer = await _stripeService.AddStripeCustomerAsync(customer);
                SaveCustomerId.CustomerId = stripeCustomer.CustomerId;
                return Ok(stripeCustomer);
            }

            return BadRequest(customer);            
        }

        [HttpPost("payment/add")]
        public async Task<IActionResult> AddStripePayment(AddStripePayment payment)
        {
            if(payment.CustomerId ==  SaveCustomerId.CustomerId)
            {
                StripePayment stripePayment = await _stripeService.AddStripePaymentAsync(payment);
                return Ok(stripePayment);
            }
            return BadRequest("Invalid Customer Id");
        }
    }
}
