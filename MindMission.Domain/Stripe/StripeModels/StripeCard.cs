using MindMission.Domain.Stripe.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe.StripeModels
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
