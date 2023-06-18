﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Domain.Common
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string>? Errors { get; set; }
    }
}
