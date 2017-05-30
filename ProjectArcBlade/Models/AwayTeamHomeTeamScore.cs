using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class AwayTeamHomeTeamScore : HomeTeamScore
    {
        public Game Game { get; set; }
    }
}
