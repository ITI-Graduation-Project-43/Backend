using System.ComponentModel.DataAnnotations;

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