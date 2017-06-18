using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProjectArcBlade.Data.Constants;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class PreviewMatchViewModel
    {
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeMatchTeamId { get; set; }
        public int AwayMatchTeamId { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string Venue { get; set; }
        public string Postcode { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamStatus { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamStatus { get; set; }         
        public string[] AwayTeamPlayers { get; set; }
        public string[] HomeTeamPlayers { get; set; }
        public string[] AwayTeamPlayerGroups { get; set; }
        public string[] HomeTeamPlayerGroups { get; set; }
        public int[] AwayTeamPlayerIds { get; set; }
        public int[] HomeTeamPlayerIds { get; set; }
        public int HomeTeamCaptainId { get; set; }
        public int AwayTeamCaptainId { get; set; }
        
        //calculated fields
        public bool HomeTeamStatusIsActive { get { return HomeTeamStatus == Constants.TeamStatus.Active; } }
        public bool AwayTeamStatusIsActive { get { return AwayTeamStatus == Constants.TeamStatus.Active; } }
        public bool AllowMatchStart { get { return (HomeTeamStatusIsActive && AwayTeamStatusIsActive); } }
        public bool IsHomeTeam { get { return HomeTeamId == TeamId ? true : false; } }
        public bool AllowTeamSubmit { get { return IsHomeTeam ? (HomeTeamStatusIsActive ? false : true) : (AwayTeamStatusIsActive ? false : true); } }
        public int MatchTeamId { get { return IsHomeTeam ? HomeMatchTeamId : AwayMatchTeamId; } }
        public TeamType TeamType { get { return IsHomeTeam ? TeamType.Home : TeamType.Away; } }
        public string TeamName { get { return IsHomeTeam ? HomeTeamName : AwayTeamName; } }

    }
}
