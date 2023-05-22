using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe
{
    public record StripeCustomer
    (
        string Name,
        string Email,
        string CustomerId
    );
}
