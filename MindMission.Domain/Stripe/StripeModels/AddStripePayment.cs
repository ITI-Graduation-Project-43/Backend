using MindMission.Domain.Stripe.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe.StripeModels
{
    public record AddStripePayment
    (
        string CustomerId,
        string ReceiptEmail,
        string Description,

        [RegularExpression("usd|aed|afn|all|amd",ErrorMessage = "Available Currencies are [USD,AED,AFN,ALL,AMD]")]
        string Currency,
        long Amount
    );
}
