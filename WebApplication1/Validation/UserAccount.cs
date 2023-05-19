using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MindMission.Domain.Models
{
    [Index(nameof(UserId), nameof(AccountId), Name = "UX_UserAccounts_User_AccountType", IsUnique = true)]
    [Index(nameof(UserId), Name = "idx_useraccounts_userid")]
    public partial class UserAccount
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string AccountLink { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("UserAccounts")]
        public virtual Account Account { get; set; }
    }
}
