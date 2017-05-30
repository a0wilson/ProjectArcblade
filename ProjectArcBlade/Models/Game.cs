using System.Collections.Generic;

namespace ProjectArcBlade.Models
{
    public class Game
    {
        public int Id { get; set; }        
        public int Number { get; set; }
        public Set Set { get; set; }
        public GameAwayResult GameAwayResult { get; set; }
        public GameHomeResult GameHomeResult { get; set; }
        public HomeTeamAwayTeamScore HomeTeamAwayTeamScore { get; set; }
        public HomeTeamHomeTeamScore HomeTeamHomeTeamScore { get; set; }
        public AwayTeamAwayTeamScore AwayTeamAwayTeamScore { get; set; }
        public AwayTeamHomeTeamScore AwayTeamHomeTeamScore { get; set; }

        public int HomeTeamAwayTeamScoreId { get; set; }
        public int HomeTeamHomeTeamScoreId { get; set; }
        public int AwayTeamAwayTeamScoreId { get; set; }
        public int AwayTeamHomeTeamScoreId { get; set; }
    }
}
