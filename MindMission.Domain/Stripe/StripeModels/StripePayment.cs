namespace MindMission.Domain.Stripe.StripeModels
{
    public record StripePayment
    (
        string CustomerId,
        string ReceiptEmail,
        string Description,
        string Currency,
        long Amount,
        string PaymentID
    );
}