using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchProgressViewModel
    {
        public Team Team { get; set; }
        public Match Match { get; set; }

        public bool IsHomeTeam { get { return Team.Id == Match.HomeMatchTeam.Team.Id; } }
    }
}
