using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class ScoreStatus : Status
    {
        public ICollection<AwayTeamScore> AwayTeamScores { get; set; }
        public ICollection<HomeTeamScore> HomeTeamScores { get; set; }
    }
}
