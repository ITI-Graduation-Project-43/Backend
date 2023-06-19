using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;


namespace MindMission.Infrastructure.Repositories
{
    public class VideoRepository : Repository<Video, int>, IVideoRepository
    {
        public VideoRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}
