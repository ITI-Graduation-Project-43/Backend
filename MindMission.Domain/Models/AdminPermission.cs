using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MindMission.Domain.Models
{
    [Index(nameof(Id), Name = "idx_adminpermissions_adminid")]
    public partial class AdminPermission : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public int PermissionId { get; set; }
        public DateTime GrantedAt { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty("AdminPermissions")]
        public virtual Admin Admin { get; set; }
        [ForeignKey(nameof(PermissionId))]
        [InverseProperty("AdminPermissions")]
        public virtual Permission Permission { get; set; }
    }
}
