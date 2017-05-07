using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class TeamDashboardViewModel
    {
        public Team Team { get; set; }
        public ICollection<Match> UpcomingMatches { get; set; }
        public ICollection<Match> RecentMatches { get; set; }
        public ICollection<NameValuePair> Overview { get; set; }

        public int SelectedTeamId { get; set; }
        public List<SelectListItem> AvailableTeams{ get; set; }
    }
}
