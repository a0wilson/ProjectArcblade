using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class AppData
    {
        public AppData()
        {
            //Assume Alex Wilson is logged in.
            LeagueId = 1;
            ClubId = 1;
            ClubPlayerId = 1;
        }

        public int LeagueId { get; set; }
        public int ClubId { get; set; }
        public int ClubPlayerId { get; set; }
    }
}
