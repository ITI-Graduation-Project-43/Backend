using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe.StripeModels
{
    public record AddStripeCustomer
    (
        string Name,
        string Email,
        StripeCard CreditCard
    );
}
