using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public partial class Admin
    {
        public Admin()
        {
            AdminPermissions = new HashSet<AdminPermission>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Email { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string ProfilePicture { get; set; }
        [Required]
        [StringLength(2048)]
        [Unicode(false)]
        public string PasswordHash { get; set; }
        public bool IsDeactivated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty(nameof(AdminPermission.Admin))]
        public virtual ICollection<AdminPermission> AdminPermissions { get; set; }
    }
}
