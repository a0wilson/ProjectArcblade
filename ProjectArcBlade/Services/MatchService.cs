using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;
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

        public async Task<List<Match>> GetAllLeagueMatchesByTeamAsycn(ApplicationDbContext context, int teamId)
        {
            if (_context == null) _context = context;

            var allLeagueMatches = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.ResultType)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.ResultType)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(m => (m.AwayMatchTeam.Team.Id == teamId || m.HomeMatchTeam.Team.Id == teamId)
                    && m.MatchType.Id == Constants.MatchType.League)
                .ToListAsync();

            return allLeagueMatches;
        }


    }
}
