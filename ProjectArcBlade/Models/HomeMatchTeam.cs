using ProjectArcBlade.Interfaces;
using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class HomeMatchTeam : IMatchTeam
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public Match Match { get; set; }
        public ResultType ResultType { get; set; }
        public TeamStatus TeamStatus { get; set; }

        public int MatchId { get; set; }        
        public ICollection<HomeMatchTeamGroup> HomeMatchTeamGroups { get; set; }
        public HomeMatchTeamCaptain HomeMatchTeamCaptain { get; set; }
    }
}
