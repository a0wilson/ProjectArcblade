namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchViewModel
    {
        public int TeamId { get; set; }
        public string MatchStatusName { get; set; }
        public int MatchId { get; set; }
        public int MinimumSetsToWin { get; set; }
       
        public int AwayTeamId { get; set; }
        public int AwayMatchTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public string MatchAwayResult { get; set; }
        public string AwayTeamStatus { get; set; }

        public int HomeTeamId { get; set; }
        public int HomeMatchTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string MatchHomeResult { get; set; }
        public string HomeTeamStatus { get; set; }

        public string VenueName { get; set; }
        public string StartDate { get; set; }
        
        public bool IsHomeTeam { get { return HomeTeamId == TeamId ? true : false; } }
        public string Opponent { get { return IsHomeTeam ? AwayTeamName : HomeTeamName; } }
        public string Type { get { return IsHomeTeam ? "Home" : "Away"; } }
        public string TeamStatus { get { return IsHomeTeam ? HomeTeamStatus : AwayTeamStatus; } }
        public string TeamResult { get { return IsHomeTeam ? MatchHomeResult : MatchAwayResult; } }

    }
}
