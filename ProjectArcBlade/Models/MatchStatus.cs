using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
