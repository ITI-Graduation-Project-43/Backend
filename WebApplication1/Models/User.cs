using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    [Index(nameof(Email), Name = "UC_User_Email", IsUnique = true)]
    [Index(nameof(Email), Name = "idx_users_email")]
    public partial class User
    {
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
        [StringLength(2048)]
        [Unicode(false)]
        public string PasswordHash { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UserType { get; set; }
        [StringLength(4000)]
        [Unicode(false)]
        public string Bio { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeactivated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
