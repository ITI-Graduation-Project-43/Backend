using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(UserId), nameof(AccountId), Name = "UX_UserAccounts_User_AccountType", IsUnique = true)]
    [Index(nameof(UserId), Name = "idx_useraccounts_userid")]
    public partial class UserAccount : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public int AccountId { get; set; }

        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string AccountLink { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(AccountId))]
        [InverseProperty("UserAccounts")]
        public virtual Account Account { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}