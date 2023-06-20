using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service.Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Services_Classes
{
    public class QuestionService : Service<Question, int>, IQuestionService
    {

        public QuestionService(IQuestionRepository context) : base(context)
        {
        }


    }
}