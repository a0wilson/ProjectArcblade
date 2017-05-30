using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayMatchTeamGroup
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public AwayMatchTeam AwayMatchTeam { get; set; }

        public ICollection<AwayMatchTeamGroupPlayer> AwayMatchTeamGroupPlayers { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}
