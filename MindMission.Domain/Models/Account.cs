using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class Account : BaseEntity, IEntity<int>, ISoftDeletable
    {
        public Account()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        [Unicode(false)]
        [EnumDataType(typeof(UserAccount))]
        public string AccountType { get; set; }
        public bool IsDeleted { get; set; } = false;

        [InverseProperty(nameof(UserAccount.Account))]
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}