using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayGameResultScoreFromHomeTeam : AwayGameResultScore
    {
        public HomeMatchTeam HomeMatchTeam { get; set; }
    }
}
