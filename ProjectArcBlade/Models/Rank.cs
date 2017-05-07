using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Rank
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public ICollection<RankTemplate> RankTemplates { get; set; }
        public ICollection<HomeMatchTeamGroupPlayer> HomeMatchTeamGroupPlayers { get; set; }
        public ICollection<AwayMatchTeamGroupPlayer> AwayMatchTeamGroupPlayers { get; set; }
    }
}
