namespace ProjectArcBlade.Models.MatchViewModels
{
    public class GameProgressViewModel
    {
        public int SetId { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public string TeamName { get; set; }
        public string SetAwayResult { get; set; }
        public string SetHomeResult { get; set; }
        public int[] GameId { get; set; }
        public int[] GameNumber { get; set; }
        public int?[] AwayAwaySore { get; set; }
        public int?[] AwayHomeSore { get; set; }
        public int?[] HomeAwaySore { get; set; }
        public int?[] HomeHomeSore { get; set; }
        public string[] AwayAwaySoreStatus { get; set; }
        public string[] AwayHomeSoreStatus { get; set; }
        public string[] HomeAwaySoreStatus { get; set; }
        public string[] HomeHomeSoreStatus { get; set; }
        public string HomeGroup { get; set; }
        public string AwayGroup { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public bool IsHomeTeam { get; set; }
        public int GameTotal { get; set; }
        public int SeasonId { get; set; }
        public int CategoryId { get; set; }
    }
}
