using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class HomeScoreSheet
    {
        public int Id { get; set; }
        public bool SignedOff { get; set; }
        public Audit Audit { get; set; }
        public Match Match { get; set; }
        public HomeMatchTeam HomeMatchTeam { get; set; }
        
        public int MatchId { get; set; }
        public int HomeMatchTeamId { get; set; }

        public ICollection<HomeScoreSheetLine> HomeScoreSheetLines { get; set; }
    }
}
