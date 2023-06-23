using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class PermissionDto : IDtoWithId<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; } = string.Empty;

        public List<string> AdminIds { get; set; } = new List<string>();
    }
}