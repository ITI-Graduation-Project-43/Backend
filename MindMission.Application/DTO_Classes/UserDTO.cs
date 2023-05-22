using MindMission.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTO_Classes
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeactivated { get; set; }
    }
}
