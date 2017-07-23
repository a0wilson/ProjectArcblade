using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchProgressViewModel
    {
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public MatchViewModel Match { get; set; }
        
        //Calculated fields
        public int MatchTeamId { get { return Match.IsHomeTeam ? Match.HomeMatchTeamId : Match.AwayMatchTeamId; } }
        public bool MatchDrawn { get { return (Match.AllSetsCompleted && !Match.HomeWin && !Match.AwayWin) ? true : false; } }
        public string TeamName { get { return Match.IsHomeTeam ? Match.HomeTeamName : Match.AwayTeamName; } }
        public bool AllowMatchSummary { get { return ((Match.HomeWin || Match.AwayWin) || Match.AllSetsCompleted) ? true : false; } }
        public bool AllowConcedeMatch { get { return Match.MatchStatusComplete ? false : (Match.HomeWin || Match.AwayWin) || Match.AllSetsCompleted ? false : true; } }
        
    }
}
