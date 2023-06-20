using MindMission.Application.Interfaces.Services.Base;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Service.Interfaces
{
    public interface IQuizService : IService<Quiz, int>, IQuizRepository
    {
    }
}