using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe
{
    public record AddStripePayment
    (
        string CustomerId,

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        string ReceiptEmail,

        [StringLength(100,ErrorMessage = "Description maximum length is 100 characters")]
        string Description,

        [RegularExpression("usd|aed|afn|all|amd",ErrorMessage = "Available Currencies are [USD,AED,AFN,ALL,AMD]")]
        string Currency,

        [Range(50,long.MaxValue, ErrorMessage = "Amount minimum value is 50")]
        long Amount
    );
}
