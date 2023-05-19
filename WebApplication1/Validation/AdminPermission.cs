using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MindMission.Domain.Models
{
    [Index(nameof(AdminId), Name = "idx_adminpermissions_adminid")]
    public partial class AdminPermission
    {
        [Key]
        public int AdminId { get; set; }
        [Key]
        public int PermissionId { get; set; }
        public DateTime GrantedAt { get; set; }

        [ForeignKey(nameof(AdminId))]
        [InverseProperty("AdminPermissions")]
        public virtual Admin Admin { get; set; }
        [ForeignKey(nameof(PermissionId))]
        [InverseProperty("AdminPermissions")]
        public virtual Permission Permission { get; set; }
    }
}
