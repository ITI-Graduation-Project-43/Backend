using MindMission.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs
{
    public class StudentDto: IDtoWithId<string>
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;

        public int NumCourses { get; set; }
        public int NumWishlist { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Dictionary<string, string> accounts { get; set; } = new Dictionary<string, string>();
    }
}
