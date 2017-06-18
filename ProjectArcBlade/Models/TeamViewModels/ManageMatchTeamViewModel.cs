using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ProjectArcBlade.Data.Constants;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class ManageMatchTeamViewModel
    {
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string TeamName { get; set; }
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

        public int HomeMatchTeamId { get; set; }
        public int AwayMatchTeamId { get; set; }
        public List<NameValuePair> Warnings { get; set; }
        
        public bool IsHomeTeam { get { return HomeTeamId == TeamId ? true : false; } }
        public TeamType TeamType { get { return HomeMatchTeamId != 0 ? TeamType.Home : TeamType.Away; } }
        public int MatchTeamId { get { return HomeMatchTeamId != 0 ? HomeMatchTeamId : AwayMatchTeamId; } }
        public bool TeamStatusComplete { get { return TeamStatus == null ? false : TeamStatus.Name == Constants.TeamStatus.Complete ? true : false;  } }
        public bool AllowSubmitTeam { get { return TeamStatusComplete ? true : false; } }

        public string GetMatchPlayer(int rank)
        {
            var index = rank - 1;
            if(MatchClubPlayerIds[index] == 0) return "Unknown";
            return MatchPlayers.Find(m => Convert.ToInt32(m.Value) == MatchClubPlayerIds[index]).Text;
        }
    }
}
