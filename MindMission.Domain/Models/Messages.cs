using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Models
{
    public class Messages : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsReplyed { get; set; } = false;
    }
}
