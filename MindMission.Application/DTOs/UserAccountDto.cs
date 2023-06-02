using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class UserAccountDto : IDtoWithId<int>
    {
        public string UserId { get; set; } = string.Empty;
        public int accountId { get; set; }
        public string accountLink { get; set; } = string.Empty;
        public int Id { get; set; }


    }
}