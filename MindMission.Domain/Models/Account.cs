using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    public partial class Account : BaseEntity, IEntity<int>, ISoftDeletable
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        [EnumDataType(typeof(UserAccount))]
        public string AccountName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        [EnumDataType(typeof(UserAccount))]
        public string AccountDomain { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        [InverseProperty(nameof(UserAccount.Account))]
        public virtual ICollection<UserAccount>? UserAccounts { get; set; }
    }

}