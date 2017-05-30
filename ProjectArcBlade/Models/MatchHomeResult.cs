using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class MatchHomeResult : HomeResult
    {
        public Match Match { get; set; }

        public int MatchId { get; set; }
    }
}
