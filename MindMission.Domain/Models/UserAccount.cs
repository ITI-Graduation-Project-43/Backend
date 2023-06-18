using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents the association between a user and an account.
    /// </summary>
    [Index(nameof(UserId), nameof(AccountId), Name = "UX_UserAccounts_User_AccountType", IsUnique = true)]
    [Index(nameof(UserId), Name = "idx_useraccounts_userid")]
    public partial class UserAccount : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int AccountId { get; set; }

        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string AccountLink { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(AccountId))]
        [InverseProperty("UserAccounts")]
        public virtual Account Account { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }
}