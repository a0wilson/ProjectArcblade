using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class MatchSummaryViewModel
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        
        public MatchViewModel Match { get; set; }
        
        public string TeamName { get { return Match.IsHomeTeam ? Match.HomeTeamName : Match.AwayTeamName; } }
        public int MatchTeamId { get { return Match.IsHomeTeam ? Match.HomeMatchTeamId : Match.AwayMatchTeamId; } }
        
        public bool AllowMatchSummary { get { return (Match.MatchStatusInProgress ? ((Match.HomeWin || Match.AwayWin) || Match.AllSetsCompleted ? true : false) : false); } }
        public bool AllowConcedeMatch { get { return Match.MatchStatusComplete ? false : (Match.HomeWin || Match.AwayWin) ? false : true; } }
        
        public MatchScoresheetLineViewModel[] ScoresheetLines
        {
            get
            {
                return Match.HomeGroups.Select(g =>
                    new MatchScoresheetLineViewModel
                    {
                        Heading = g.Name,
                        AwaySetWinTotal = Match.AwayGroups.Where(ag => ag.Name == g.Name).Select(ag => ag.SetWinTotal).Single(),
                        HomeSetWinTotal = Match.HomeGroups.Where(hg => hg.Name == g.Name).Select(hg => hg.SetWinTotal).Single(),
                        AwayGameWinTotal = Match.AwayGroups.Where(ag => ag.Name == g.Name).Select(ag => ag.GameWinTotal).Single(),
                        HomeGameWinTotal = Match.HomeGroups.Where(hg => hg.Name == g.Name).Select(hg => hg.GameWinTotal).Single(),
                        AwayPointsTotal = Match.AwayGroups.Where(ag => ag.Name == g.Name).Select(ag => ag.PointsTotal).Single(),
                        HomePointsTotal = Match.HomeGroups.Where(hg => hg.Name == g.Name).Select(hg => hg.PointsTotal).Single(),
                    }
                ).ToArray();
            }
        }

        //public string[] AwayGroups { get { return AwayPlayers.Select(ap => ap.GroupName).Distinct().ToArray(); } }
        //public string[] HomeGroups { get { return HomePlayers.Select(hp => hp.GroupName).Distinct().ToArray(); } }

        //public int HomeHomePoints { get { return Games.Sum(g => g.HomeHomeScore); } } 
        //public int HomeAwayPoints { get { return Games.Sum(g => g.HomeAwayScore); } }
        //public int AwayHomePoints { get { return Games.Sum(g => g.AwayHomeScore); } }
        //public int AwayAwayPoints { get { return Games.Sum(g => g.AwayAwayScore); } }
        
        //methods
        //public int GetHomeSetWinCount(string groupName) { return Sets.Where(s => s.HomeGroup == groupName && s.HomeResult == Constants.ResultType.Win).Count(); }
        //public int GetAwaySetWinCount(string groupName) { return Sets.Where(s => s.AwayGroup == groupName && s.AwayResult == Constants.ResultType.Win).Count(); }
        //public int GetHomeGameWinCount(string groupName) { return Games.Where(g => g.HomeGroup == groupName && g.HomeResult == Constants.ResultType.Win).Count(); }
        //public int GetAwayGameWinCount(string groupName) { return Games.Where(g => g.AwayGroup == groupName && g.AwayResult == Constants.ResultType.Win).Count(); }
        //public int GetHomeHomePoints(string groupName) { return Games.Where(g => g.HomeGroup == groupName).Sum(g => g.HomeHomeScore); }
        //public int GetHomeAwayPoints(string groupName) { return Games.Where(g => g.HomeGroup == groupName).Sum(g => g.HomeAwayScore); }
        //public int GetAwayHomePoints(string groupName) { return Games.Where(g => g.AwayGroup == groupName).Sum(g => g.AwayHomeScore); }
        //public int GetAwayAwayPoints(string groupName) { return Games.Where(g => g.AwayGroup == groupName).Sum(g => g.AwayAwayScore); }

        
    }
}
