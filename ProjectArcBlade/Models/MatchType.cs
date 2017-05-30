using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class MatchType : Type
    {
        public ICollection<Match> Matches { get; set; }
    }
}
