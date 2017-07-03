using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class SetViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public string HomeResult { get; set; }
        public string AwayResult { get; set; }
        public string HomeGroup { get; set; }
        public string AwayGroup { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public bool IsHomeTeam { get; set; }
        public int CategoryId { get; set; }
        public int SeasonId { get; set; }

        public GameViewModel[] Games { get; set; }

        //calculated fields
        public int AggregatedHomeScore { get { return Games == null ? 0 : Games.Where(g => g.AggregatedHomeResult == Constants.ResultType.Win).Count(); } }
        public int AggregatedAwayScore { get { return Games == null ? 0 : Games.Where(g => g.AggregatedAwayResult == Constants.ResultType.Win).Count(); } }

        public string AggregatedHomeResult
        {
            get
            {
                if (Games == null) return Constants.ResultType.Invalid;
                if (HomeResult == Constants.ResultType.Conceded) return HomeResult;
                if ((AggregatedAwayScore == AggregatedHomeScore) && AllGamesCompleted) return Constants.ResultType.Draw;
                if (AggregatedHomeScore >= MinimumGamesToWinSet) return Constants.ResultType.Win;
                if (AggregatedAwayScore >= MinimumGamesToWinSet) return Constants.ResultType.Loss;
                return Constants.ResultType.Pending;
            }
        }
        public string AggregatedAwayResult
        {
            get
            {
                if (Games == null) return Constants.ResultType.Invalid;
                if (AwayResult == Constants.ResultType.Conceded) return AwayResult;
                if ((AggregatedAwayScore == AggregatedHomeScore) && AllGamesCompleted) return Constants.ResultType.Draw;
                if (AggregatedAwayScore >= MinimumGamesToWinSet) return Constants.ResultType.Win;
                if (AggregatedHomeScore >= MinimumGamesToWinSet) return Constants.ResultType.Loss;                
                return Constants.ResultType.Pending;
            }
        }
        
        public bool AwayWin { get { return AggregatedAwayResult == Constants.ResultType.Win; } }
        public bool HomeWin { get { return AggregatedHomeResult == Constants.ResultType.Win; } }
        public bool SetDrawn { get { return AggregatedAwayResult == Constants.ResultType.Draw; } }
        public bool AllGamesCompleted { get { return Games == null ? false : AggregatedHomeScore + AggregatedAwayScore == Games.Count(); } }

        private int MinimumGamesToWinSet
        {
            get
            {
                if (Games != null)
                {
                    if (Games.Count() == 1) return 1;

                    if (Games.Count() > 1)
                    {
                        var rem = Games.Count() % 2;
                        if (rem == 0)
                        {
                            return (Games.Count() / 2) + 1;
                        }
                        else
                        {
                            return (Games.Count() + 1) / 2;
                        }
                    }
                }
                return 0;
            }
        }
    }
}
