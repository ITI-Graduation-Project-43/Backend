using MindMission.Application.DTOs;
using MindMission.Domain.Stripe.StripeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public interface IPaymentMappingService
    {
        public AddStripeCustomer MapDtoToAddStripeCustomer(PaymentDto dto);
        public AddStripePayment MapDtoToAddStripePayment(StripeCustomer customer, PaymentDto dto, decimal CoursePrice);
    }
}
