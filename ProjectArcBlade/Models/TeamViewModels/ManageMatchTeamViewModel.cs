using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class ManageMatchTeamViewModel
    {
        public Team Team { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public Team Opponents { get; set; }
        public MatchTemplate MatchTemplate { get; set; }
        public List<SelectListItem> AvailablePlayers { get; set; }
        public List<SelectListItem> MatchPlayers { get; set; }
        public int[] MatchClubPlayerIds { get; set; }
        public int[] MatchPlayerGroupIds { get; set; }
        public int[] MatchPlayerRankIds { get; set; }
        public int[] MatchPlayerIds { get; set; }
        public int CaptainId { get; set; }
        public int TeamId { get; set; }
        public int HomeMatchTeamId { get; set; }
        public int AwayMatchTeamId { get; set; }
        public List<NameValuePair> Warnings { get; set; }

        public bool IsHomeMatch { get { return AwayMatchTeamId == 0 ? true : false; } }
        
    }
}
