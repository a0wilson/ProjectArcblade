using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchProgressViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public bool IsHomeTeam { get; set; }
        public int MatchId { get; set; }
        public int[] SetId { get; set; }
        public int[] SetNumber { get; set; }
        public string[] SetStatus { get; set; }
        public string[] SetHomeResult { get; set; }
        public string[] SetAwayResult { get; set; }
        public int[] HomeScore { get; set; }
        public int[] AwayScore { get; set; }
        public string[] HomeGroupName { get; set; }
        public string[] AwayGroupName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }        
        public int SetTotal { get; set; }
        public bool AllowMatchCompletion { get; set; }
    }
}
