using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public partial class Permission
    {
        public Permission()
        {
            AdminPermissions = new HashSet<AdminPermission>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; }

        [InverseProperty(nameof(AdminPermission.Permission))]
        public virtual ICollection<AdminPermission> AdminPermissions { get; set; }
    }
}
