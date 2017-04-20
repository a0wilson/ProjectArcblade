using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class ManageTeamPlayersViewModel
    {
        public string Name { get; set; }
        [Display(Name="Available Players")]
        public int[] AvailableTeamPlayerIds { get; set; }
        [Display(Name = "Assigned Players")]
        public int[] AssignedTeamPlayerIds { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        [Display(Name = "Team")]
        public int TeamId { get; set; }
        [Display(Name="Filter Available Players")]
        public bool FilterAvailablePlayers { get; set; }

        
        public List<SelectListItem> AvailableTeamPlayers { get; set; }
        public List<SelectListItem> AssignedTeamPlayers { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Teams { get; set; }

    }
}
