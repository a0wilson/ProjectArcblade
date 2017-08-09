using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchSummaryViewModel
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        
        public MatchViewModel Match { get; set; }
        public GameViewModel[] ContestedGames { get; set; }

        public string TeamName { get { return Match.IsHomeTeam ? Match.HomeTeamName : Match.AwayTeamName; } }
        public int MatchTeamId { get { return Match.IsHomeTeam ? Match.HomeMatchTeamId : Match.AwayMatchTeamId; } }

        public bool NoContestedGames { get { return Match.ContestedGamesTotal == 0; } }
        public bool AllowMatchSignOff { get { return !Match.SignedOff && NoContestedGames; } }
    }
}
