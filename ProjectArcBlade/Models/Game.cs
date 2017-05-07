using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Game
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public int Order { get; set; }
        public AwayMatchTeamGroup AwayMatchTeamGroup { get; set; }
        public HomeMatchTeamGroup HomeMatchTeamGroup { get; set; }

        public AwayGameResult AwayGameResult { get; set; }
        public HomeGameResult HomeGameResult { get; set; }
    }
}
