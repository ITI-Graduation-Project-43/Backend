using MindMission.Application.DTOs.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs
{
    public class WishlistDto : IDtoWithId
    {
        public string StudentName { get; set; } = string.Empty;
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public decimal CoursePrice { get; set; }
        public string StudentId { get; set; } = string.Empty;
    }
}
