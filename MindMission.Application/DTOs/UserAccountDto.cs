using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class UserAccountDto : IDtoWithId<int>
    {
        public string UserId { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string AccountLink { get; set; } = string.Empty;
        public int Id { get; set; }


    }
}