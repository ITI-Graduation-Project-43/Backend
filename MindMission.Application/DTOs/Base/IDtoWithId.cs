using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs.Base
{
    public interface IDtoWithId<Type>
    {
        Type Id { get; set; }
    }

}
