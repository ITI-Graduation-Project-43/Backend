using MindMission.Domain.Stripe.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe
{
    public record StripeCard
    (
        [Required(ErrorMessage = "Name on card is required")]
        [StringLength(50,MinimumLength = 3, ErrorMessage = "Name on card length is between 3 and 50")]
        string Name,

        [CardNumberValidator]
        string CardNumber,

        [ExpirationYearValidator]
        string ExpirationYear,

        [ExpirationMonthValidator("ExpirationYear")]
        string ExpirationMonth,

        [CvcValidator]
        string CVC
    );
}
