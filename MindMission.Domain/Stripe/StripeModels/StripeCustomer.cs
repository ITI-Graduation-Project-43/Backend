namespace MindMission.Domain.Stripe.StripeModels
{
    public record StripeCustomer
    (
        string Name,
        string Email,
        string CustomerId
    );
}