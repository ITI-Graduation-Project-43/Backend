using MindMission.Application.DTOs;
using MindMission.Domain.Stripe.StripeModels;

namespace MindMission.Application.Mapping
{
    public class PaymentMappingService : IPaymentMappingService
    {
        public AddStripeCustomer MapDtoToAddStripeCustomer(PaymentDto dto)
        {
            AddStripeCustomer Customer = new AddStripeCustomer(
                dto.Name,
                dto.Email,
                new StripeCard(
                    dto.NameOnCard,
                    dto.CardNumber,
                    dto.ExpirationYear,
                    dto.ExpirationMonth,
                    dto.CVC));

            return Customer;
        }

        public AddStripePayment MapDtoToAddStripePayment(StripeCustomer customer, PaymentDto dto, decimal CoursesPrice)
        {
            AddStripePayment Payment = new AddStripePayment(
                customer.CustomerId,
                dto.Email,
                dto.Description,
                "usd",
                (long)CoursesPrice * 100);

            return Payment;
        }
    }
}