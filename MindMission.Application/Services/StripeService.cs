using MindMission.Application.DTOs;
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
        private readonly ISiteCouponRepository _siteCouponRepository;
        private readonly ICouponService _couponService;

        //////Injection of Stripe services to be used to implement the two methods
        public StripeService(
            ChargeService chargeService,
            CustomerService customerService,
            TokenService tokenService,
            ICourseRepository courseRepository,
            ISiteCouponRepository siteCouponRepository,
            ICouponService couponService
            )
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
            _courseRepository = courseRepository;
            _siteCouponRepository = siteCouponRepository;
            _couponService = couponService;
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
        public async Task<long> GetTotalPrice(List<int> coursesIds, string? code, List<CourseCoupon>? courseCoupons)
        {
            decimal totalPrice = 0;
            decimal discount = 0;
            decimal coursesDiscount = 0;
            var coursesIdsWithDiscount = new List<int>();
            var couponsCodes = new List<string>();
            SiteCoupon? siteCoupon = null;
            MindMission.Domain.Models.Coupon? coupon = null;


            #region Site Coupon
            if (code != null)
            {
                try
                {
                    siteCoupon = await _siteCouponRepository.GetByCode(code);

                }
                catch (KeyNotFoundException)
                {
                    siteCoupon = null;
                }
            }


            if (siteCoupon != null)
            {
                if (siteCoupon.Discount != null)
                    discount = siteCoupon.Discount.Value / 100m;
            }
            #endregion

            if (courseCoupons != null)
            {
                foreach (var couponItem in courseCoupons)
                {
                    coursesIdsWithDiscount.Add(couponItem.CourseId);
                    couponsCodes.Add(couponItem.CouponCode);
                }
            }


            foreach (int courseId in coursesIds)
            {
                var course = await GetEnrolledCourse(courseId);

                #region Courses Coupons
                if (coursesIdsWithDiscount.IndexOf(courseId) != -1)
                {
                    int index = coursesIdsWithDiscount.IndexOf(courseId);
                    try
                    {
                        coupon = await _couponService.GetCouponByCodeAndCourse(couponsCodes[index], coursesIdsWithDiscount[index]);
                    }
                    catch (KeyNotFoundException)
                    {
                        coupon = null;
                    }

                    if (coupon != null)
                    {
                        if (coupon.Discount != null)
                            coursesDiscount += course.Price * (coupon.Discount.Value / 100m);
                    }
                }
                #endregion

                totalPrice += (course).Price;
            }

            return (long)((totalPrice - (totalPrice * discount)) - coursesDiscount);
        }
    }
}