using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class LessonRepository : Repository<Lesson, int>, ILessonRepository
    {
        public LessonRepository(MindMissionDbContext context) : base(context)
        {

        }
    }
}
