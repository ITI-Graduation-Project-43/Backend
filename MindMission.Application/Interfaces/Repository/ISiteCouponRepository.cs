using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Repository
{
    public interface ISiteCouponRepository : IRepository<SiteCoupon, int>
    {
        Task<SiteCoupon> GetByCode(string code);
    }
}
