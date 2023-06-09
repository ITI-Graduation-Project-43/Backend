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