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
        public SetViewModel[] Sets { get; set; }
        public GameViewModel[] Games { get; set; }
        public PlayerViewModel[] HomePlayers { get; set; }
        public PlayerViewModel[] AwayPlayers { get; set; }

        public string TeamName { get { return Match.IsHomeTeam ? Match.HomeTeamName : Match.AwayTeamName; } }
        public int MatchTeamId { get { return Match.IsHomeTeam ? Match.HomeMatchTeamId : Match.AwayMatchTeamId; } }
        public bool MatchDrawn { get { return (AllSetsCompleted && !HomeWin && !AwayWin) ? true : false; } }
        public bool HomeWin { get { return Sets.Where(s => s.HomeResult == Constants.ResultType.Win).Count() > Match.MinimumSetsToWin; } }
        public bool AwayWin { get { return Sets.Where(s => s.AwayResult == Constants.ResultType.Win).Count() > Match.MinimumSetsToWin; } }
        public bool AllSetsCompleted { get { return Sets.Where(s => s.Status == Constants.SetStatus.Complete).Count() == Sets.Count(); } }
        public bool MatchStatusInProgress { get { return Match.MatchStatusName == Constants.MatchStatus.InProgress; } }
        public bool MatchStatusComplete { get { return Match.MatchStatusName == Constants.MatchStatus.Complete; } }
        public bool AllowMatchSummary { get { return (MatchStatusInProgress ? ((HomeWin || AwayWin) || AllSetsCompleted ? true : false) : false); } }
        public bool AllowConcedeMatch { get { return MatchStatusComplete ? false : (HomeWin || AwayWin) ? false : true; } }
    }
}
