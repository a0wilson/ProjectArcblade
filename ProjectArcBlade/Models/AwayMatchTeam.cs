using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayMatchTeam
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public Match Match { get; set; }
        public ResultType ResultType { get; set; }

        public int MatchId { get; set; }
        public ICollection<AwayMatchTeamGroup> AwayMatchTeamGroups { get; set; }
    }
}
