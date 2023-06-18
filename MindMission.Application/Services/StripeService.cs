using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Domain.Stripe.CustomValidationAttributes;
using MindMission.Domain.Stripe.StripeModels;
using Stripe;

namespace MindMission.Application.Services
{
    public class StripeService : IStripeService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;
        private readonly ICourseRepository _courseRepository;
		private readonly ICouponRepository _couponRepository;

		//////Injection of Stripe services to be used to implement the two methods
		public StripeService(
            ChargeService chargeService,
            CustomerService customerService,
            TokenService tokenService,
            ICourseRepository courseRepository,
            ICouponRepository couponRepository
            )
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
            _courseRepository = courseRepository;
			_couponRepository = couponRepository;
		}

        public async Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer)
        {
            //////Configuration of Stripe card token
            TokenCreateOptions tokenCreateOptions = new TokenCreateOptions()
            {
                Card = new TokenCardOptions()
                {
                    Name = customer.Name,
                    Number = customer.CreditCard.CardNumber,
                    ExpYear = customer.CreditCard.ExpirationYear,
                    ExpMonth = customer.CreditCard.ExpirationMonth,
                    Cvc = customer.CreditCard.CVC
                }
            };

            //////Create the token
            Token StripeToken = await _tokenService.CreateAsync(tokenCreateOptions, null);

            //////Configuration of Stripe Customer
            CustomerCreateOptions customerCreateOptions = new CustomerCreateOptions()
            {
                Name = customer.Name,
                Email = customer.Email,
                Source = StripeToken.Id
            };

            //////Create the Customer
            Customer StripeCustomer = await _customerService.CreateAsync(customerCreateOptions, null);

            //////Return the Customer in the StripeCustomer record
            return new StripeCustomer(StripeCustomer.Name, StripeCustomer.Email, StripeCustomer.Id);
        }

        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment)
        {
            //////Configuration of Stripe Payment
            ChargeCreateOptions chargeCreateOptions = new ChargeCreateOptions()
            {
                Customer = payment.CustomerId,
                ReceiptEmail = payment.ReceiptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount
            };

            //////Create the payment
            Charge StripePayment = await _chargeService.CreateAsync(chargeCreateOptions, null);

            //////Return the Payment in the StripePayment record
            return new StripePayment(
                StripePayment.CustomerId,
                StripePayment.ReceiptEmail,
                StripePayment.Description,
                StripePayment.Currency,
                StripePayment.Amount,
                StripePayment.Id);
        }

        //////
        //Adding Customer to stripe by checking card details
        //////
        public async Task<StripeCustomer> GetStripeCustomer(AddStripeCustomer customer)
        {
            StripeCustomer stripeCustomer = await AddStripeCustomerAsync(customer);
            ReturnedCutomerId.CustomerId = stripeCustomer.CustomerId;
            return stripeCustomer;
        }

        //////
        //Adding Charge to stripe by added customer Id
        //////
        public async Task<StripePayment> GetStripePayment(AddStripePayment payment)
        {
            StripePayment stripePayment = await AddStripePaymentAsync(payment);
            return stripePayment;
        }

        //////Get Choosed Course to enroll in.
        public async Task<Course> GetEnrolledCourse(int id) => await _courseRepository.GetByIdAsync(id);


        //////Calc Discount
        public async Task<long> GetTotalPrice(List<int> coursesIds, string? code)
        {
            decimal totalPrice = 0;
            decimal discount = 0;
            Course courseWithDiscount = null;

            if(code != null && code.Length >= 5)
            {
				Domain.Models.Coupon coupon = await _couponRepository.getCouponByCode(code);
				if (coupon != null)
				{
					if (coursesIds.IndexOf(coupon.CourseId.Value) != -1)
					{
						courseWithDiscount = await GetEnrolledCourse(coupon.CourseId.Value);
						discount = courseWithDiscount.Price * (coupon.Discount / 100m).Value;
					}
				}
			}

			

			decimal coursePrice = 0;
			foreach (int courseId in coursesIds)
            {
                if(courseWithDiscount != null && courseWithDiscount.Id == courseId)
                {
                    coursePrice = courseWithDiscount.Price;
                }
                else
                {
					coursePrice = (await GetEnrolledCourse(courseId)).Price;
				}
				totalPrice += coursePrice;
			}
			return (long) (totalPrice - discount);
        }
    }
}