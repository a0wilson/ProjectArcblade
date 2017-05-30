using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class HomeTeamAwayTeamScore : AwayTeamScore
    {
        public Game Game { get; set; }
    }
}
