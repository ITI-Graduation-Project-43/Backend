using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IAdminRepository :IRepository<Admin, int>
    {

    }
}
