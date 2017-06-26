using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeScoreSheet : ScoreSheet
    {
        public Match Match { get; set; }
        public HomeMatchTeam HomeMatchTeam { get; set; }
        
        public int MatchId { get; set; }
        public int HomeMatchTeamId { get; set; }
    }
}
