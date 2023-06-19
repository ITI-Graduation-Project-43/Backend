using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{

    public class VideoService : Service<Video, int>, IVideoService
    {
        public VideoService(IVideoRepository context) : base(context)
        {

        }

    }
}
