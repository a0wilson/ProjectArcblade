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
        public ICollection<SetViewModel> Sets { get; set; }
        
        //Calculated fields
        public bool HomeWin { get { return  Sets == null ? false : Sets.Where(s => s.HomeResult == Constants.ResultType.Win).Count() > Match.MinimumSetsToWin; } }
        public bool AwayWin { get { return Sets == null ? false : Sets.Where(s => s.AwayResult == Constants.ResultType.Win).Count() > Match.MinimumSetsToWin; } }
        public string TeamName { get { return Match == null ? "" : Match.IsHomeTeam ? Match.HomeTeamName : Match.AwayTeamName; } }
        public bool AllSetsCompleted { get { return Sets == null ? false : Sets.Where(s => s.Status == Constants.SetStatus.Complete).Count() == Sets.Count(); } }
        public bool MatchStatusInProgress { get { return Match == null ? false : Match.MatchStatusName == Constants.MatchStatus.InProgress; } }
        public bool AllowMatchCompletion { get { return Match == null ? false : (MatchStatusInProgress ? ((HomeWin || AwayWin) || AllSetsCompleted ? true : false) : false); } }

    }
}
