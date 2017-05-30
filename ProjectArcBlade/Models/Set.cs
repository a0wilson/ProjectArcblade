using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class Set
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public int Number { get; set; }
        public SetStatus SetStatus { get; set; }
        public SetAwayResult SetAwayResult { get; set; }
        public SetHomeResult SetHomeResult { get; set; }
        public AwayMatchTeamGroup AwayMatchTeamGroup { get; set; }
        public HomeMatchTeamGroup HomeMatchTeamGroup { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
