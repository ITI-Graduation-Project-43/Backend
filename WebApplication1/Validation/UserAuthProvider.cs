using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MindMission.Domain.Models
{
    public partial class UserAuthProvider
    {
        [Key]
        public int AccountId { get; set; }
        public int UserId { get; set; }
        [StringLength(255)]
        public string AccountProvider { get; set; }
        [StringLength(2048)]
        public string AccountLink { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
