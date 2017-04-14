using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class MatchAppData
    {
        public MatchAppData()
        {
           
        }

        public int CategoryId { get; set; }
        public int DivisionId { get; set; }
        public int SeasonId { get; set; }

        public int HomeTeamId { get; set; }
        public int HomeTeamHandicap { get; set; }

        public int AwayTeamId { get; set; }
        public int AwayTeamHandicap { get; set; }

        public bool IsCupMatch { get; set; }
        public int CupId { get; set; }
        

    }
}
