using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe
{
    public record StripeCard
    (
        string Name,
        string CardNumber,
        string ExpirationYear,
        string ExpirationMonth,
        string CVC
    );
}
