using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class LeagueRule
    {
        public int Id { get; set; }
        public Rule Rule { get; set; }
        public League League { get; set; }
    }
}
