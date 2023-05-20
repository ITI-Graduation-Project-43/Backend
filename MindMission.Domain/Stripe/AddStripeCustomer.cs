using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe
{
    public record AddStripeCustomer
    (
        string Name,
        string Email,
        StripeCard CreditCard
    );
}
