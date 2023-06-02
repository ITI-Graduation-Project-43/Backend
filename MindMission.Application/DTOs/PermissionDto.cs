using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class PermissionDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; }

        [StringLength(2048)]
        [Unicode(false)]
        public string Description { get; set; }

        public List<int> AdminIds { get; set; }
    }
}