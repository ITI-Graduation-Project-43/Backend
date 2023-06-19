using MindMission.Application.Interfaces.Services.Base;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Service_Interfaces
{
    public interface ICategoryService : IService<Category, int>, ICategoryRepository
    {

    }
}