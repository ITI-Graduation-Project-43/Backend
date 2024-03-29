﻿using MindMission.Application.DTOs;
using MindMission.Domain.Stripe.StripeModels;

namespace MindMission.Application.Mapping
{
    public interface IPaymentMappingService
    {
        public AddStripeCustomer MapDtoToAddStripeCustomer(PaymentDto dto);

        public AddStripePayment MapDtoToAddStripePayment(StripeCustomer customer, PaymentDto dto, decimal CoursePrice);
    }
}