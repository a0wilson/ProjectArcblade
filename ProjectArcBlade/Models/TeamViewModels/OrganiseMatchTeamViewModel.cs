using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class OrganiseMatchTeamViewModel
    {
        [Display(Name = "Available Players")]
        public int[] AvailableMatchPlayerIds { get; set; }
        [Display(Name = "Assigned Players")]
        public int[] AssignedMatchPlayerIds { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [HiddenInput]
        public int HomeMatchTeamId { get; set; }
        [HiddenInput]
        public int AwayMatchTeamId { get; set; }
        public int TeamId { get; set; }
        [Display(Name="Captain")]
        public int CaptainId { get; set; }
        
        public List<NameValuePair> Warnings { get; set; }
        public List<SelectListItem> AvailableMatchlayers { get; set; }
        public List<SelectListItem> AssignedClubPlayers { get; set; }
        public List<SelectListItem> AssignedMatchPlayers { get; set; }
        public List<SelectListItem> Groups { get; set; }
    }
}
