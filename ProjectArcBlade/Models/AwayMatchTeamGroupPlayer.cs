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
        public ClubUser ClubUser { get; set; }

        public ICollection<AwayMatchTeamCaptain> AwayMatchTeamCaptains { get; set; }
    }
}
