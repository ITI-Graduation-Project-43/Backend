using MindMission.Domain.Models;
using MindMission.Domain.Stripe.StripeModels;

namespace MindMission.Application.Interfaces.Services
{
    public interface IStripeService
    {
        Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer);

        Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment);

        Task<StripePayment> GetStripePayment(AddStripePayment payment);

        Task<StripeCustomer> GetStripeCustomer(AddStripeCustomer customer);

        Task<Course> GetEnrolledCourse(int id);

        Task<long> GetTotalPrice(List<int> coursesIds);

	}
}