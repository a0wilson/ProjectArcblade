using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayMatchTeamGroupPlayer
    {
        public int Id { get; set; }
        public AwayMatchTeamGroup AwayMatchTeamGroup { get; set; }
        public ClubPlayer ClubPlayer { get; set; }
        public Rank Rank { get; set; }
    }
}
