using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchScoresheetLineViewModel
    {
        public int GroupId { get; set; }
        public string Heading { get; set; }
        public int HomeSetWinTotal { get; set; }
        public int AwaySetWinTotal { get; set; }
        public int HomeGameWinTotal { get; set; }
        public int AwayGameWinTotal { get; set; }
        public int HomePointsTotal { get; set; }
        public int AwayPointsTotal { get; set; }
    }
}
