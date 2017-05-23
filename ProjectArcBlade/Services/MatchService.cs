using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;
using ProjectArcBlade.Models.MatchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class MatchService
    {
        ApplicationDbContext _context;

        public async Task<MatchTemplate> GetMatchTemplateBySeasonAndCategoryAsync(ApplicationDbContext context, int seasonId, int categoryId)
        {
            if (_context == null) _context = context;

            var matchTemplatesForSeason = await _context.MatchTemplateSeasons
                .Include(mts => mts.MatchTemplate)
                .Include(mts => mts.Season)
                .Where(mts => mts.Season.Id == seasonId)
                .Select(mts => mts.MatchTemplate)
                .ToListAsync();

            var matchTemplatesForCategory = await _context.MatchTemplateCategories
                .Include(mtc => mtc.MatchTemplate)
                .Include(mtc => mtc.Category)
                .Where(mtc => mtc.Category.Id == categoryId)
                .Select(mtc => mtc.MatchTemplate)
                .ToListAsync();

            if (matchTemplatesForSeason.Count != 0 && matchTemplatesForCategory.Count != 0)
            {
                var matchTemplate = new MatchTemplate();
                var intersectingMatchTemplates = matchTemplatesForSeason.Intersect(matchTemplatesForCategory).ToList();
                if (intersectingMatchTemplates.Count == 1)
                {
                    matchTemplate = intersectingMatchTemplates.First();
                }
                else
                {
                    if (matchTemplatesForCategory.Count == 1) matchTemplate = matchTemplatesForCategory.First();
                    if (matchTemplatesForSeason.Count == 1) matchTemplate = matchTemplatesForSeason.First();
                }

                if (matchTemplate.Id != 0)
                {
                    //get all the navigation properties for the matchTemplate

                    await _context.Entry(matchTemplate)
                        .Collection(mt => mt.GroupTemplates)
                        .Query()
                        .Include(gt => gt.Group)
                        .ToListAsync();

                    foreach (var groupTemplate in matchTemplate.GroupTemplates)
                    {
                        await _context.Entry(groupTemplate)
                            .Collection(gt => gt.RankTemplates)
                            .Query()
                            .Include(rt => rt.Rank)
                            .ToListAsync();
                    }

                    await _context.Entry(matchTemplate)
                        .Collection(mt => mt.MatchGameTemplates)
                        .Query()
                        .Include(mgt => mgt.HomeGroupTemplate)
                        .Include(mgt => mgt.AwayGroupTemplate)
                        .ToListAsync();

                    return matchTemplate;
                }
            }

            return null;
        }

        public async Task<List<RankTemplate>> GetRankTemplatesByMatchTemplateAsync(ApplicationDbContext context, int matchTemplateId)
        {
            if (_context == null) _context = context;

            var rankTemplates = await _context.RankTemplates
                .Include(rt => rt.GroupTemplate).ThenInclude(gt => gt.MatchTemplate)
                .Include(rt => rt.Rank)
                .Where(rt => rt.GroupTemplate.MatchTemplate.Id == matchTemplateId)
                .ToListAsync();

            return rankTemplates;
        }

        public async Task<List<GroupTemplate>> GetGroupTemplatesByMatchTemplateAsync(ApplicationDbContext context, int matchTemplateId)
        {
            if (_context == null) _context = context;

            var groupTemplates = await _context.GroupTemplates
                .Include(gt => gt.Group)
                .Include(gt => gt.RankTemplates)
                .Include(gt => gt.MatchTemplate)
                .Where(gt => gt.MatchTemplate.Id == matchTemplateId)
                .ToListAsync();

            return groupTemplates;
        }

        public async Task<List<Match>> GetAllLeagueMatchesByTeamAsync(ApplicationDbContext context, int teamId)
        {
            if (_context == null) _context = context;

            var allLeagueMatches = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.MatchStatus)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.ResultType)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.TeamStatus)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.ResultType)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.TeamStatus)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(m => (m.AwayMatchTeam.Team.Id == teamId || m.HomeMatchTeam.Team.Id == teamId)
                    && m.MatchType.Id == Constants.MatchType.League)
                .ToListAsync();

            return allLeagueMatches;
        }

        public async Task<PreviewMatchViewModel>GetPreviewMatchViewModelAsync(ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.TeamStatus)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.Division)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.Category)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.TeamStatus)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(m => m.Id == matchId)
                .SingleAsync();

            var awayTeamPlayers = await _context.AwayMatchTeamGroupPlayers
                .Include(amtgp => amtgp.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                .Include(amtgp => amtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                .Where(amtgp => amtgp.AwayMatchTeamGroup.AwayMatchTeam.Match.Id == matchId)
                .ToListAsync();

            var homeTeamPlayers = await _context.HomeMatchTeamGroupPlayers
                .Include(hmtgp => hmtgp.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                .Where(htmgp => htmgp.HomeMatchTeamGroup.HomeMatchTeam.Match.Id == matchId)
                .ToListAsync();

            var homeTeamCaptainId = await _context.HomeMatchTeamCaptains
                .Include(hmtc => hmtc.ClubPlayer)
                .Where(hmtc => hmtc.HomeMatchTeam.Id == match.HomeMatchTeam.Id && hmtc.ClubPlayer != null)
                .Select(hmtc => hmtc.ClubPlayer.Id)
                .FirstOrDefaultAsync();

            var awayTeamCaptainId = await _context.AwayMatchTeamCaptains
                .Include(amtc => amtc.ClubPlayer)
                .Where(amtc => amtc.AwayMatchTeam.Id == match.AwayMatchTeam.Id && amtc.ClubPlayer != null)
                .Select(amtc => amtc.ClubPlayer.Id)
                .FirstOrDefaultAsync();

            var team = await _context.Teams
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .SingleAsync(t => t.Id == teamId);

            var viewModel = new PreviewMatchViewModel
            {
                Team = team,
                Match = match,
                AwayTeamPlayers = awayTeamPlayers,
                HomeTeamPlayers = homeTeamPlayers,
                HomeTeamCaptainId = homeTeamCaptainId,
                AwayTeamCaptainId = awayTeamCaptainId
            };

            return viewModel;
        }

        public async Task StartMatch(ApplicationDbContext context, int matchId)
        {
            if (_context == null) _context = context;

            var match = await _context.Matches
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.ResultType)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.ResultType)
                .Where(m => m.Id == matchId)
                .SingleAsync();

            var resultTypePending = await _context.ResultTypes.FindAsync(Constants.ResultType.Pending);
            var matchStatusInProgress = await _context.MatchStatuses.FindAsync(Constants.MatchStatus.InProgress);

            match.AwayMatchTeam.ResultType = resultTypePending;
            match.HomeMatchTeam.ResultType = resultTypePending;
            match.MatchStatus = matchStatusInProgress;

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
        }

        private async Task<Match> GetMatchByIdAsync(int matchId)
        {
            var match = await  _context.Matches
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(m => m.Id == matchId)
                .SingleAsync();

            await _context.Entry(match)
                .Collection(m => m.Games)
                .Query()
                .Include(g => g.HomeGameResult).ThenInclude(hgr => hgr.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(g => g.AwayGameResult).ThenInclude(agr => agr.AwayMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .ToListAsync();

            foreach (var game in match.Games)
            {
                await _context.Entry(game.HomeGameResult)
                    .Collection(hgr => hgr.HomeGameResultScores)
                    .Query()
                    .Include(hgrs => hgrs.ScoreStatus)
                    .ToListAsync();

                await _context.Entry(game.AwayGameResult)
                    .Collection(g => g.AwayGameResultScores)
                    .Query()
                    .Include(agrs => agrs.ScoreStatus)
                    .ToListAsync();
            }

            return match;
        }

        public async Task<MatchProgressViewModel> GetMatchProgressViewModelAsync( ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);

            var team = await _context.Teams
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .SingleAsync(t => t.Id == teamId);

            var viewModel = new MatchProgressViewModel
            {
                Match = match,
                Team = team
            };

            return viewModel;
        }
    }
}
