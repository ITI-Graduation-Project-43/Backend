namespace MindMission.Domain.Stripe.StripeModels
{
    public record AddStripeCustomer
    (
        string Name,
        string Email,
        StripeCard CreditCard
    );
}