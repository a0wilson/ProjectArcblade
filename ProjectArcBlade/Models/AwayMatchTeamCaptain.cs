using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayMatchTeamCaptain
    {
        public int Id { get; set; }
        public AwayMatchTeam AwayMatchTeam { get; set; }
        public ClubPlayer ClubPlayer { get; set; }

        public int AwayMatchTeamId { get; set; }
    }
}
