using MindMission.Domain.Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Service_Interfaces
{
    public interface IStripeService
    {
        Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken cancellationToken);
        Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken cancellationToken);
    }
}
