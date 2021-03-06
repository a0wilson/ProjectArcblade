﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayMatchTeam
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public Match Match { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public AwayScoreSheet AwayScoreSheet { get; set; }

        public int MatchId { get; set; } 
        public ICollection<AwayMatchTeamGroup> AwayMatchTeamGroups { get; set; }
        public AwayMatchTeamCaptain AwayMatchTeamCaptain { get; set; }
    }
}
