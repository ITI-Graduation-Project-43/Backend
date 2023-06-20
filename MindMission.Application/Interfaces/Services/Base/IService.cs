using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Common;


namespace MindMission.Application.Interfaces.Services.Base
{
    public interface IService<TClass, TDataType> : IRepository<TClass, TDataType> where TClass : class, IEntity<TDataType>, new()
    {

    }

}
