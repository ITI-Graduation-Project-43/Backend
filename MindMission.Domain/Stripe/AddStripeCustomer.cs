using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Stripe
{
    public record AddStripeCustomer
    (
        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer Name should be between 3 and 50")]
        [DataType(DataType.Text,ErrorMessage = "dfdgfg")]
        string Name,

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        string Email,

        [Required(ErrorMessage ="Credit Card Details are required")]
        StripeCard CreditCard
    );
}
