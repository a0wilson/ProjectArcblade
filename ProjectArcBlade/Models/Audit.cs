using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<AwayTeamScore> AwayGameResultScores { get; set; }
        public ICollection<HomeTeamScore> HomeGameResultScores { get; set; }
        public ICollection<ScoreSheet> ScoreSheets { get; set; }
    }
}
