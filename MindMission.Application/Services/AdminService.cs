using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;


namespace MindMission.Application.Services
{
    public class AdminService : Service<Admin, int>, IAdminService
    {

        public AdminService(IAdminRepository context) : base(context)
        {
        }

    }
}