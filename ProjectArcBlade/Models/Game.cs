using ProjectArcBlade.Data;
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

        public bool HomeTeamAwayTeamScoreAccepted { get { return HomeTeamAwayTeamScore.ScoreStatus.Name == Constants.ScoreStatus.Accepted; } }
        public bool HomeTeamHomeTeamScoreAccepted { get { return HomeTeamHomeTeamScore.ScoreStatus.Name == Constants.ScoreStatus.Accepted; } }
        public bool AwayTeamAwayTeamScoreAccepted { get { return AwayTeamAwayTeamScore.ScoreStatus.Name == Constants.ScoreStatus.Accepted; } }
        public bool AwayTeamHomeTeamScoreAccepted { get { return AwayTeamHomeTeamScore.ScoreStatus.Name == Constants.ScoreStatus.Accepted; } }

        public bool HomeTeamAwayTeamScoreEntered { get { return HomeTeamAwayTeamScore.ScoreStatus.Name != Constants.ScoreStatus.NoEntry; } }
        public bool HomeTeamHomeTeamScoreEntered { get { return HomeTeamHomeTeamScore.ScoreStatus.Name != Constants.ScoreStatus.NoEntry; } }
        public bool AwayTeamAwayTeamScoreEntered { get { return AwayTeamAwayTeamScore.ScoreStatus.Name != Constants.ScoreStatus.NoEntry; } }
        public bool AwayTeamHomeTeamScoreEntered { get { return AwayTeamHomeTeamScore.ScoreStatus.Name != Constants.ScoreStatus.NoEntry; } }

    }
}
