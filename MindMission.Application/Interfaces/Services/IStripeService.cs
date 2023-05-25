using MindMission.Domain.Models;
using MindMission.Domain.Stripe.StripeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Services
{
    public interface IStripeService
    {
        Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer);
        Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment);
        public Task<Course> GetEnrolledCourse(int id);
    }
}
