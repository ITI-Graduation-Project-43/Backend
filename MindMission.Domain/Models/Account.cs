using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;

namespace MindMission.Domain.Models
{
    public partial class Account : IEntity<int>
    {
        public Account()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        [Unicode(false)]
        [EnumDataType(typeof(AccountType))]
        public string AccountType { get; set; }

        [InverseProperty(nameof(UserAccount.Account))]
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
