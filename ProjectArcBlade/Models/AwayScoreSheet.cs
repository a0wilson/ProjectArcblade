using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayScoreSheet : ScoreSheet
    {
        public Match Match { get; set; }
        public AwayMatchTeam AwayMatchTeam { get; set; }
        
        public int MatchId { get; set; }
        public int AwayMatchTeamId { get; set; }
    }
}
