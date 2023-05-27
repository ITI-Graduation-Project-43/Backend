using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class QuestionRepository : Repository<Question, int>, IQuestionRepository
    {
        public QuestionRepository(MindMissionDbContext context) : base(context)
        {

        }
    }
}
