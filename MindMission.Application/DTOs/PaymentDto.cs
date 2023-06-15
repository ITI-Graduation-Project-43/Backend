using MindMission.Domain.Stripe.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class PaymentDto
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer Name should be between 3 and 50")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name On Card is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer Name should be between 3 and 50")]
        public string NameOnCard { get; set; } = string.Empty;

        [CardNumberValidator]
        public string CardNumber { get; set; } = string.Empty;

        [ExpirationYearValidator]
        public string ExpirationYear { get; set; } = string.Empty;

        [ExpirationMonthValidator("ExpirationYear")]
        public string ExpirationMonth { get; set; } = string.Empty;

        [CvcValidator]
        public string CVC { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Description maximum length is 100 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Courses Ids list is required")]
        public List<int> CoursesIds { get; set; } = new List<int>();
    }
}