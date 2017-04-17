using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class HomeMatchTeam
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public Match Match { get; set; }

        public int MatchId { get; set; }
        public ICollection<HomeMatchTeamGroup> HomeMatchTeamGroups { get; set; }
    }
}
