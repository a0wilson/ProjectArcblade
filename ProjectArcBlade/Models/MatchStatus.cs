﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchStatus : Status
    {
        public ICollection<Match> Matches { get; set; }
    }
}
