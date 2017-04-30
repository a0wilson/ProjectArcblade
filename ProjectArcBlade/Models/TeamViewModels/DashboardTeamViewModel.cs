using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class DashboardTeamViewModel
    {
        public Team Team { get; set; }
        public ICollection<Match> UpcomingMatches { get; set; }
        public ICollection<Match> RecentMatches { get; set; }
        public ICollection<NameValuePair> Overview { get; set; }
    }
}
