using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class PermissionService : Service<Permission, int>, IPermissionService
    {

        public PermissionService(IPermissionRepository context) : base(context)
        {

        }


    }
}