using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectArcBlade.Models
{
    public class AwayScoreSheet
    {
        public int Id { get; set; }
        public bool SignedOff { get; set; }
        public Audit Audit { get; set; }
        public Match Match { get; set; }
        public AwayMatchTeam AwayMatchTeam { get; set; }
        
        public int MatchId { get; set; }
        public int AwayMatchTeamId { get; set; }

        public ICollection<AwayScoreSheetLine> AwayScoreSheetLines { get; set; }
    }
}
