using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectArcBlade.Models.MatchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class TeamDashboardViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public IEnumerable<MatchViewModel> InProgressMatches { get; set; }
        public IEnumerable<MatchViewModel> UpcomingMatches { get; set; }
        public IEnumerable<MatchViewModel> CompletedMatches { get; set; }
        public IEnumerable<NameValuePair> Overview { get; set; }

        public List<SelectListItem> AvailableTeams{ get; set; }
        
    }
}
