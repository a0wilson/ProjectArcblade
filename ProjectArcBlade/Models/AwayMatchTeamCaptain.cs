using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayMatchTeamCaptain
    {
        public int Id { get; set; }
        public AwayMatchTeamGroupPlayer AwayMatchTeamGroupPlayer { get; set; }
    }
}
