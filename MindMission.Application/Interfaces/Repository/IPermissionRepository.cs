﻿using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IPermissionRepository : IRepository<Permission, int>
    {
    }
}