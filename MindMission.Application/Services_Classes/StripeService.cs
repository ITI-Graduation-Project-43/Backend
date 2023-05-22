using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Stripe;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services_Classes
{
    public class StripeService : IStripeService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;

        //////Injection of Stripe services to be used to implement the two methods
        public StripeService(ChargeService chargeService, CustomerService customerService, TokenService tokenService)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
        }

        public async Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer)
        {
            //////Configuration of Stripe card token
            TokenCreateOptions tokenCreateOptions = new TokenCreateOptions()
            {
                Card = new TokenCardOptions()
                {
                    Name = customer.Name,
                    Number = customer.CreditCard.CardNumber,
                    ExpYear = customer.CreditCard.ExpirationYear,
                    ExpMonth = customer.CreditCard.ExpirationMonth,
                    Cvc = customer.CreditCard.CVC
                }
            };

            //////Create the token
            Token StripeToken = await _tokenService.CreateAsync(tokenCreateOptions,null);


            //////Configuration of Stripe Customer
            CustomerCreateOptions customerCreateOptions = new CustomerCreateOptions()
            {
                Name = customer.Name,
                Email = customer.Email,
                Source = StripeToken.Id
            };

            //////Create the Customer
            Customer StripeCustomer = await _customerService.CreateAsync(customerCreateOptions,null);

            //////Return the Customer in the StripeCustomer record
            return new StripeCustomer(StripeCustomer.Name, StripeCustomer.Email, StripeCustomer.Id);
        }

        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment)
        {

            //////Configuration of Stripe Payment
            ChargeCreateOptions chargeCreateOptions = new ChargeCreateOptions()
            {
                Customer = payment.CustomerId,
                ReceiptEmail = payment.ReceiptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount
            };

            //////Create the payment
            var StripePayment = await _chargeService.CreateAsync(chargeCreateOptions,null);

            //////Return the Payment in the StripePayment record
            return new StripePayment(
                StripePayment.CustomerId, 
                StripePayment.ReceiptEmail, 
                StripePayment.Description, 
                StripePayment.Currency, 
                StripePayment.Amount, 
                StripePayment.Id);
        }

        //////Check for same year and a passed month
        public bool CheckSameYearPassedMonth(string year, string month)
        {
            if (int.TryParse(year, out int expYear) && int.TryParse(month, out int expMonth))
            {
                if ((expYear + 2000) == DateTime.Now.Year && expMonth < DateTime.Now.Month)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
