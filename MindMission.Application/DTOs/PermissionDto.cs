using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
