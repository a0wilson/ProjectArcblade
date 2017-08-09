using ProjectArcBlade.Data;
using System.Linq;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchViewModel
    {
        public int TeamId { get; set; }
        public string MatchStatus { get; set; }
        public int MatchId { get; set; }
        public int SeasonId { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Division { get; set; }
        public string Season { get; set; }
                        
        public SetViewModel[] Sets { get; set; }
        public HomeGroupViewModel[] HomeGroups { get; set; }
        public AwayGroupViewModel[] AwayGroups { get; set; }

        public int AwayTeamId { get; set; }
        public int AwayMatchTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayResult { get; set; }
        public string AwayTeamStatus { get; set; }
        public bool AwayTeamSignOff { get; set; }
        
        public int HomeTeamId { get; set; }
        public int HomeMatchTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeResult { get; set; }
        public string HomeTeamStatus { get; set; }
        public bool HomeTeamSignOff { get; set; }

        public string VenueName { get; set; }
        public string StartDate { get; set; }
        
        public bool IsHomeTeam { get { return HomeTeamId == TeamId; } }
        public string Opponent { get { return IsHomeTeam ? AwayTeamName : HomeTeamName; } }
        public Constants.TeamType TeamType { get { return IsHomeTeam ? Constants.TeamType.Home : Constants.TeamType.Away; } }
        public string TeamStatus { get { return IsHomeTeam ? HomeTeamStatus : AwayTeamStatus; } }
        public string TeamResult { get { return IsHomeTeam ? HomeResult : AwayResult; } }

        public bool HomeWin { get { return AggregatedHomeResult == Constants.ResultType.Win; } }
        public bool AwayWin { get { return AggregatedAwayResult == Constants.ResultType.Win; } }
        public bool MatchDrawn { get { return AggregatedHomeResult == Constants.ResultType.Draw; } }
        public bool AllSetsCompleted { get { return (AggregatedHomeScore + AggregatedAwayScore) == Sets.Count(); } }
        public bool MatchStatusInProgress { get { return MatchStatus == Constants.MatchStatus.InProgress; } }
        public bool MatchStatusComplete { get { return MatchStatus == Constants.MatchStatus.Complete; } }
        public bool MatchIsComplete { get { return HomeWin || AwayWin || AllSetsCompleted; } }

        public int AggregatedHomeScore { get { return Sets == null ? 0 : Sets.Where(s => s.AggregatedHomeResult == Constants.ResultType.Win).Count(); } }
        public int AggregatedAwayScore { get { return Sets == null ? 0 : Sets.Where(s => s.AggregatedAwayResult == Constants.ResultType.Win).Count(); } }

        public string AggregatedHomeResult
        {
            get
            {
                if (Sets == null) return Constants.ResultType.Invalid;
                if (HomeResult == Constants.ResultType.Conceded) return HomeResult;
                if ((AggregatedAwayScore == AggregatedHomeScore ) && AllSetsCompleted) return Constants.ResultType.Draw;
                if (AggregatedHomeScore >= MinimumSetsToWinMatch) return Constants.ResultType.Win;
                if (AggregatedAwayScore >= MinimumSetsToWinMatch) return Constants.ResultType.Loss;
                return Constants.ResultType.Pending;
            }
        }

        public string AggregatedAwayResult
        {
            get
            {
                if (Sets == null) return Constants.ResultType.Invalid;
                if (AwayResult == Constants.ResultType.Conceded) return AwayResult;
                if ((AggregatedAwayScore == AggregatedHomeScore) && AllSetsCompleted) return Constants.ResultType.Draw;
                if (AggregatedAwayScore >= MinimumSetsToWinMatch) return Constants.ResultType.Win;
                if (AggregatedHomeScore >= MinimumSetsToWinMatch) return Constants.ResultType.Loss;
                return Constants.ResultType.Pending;
            }
        }

        public int HomeSetWinTotal { get { return Sets == null ? 0 : Sets.Where(s => s.AggregatedHomeResult == Constants.ResultType.Win).Count(); } }
        public int AwaySetWinTotal { get { return Sets == null ? 0 : Sets.Where(s => s.AggregatedAwayResult == Constants.ResultType.Win).Count(); } }
        public int HomeGameWinTotal { get { return Sets == null ? 0 : Sets.Sum(s => s.Games.Where(g => g.AggregatedHomeResult == Constants.ResultType.Win).Count()); } }
        public int AwayGameWinTotal { get { return Sets == null ? 0 : Sets.Sum(s => s.Games.Where(g => g.AggregatedAwayResult == Constants.ResultType.Win).Count()); } }
        public int HomePointsTotal { get { return Sets == null ? 0 : Sets.Sum(s => s.Games.Sum(g => g.AggregatedHomeScoreWithDefault)); } }
        public int AwayPointsTotal { get { return Sets == null ? 0 : Sets.Sum(s => s.Games.Sum(g => g.AggregatedAwayScoreWithDefault)); } }

        public int ContestedGamesTotal { get { return Sets == null ? 0 : Sets.Sum(s => s.ContestedGames.Count()); } }

        public bool SignedOff { get { return IsHomeTeam ? HomeTeamSignOff : AwayTeamSignOff; } }
        public bool SignedOffByOpponent { get { return IsHomeTeam ? AwayTeamSignOff : HomeTeamSignOff; } }

        public MatchScoresheetLineViewModel[] ScoresheetLines
        {
            get
            {
                if (HomeGroups == null || AwayGroups == null || AwayGroups.Any(ag => ag.Id == 0) || HomeGroups.Any(hg => hg.Id == 0)) return null;

                return HomeGroups.Select(g =>
                    new MatchScoresheetLineViewModel
                    {
                        GroupId = g.GroupId,
                        Heading = g.Name,
                        AwaySetWinTotal = AwayGroups.Where(ag => ag.Id == g.Id).Select(ag => ag.SetWinTotal).Single(),
                        HomeSetWinTotal = HomeGroups.Where(hg => hg.Id == g.Id).Select(hg => hg.SetWinTotal).Single(),
                        AwayGameWinTotal = AwayGroups.Where(ag => ag.Id == g.Id).Select(ag => ag.GameWinTotal).Single(),
                        HomeGameWinTotal = HomeGroups.Where(hg => hg.Id == g.Id).Select(hg => hg.GameWinTotal).Single(),
                        AwayPointsTotal = AwayGroups.Where(ag => ag.Id == g.Id).Select(ag => ag.PointsTotal).Single(),
                        HomePointsTotal = HomeGroups.Where(hg => hg.Id == g.Id).Select(hg => hg.PointsTotal).Single(),
                    }
                ).ToArray();
            }
        }

        public int MinimumSetsToWinMatch
        {
            get
            {
                if (Sets != null)
                {
                    if (Sets.Count() == 1) return 1;

                    if (Sets.Count() > 1)
                    {
                        var rem = Sets.Count() % 2;
                        if (rem == 0)
                        {
                            return (Sets.Count() / 2) + 1;
                        }
                        else
                        {
                            return (Sets.Count() + 1) / 2;
                        }
                    }
                }
                return 0;
            }
        }

    }
}
