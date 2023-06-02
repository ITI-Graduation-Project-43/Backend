using System;
using System.Linq;

namespace MindMission.Domain.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
