using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
