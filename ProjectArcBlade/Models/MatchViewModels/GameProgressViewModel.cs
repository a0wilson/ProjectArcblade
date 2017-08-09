using ProjectArcBlade.Data;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class GameProgressViewModel
    {
        //public int SetId { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public SetViewModel Set { get; set; }

        public string TeamName {  get { return Set.IsHomeTeam ? Set.HomeTeam : Set.AwayTeam; } }
        public bool AllowConcedeGame { get { return Set.AwayWin || Set.HomeWin ? false : true; } }
        public bool ReadOnlyMode { get { return Set.MatchSignOff || Set.SetConceded; } }
    }
}
