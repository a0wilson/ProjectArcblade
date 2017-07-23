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
        public GameViewModel[] ContestedGames { get; set; }

        public string TeamName { get { return Match.IsHomeTeam ? Match.HomeTeamName : Match.AwayTeamName; } }
        public int MatchTeamId { get { return Match.IsHomeTeam ? Match.HomeMatchTeamId : Match.AwayMatchTeamId; } }

        public bool NoContestedGames { get { return Match.ContestedGamesTotal == 0; } }
        public bool AllowCompleteMatch { get { return !Match.MatchStatusComplete && NoContestedGames; } }

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
    }
}
