using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public partial class Account
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
        public string AccountType { get; set; }

        [InverseProperty(nameof(UserAccount.Account))]
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
