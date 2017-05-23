using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeGameResultScoreFromAwayTeam : HomeGameResultScore
    {
        public AwayMatchTeam AwayMatchTeam { get; set; }
    }
}
