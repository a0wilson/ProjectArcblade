using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class ManageNominationsViewModel
    {
        public Team Team { get; set; }
        public MatchTemplate MatchTemplate {get; set;}
        public List<SelectListItem> AvailablePlayers { get; set; }
        public int[] NominatedPlayerIds { get; set; }
        public int[] NominatedPlayerGroupIds { get; set; }
        public int[] NominatedPlayerRankIds { get; set; }
        public int[] NominatedPlayerTeamIds { get; set; }
        public int TeamId { get; set; }
    }
}
