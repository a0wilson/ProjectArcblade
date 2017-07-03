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

        public MatchViewModel GetMatchViewModel(Match match, Rule rule, int teamId, bool includeSets, bool includeGroups)
        {
            var isHomeTeam = match.HomeMatchTeam.Team.Id == teamId;
            return new MatchViewModel
            {
                MatchId = match.Id,
                TeamId = teamId,
                VenueName = match.Venue.Name,
                StartDate = Convert.ToDateTime(match.StartDate).ToString(Constants.DateFormat.Short),
                MinimumSetsToWin = match.MinimumSetsToWinMatch,
                Category = match.Category.Name,
                Division = match.Division.Name,
                Season = match.Season.Name,
                MatchStatus = match.MatchStatus.Name,

                HomeMatchTeamId = match.HomeMatchTeam.Id,
                HomeTeamId = match.HomeMatchTeam.Team.Id,
                HomeTeamName = match.HomeMatchTeam.Team.FullName,
                HomeTeamStatus = match.HomeMatchTeam.TeamStatus.Name,
                HomeResult = match.MatchHomeResult.ResultType.Name,

                AwayMatchTeamId = match.AwayMatchTeam.Id,
                AwayTeamId = match.AwayMatchTeam.Team.Id,
                AwayTeamName = match.AwayMatchTeam.Team.FullName,
                AwayTeamStatus = match.AwayMatchTeam.TeamStatus.Name,
                AwayResult = match.MatchAwayResult.ResultType.Name,
                
                Sets = includeSets ? match.Sets.Select(s => GetSetViewModel(s, rule, isHomeTeam)).ToArray() : null,
                HomeGroups = includeGroups ? match.HomeMatchTeam.HomeMatchTeamGroups.Select(hmtg => GetHomeGroupViewModel(hmtg, rule, isHomeTeam)).ToArray() : null,
                AwayGroups = includeGroups ? match.AwayMatchTeam.AwayMatchTeamGroups.Select(amtg => GetAwayGroupViewModel(amtg, rule, isHomeTeam)).ToArray() : null,
            };
        }
        
        public SetViewModel GetSetViewModel(Set set, Rule rule, bool IsHomeTeam)
        {
            return new SetViewModel
            {
                Id = set.Id,
                Number = set.Number,
                Status = set.SetStatus.Name,
                IsHomeTeam = IsHomeTeam,
                SeasonId = set.Match.Season.Id,
                CategoryId = set.Match.Category.Id,
                                
                HomeGroup = set.HomeMatchTeamGroup.Group.Name,
                HomeResult = set.SetHomeResult.ResultType.Name,
                HomeTeamId = set.HomeMatchTeamGroup.HomeMatchTeam.Team.Id,
                HomeTeam = set.HomeMatchTeamGroup.HomeMatchTeam.Team.FullName,

                AwayTeamId = set.AwayMatchTeamGroup.AwayMatchTeam.Team.Id,
                AwayResult = set.SetAwayResult.ResultType.Name,
                AwayGroup = set.AwayMatchTeamGroup.Group.Name,
                AwayTeam = set.AwayMatchTeamGroup.AwayMatchTeam.Team.FullName,

                Games = set.Games.Select(g => GetGameViewModel(g, rule, IsHomeTeam)).ToArray()
            };
        }
        
        private GameViewModel GetGameViewModel (Game game, Rule rule, bool IsHomeTeam)
        {
            return new GameViewModel
            {
                Id = game.Id,
                SetId = game.Set.Id,
                SetNumber = game.Set.Number,
                Rule = rule,
                IsHomeTeam = IsHomeTeam,
                GameNumber = game.Number,

                AwayGroup = game.Set.AwayMatchTeamGroup.Group.Name,
                AwayResult = game.GameAwayResult.ResultType.Name,
                AwayAwayScore = game.AwayTeamAwayTeamScore.Score,
                AwayAwayScoreStatus = game.AwayTeamAwayTeamScore.ScoreStatus.Name,
                HomeAwayScore = game.HomeTeamAwayTeamScore.Score,
                HomeAwayScoreStatus = game.HomeTeamAwayTeamScore.ScoreStatus.Name,

                HomeGroup = game.Set.HomeMatchTeamGroup.Group.Name,
                HomeResult = game.GameHomeResult.ResultType.Name,
                HomeHomeScore = game.HomeTeamHomeTeamScore.Score,
                HomeHomeScoreStatus = game.HomeTeamHomeTeamScore.ScoreStatus.Name,
                AwayHomeScore = game.AwayTeamHomeTeamScore.Score,
                AwayHomeScoreStatus = game.AwayTeamHomeTeamScore.ScoreStatus.Name,
                
            };
        }
        
        private HomeGroupViewModel GetHomeGroupViewModel(HomeMatchTeamGroup matchTeamGroup, Rule rule, bool isHomeTeam)
        {
            return new HomeGroupViewModel
            {
                Id = matchTeamGroup.Id,
                Name = matchTeamGroup.Group.Name,
                Sets = matchTeamGroup.Sets.Select(s => GetSetViewModel(s, rule, isHomeTeam)).ToArray(),
                Players = matchTeamGroup.HomeMatchTeamGroupPlayers.Select(hmtgp => GetHomePlayerViewModel(hmtgp)).ToArray(),
            };
        }

        private AwayGroupViewModel GetAwayGroupViewModel(AwayMatchTeamGroup matchTeamGroup, Rule rule, bool isHomeTeam)
        {
            return new AwayGroupViewModel
            {
                Id = matchTeamGroup.Id,
                Name = matchTeamGroup.Group.Name,
                Sets = matchTeamGroup.Sets.Select(s => GetSetViewModel(s, rule, isHomeTeam)).ToArray(),
                Players = matchTeamGroup.AwayMatchTeamGroupPlayers.Select(amtgp => GetAwayPlayerViewModel(amtgp)).ToArray(),
            };
        }

        private PlayerViewModel GetHomePlayerViewModel(HomeMatchTeamGroupPlayer player)
        {
            return new PlayerViewModel
            {
                MatchTeamGroupPlayerId = player.Id,
                GroupId = player.HomeMatchTeamGroup.Group.Id,
                GroupName = player.HomeMatchTeamGroup.Group.Name,
                FullName = player.ClubPlayer.PlayerDetail.FullName,
                MatchTeamId = player.HomeMatchTeamGroup.HomeMatchTeam.Id,
                MatchTeamGroupId = player.HomeMatchTeamGroup.Id
            };
        }

        private PlayerViewModel GetAwayPlayerViewModel(AwayMatchTeamGroupPlayer player)
        {
            return new PlayerViewModel
            {
                GroupId = player.AwayMatchTeamGroup.Group.Id,
                GroupName = player.AwayMatchTeamGroup.Group.Name,
                FullName = player.ClubPlayer.PlayerDetail.FullName,
                MatchTeamId = player.AwayMatchTeamGroup.AwayMatchTeam.Id,
                MatchTeamGroupId = player.AwayMatchTeamGroup.Id
            };
        }

        public async Task<MatchStatus> GetMatchStatusAsync(ApplicationDbContext context, string value)
        {
            if (_context == null) _context = context;
            return await _context.MatchStatuses.SingleAsync(lookup => lookup.Name == value);
        }

        public async Task<SetStatus> GetSetStatusAsync(ApplicationDbContext context, string value)
        {
            if (_context == null) _context = context;
            return await _context.SetStatuses.SingleAsync(lookup => lookup.Name == value);
        }

        public async Task<ScoreStatus> GetScoreStatusAsync(ApplicationDbContext context, string value)
        {
            if (_context == null) _context = context;
            return await _context.ScoreStatuses.SingleAsync(lookup => lookup.Name == value);
        }

        public async Task<ResultType> GetResultTypeAsync(ApplicationDbContext context, string value)
        {
            if (_context == null) _context = context;
            return await _context.ResultTypes.SingleAsync(lookup => lookup.Name == value);
        }

        public async Task<MatchType> GetMatchTypeAsync(ApplicationDbContext context, string value)
        {
            if (_context == null) _context = context;
            return await _context.MatchTypes.SingleAsync(lookup => lookup.Name == value);
        }

        public async Task<Rule> GetRuleBySeasonAndCategoryAsync(ApplicationDbContext context, int seasonId, int categoryId)
        {
            if (_context == null) _context = context;

            var rule = await _context.MatchTemplateLinks
                .Include(mtl => mtl.MatchTemplate)
                .Include(mtl => mtl.Rule)
                .Where(mtl => mtl.Season.Id == seasonId && mtl.Category.Id == categoryId)
                .Select(mtl => mtl.Rule)
                .SingleOrDefaultAsync();

            if (rule != null)
            {
                //load the result rules navigation property for the matchTemplate

                await _context.Entry(rule)
                    .Collection(mt => mt.ResultRules)
                    .Query()
                    .Include(rr => rr.Condition)
                    .Include(rr => rr.JoinCondition)
                    .Include(rr => rr.Operator)
                    .Include(rr => rr.ResultType)
                    .ToListAsync();
            }

            return rule;
        }

        public async Task<MatchTemplate> GetMatchTemplateBySeasonAndCategoryAsync(ApplicationDbContext context, int seasonId, int categoryId)
        {
            if (_context == null) _context = context;

            var matchTemplate = await _context.MatchTemplateLinks
                .Include(mtl => mtl.MatchTemplate)
                .Include(mtl => mtl.Season)
                .Include(mtl => mtl.Category)
                .Where(mtl => mtl.Season.Id == seasonId && mtl.Category.Id == categoryId)
                .Select(mts => mts.MatchTemplate)
                .SingleOrDefaultAsync();
            
            if (matchTemplate != null)
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
                    .Collection(mt => mt.SetTemplates)
                    .Query()
                    .Include(mgt => mgt.HomeGroupTemplate)
                    .Include(mgt => mgt.AwayGroupTemplate)
                    .ToListAsync();

                foreach(var setTemplate in matchTemplate.SetTemplates)
                {
                    await _context.Entry(setTemplate)
                        .Collection(st => st.GameTemplates)
                        .Query()
                        .ToListAsync();
                }
            }

            return matchTemplate;
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
                .Include(m => m.Season)
                .Include(m => m.Category)
                .Include(m => m.MatchStatus)
                .Include(m => m.MatchAwayResult).ThenInclude(amt => amt.ResultType)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.TeamStatus)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.MatchHomeResult).ThenInclude(hmt => hmt.ResultType)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.TeamStatus)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(m => (m.AwayMatchTeam.Team.Id == teamId || m.HomeMatchTeam.Team.Id == teamId)
                    && m.MatchType.Name == Constants.MatchType.League)
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
                .Where(amtgp => amtgp.AwayMatchTeamGroup.AwayMatchTeam.Match.Id == matchId && amtgp.ClubPlayer != null)
                .ToListAsync();

            var homeTeamPlayers = await _context.HomeMatchTeamGroupPlayers
                .Include(hmtgp => hmtgp.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                .Where(htmgp => htmgp.HomeMatchTeamGroup.HomeMatchTeam.Match.Id == matchId && htmgp.ClubPlayer != null)
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
                TeamId = teamId,
                MatchId = matchId,
                Venue = match.Venue.Name,
                Postcode = match.Venue.Postcode,
                StartDate = Convert.ToDateTime(match.StartDate).ToString(Constants.DateFormat.Long),
                StartTime = Convert.ToDateTime(match.StartTime).ToString(Constants.TimeFormat.Short),

                AwayTeamId = match.AwayMatchTeam.Team.Id,
                AwayMatchTeamId = match.AwayMatchTeam.Id,
                AwayTeamCaptainId = awayTeamCaptainId,
                AwayTeamName = match.AwayMatchTeam.Team.FullName,
                AwayTeamStatus = match.AwayMatchTeam.TeamStatus.Name,
                AwayTeamPlayerIds = awayTeamPlayers.Select(atp => atp.ClubPlayer.Id).ToArray(),
                AwayTeamPlayers = awayTeamPlayers.Select(atp => atp.ClubPlayer.PlayerDetail.FullName).ToArray(),
                AwayTeamPlayerGroups = awayTeamPlayers.Select(atp => atp.AwayMatchTeamGroup.Group.Name).ToArray(),

                HomeTeamId = match.HomeMatchTeam.Team.Id,
                HomeMatchTeamId = match.HomeMatchTeam.Id,
                HomeTeamCaptainId = homeTeamCaptainId,
                HomeTeamName = match.HomeMatchTeam.Team.FullName,
                HomeTeamStatus = match.HomeMatchTeam.TeamStatus.Name,
                HomeTeamPlayerIds = homeTeamPlayers.Select(htp => htp.ClubPlayer.Id).ToArray(),
                HomeTeamPlayers = homeTeamPlayers.Select(htp => htp.ClubPlayer.PlayerDetail.FullName).ToArray(),
                HomeTeamPlayerGroups = homeTeamPlayers.Select(htp => htp.HomeMatchTeamGroup.Group.Name).ToArray()
            };

            return viewModel;
        }

        public async Task StartMatch(ApplicationDbContext context, int matchId)
        {
            if (_context == null) _context = context;

            var match = await _context.Matches
                .Include(m => m.MatchAwayResult).ThenInclude(amt => amt.ResultType)
                .Include(m => m.MatchHomeResult).ThenInclude(hmt => hmt.ResultType)
                .Where(m => m.Id == matchId)
                .SingleAsync();

            var resultTypePending = await GetResultTypeAsync(_context, Constants.ResultType.Pending);
            var matchStatusInProgress = await GetMatchStatusAsync(_context, Constants.MatchStatus.InProgress);

            match.MatchAwayResult.ResultType = resultTypePending;
            match.MatchHomeResult.ResultType = resultTypePending;
            match.MatchStatus = matchStatusInProgress;

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
        }

        private async Task<Match> GetMatchByIdAsync(int matchId)
        {
            var match = await  _context.Matches
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.TeamStatus)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.TeamStatus)
                .Include(m => m.MatchHomeResult).ThenInclude(mhr => mhr.ResultType)
                .Include(m => m.MatchAwayResult).ThenInclude(amr => amr.ResultType)
                .Include(m => m.MatchStatus)
                .Include(m => m.MatchType)
                .Include(m => m.Category)
                .Include(m => m.Season)
                .Include(m => m.Division)
                .Include(m => m.Venue)
                .Where(m => m.Id == matchId)
                .SingleAsync();

            await _context.Entry(match)
                .Collection(m => m.Sets)
                .Query()
                .Include(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                .Include(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(s => s.SetHomeResult).ThenInclude(shr => shr.ResultType)
                .Include(s => s.SetAwayResult).ThenInclude(shr => shr.ResultType)
                .Include(s => s.Match).ThenInclude(m => m.Season)
                .Include(s => s.Match).ThenInclude(m => m.Category)
                .Include(s => s.Match).ThenInclude(m => m.Division)
                .Include(s => s.SetStatus)
                .OrderBy(s => s.Number)
                .ToListAsync();

            foreach (var set in match.Sets)
            {
                await _context.Entry(set)
                    .Collection(s => s.Games)
                    .Query()
                    .Include(g => g.GameAwayResult).ThenInclude(gar => gar.ResultType)
                    .Include(g => g.GameHomeResult).ThenInclude(ghr => ghr.ResultType)
                    .Include(g => g.AwayTeamAwayTeamScore).ThenInclude(atats => atats.ScoreStatus)
                    .Include(g => g.AwayTeamAwayTeamScore).ThenInclude(atats => atats.Audit)
                    .Include(g => g.AwayTeamHomeTeamScore).ThenInclude(athts => athts.ScoreStatus)
                    .Include(g => g.AwayTeamHomeTeamScore).ThenInclude(athts => athts.Audit)
                    .Include(g => g.HomeTeamAwayTeamScore).ThenInclude(htats => htats.ScoreStatus)
                    .Include(g => g.HomeTeamAwayTeamScore).ThenInclude(htats => htats.Audit)
                    .Include(g => g.HomeTeamHomeTeamScore).ThenInclude(hthts => hthts.ScoreStatus)
                    .Include(g => g.HomeTeamHomeTeamScore).ThenInclude(hthts => hthts.Audit)
                    .OrderBy(g => g.Number)
                    .ToListAsync();

                await _context.Entry(set.HomeMatchTeamGroup)
                    .Collection(hmtg => hmtg.HomeMatchTeamGroupPlayers)
                    .Query()
                    .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                    .ToListAsync();

                await _context.Entry(set.AwayMatchTeamGroup)
                    .Collection(amtg => amtg.AwayMatchTeamGroupPlayers)
                    .Query()
                    .Include(amtgp => amtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                    .ToListAsync();
            }

            return match;
        }

        
        public async Task<Set> GetSetByIdAsync(ApplicationDbContext context, int setId)
        {
            if (_context == null) _context = context;

            var set = await _context.Sets
                .Include(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                .Include(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(s => s.SetAwayResult).ThenInclude(sar => sar.ResultType)
                .Include(s => s.SetHomeResult).ThenInclude(shr => shr.ResultType)
                .Include(s => s.SetStatus)
                .Include(s => s.Match).ThenInclude(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(s => s.Match).ThenInclude(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(s => s.Match).ThenInclude(m => m.Category)
                .Include(s => s.Match).ThenInclude(m => m.Division)
                .Include(s => s.Match).ThenInclude(m => m.Season)
                .Where(s => s.Id == setId)
                .SingleAsync();

            //get associated games.
            await _context.Entry(set)
                .Collection(s => s.Games)
                .Query()
                .Include(g => g.GameAwayResult).ThenInclude(gar => gar.ResultType)
                .Include(g => g.GameHomeResult).ThenInclude(ghr => ghr.ResultType)
                .Include(g => g.AwayTeamAwayTeamScore).ThenInclude(atats => atats.ScoreStatus)
                .Include(g => g.AwayTeamAwayTeamScore).ThenInclude(atats => atats.Audit)
                .Include(g => g.AwayTeamHomeTeamScore).ThenInclude(athts => athts.ScoreStatus)
                .Include(g => g.AwayTeamHomeTeamScore).ThenInclude(athts => athts.Audit)
                .Include(g => g.HomeTeamAwayTeamScore).ThenInclude(htats => htats.ScoreStatus)
                .Include(g => g.HomeTeamAwayTeamScore).ThenInclude(htats => htats.Audit)
                .Include(g => g.HomeTeamHomeTeamScore).ThenInclude(hthts => hthts.ScoreStatus)
                .Include(g => g.HomeTeamHomeTeamScore).ThenInclude(hthts => hthts.Audit)
                .OrderBy(g => g.Number)
                .ToListAsync();
            
            return set;
        }

        public async Task<MatchProgressViewModel> GetMatchProgressViewModelAsync(ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);
            var rule = await GetRuleBySeasonAndCategoryAsync(_context, match.Season.Id, match.Category.Id);
            var matchViewModel = GetMatchViewModel(match, rule, teamId, true, true);

            var viewModel = new MatchProgressViewModel
            {
                TeamId = teamId,
                MatchId = matchId,
                Match = matchViewModel,
            };

            return viewModel;
        }

        public async Task<ServiceResult<MatchSummaryViewModel>> GetMatchSummaryViewModelAsync(ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);
            var rule = await GetRuleBySeasonAndCategoryAsync(_context, match.Season.Id, match.Category.Id);
            var matchViewModel = GetMatchViewModel(match, rule, teamId, true, true);
           
            var homeWin = match.Sets.Where(s => s.SetHomeResult.ResultType.Name == Constants.ResultType.Win).Count() >= match.MinimumSetsToWinMatch;
            var awayWin = match.Sets.Where(s => s.SetAwayResult.ResultType.Name == Constants.ResultType.Win).Count() >= match.MinimumSetsToWinMatch;

            var viewModel = new MatchSummaryViewModel
            {
                TeamId = teamId,
                MatchId = matchId,
                Match = matchViewModel,
            };

            var result = new ServiceResult<MatchSummaryViewModel>
            {
                ReturnValue = viewModel,
                Success = true
            };
            return result;
        }


        public async Task<GameProgressViewModel> GetGameProgressViewModelAsync( ApplicationDbContext context, int setId, int teamId)
        {
            if (_context == null) _context = context;

            var set = await GetSetByIdAsync(context, setId);
            var rule = await GetRuleBySeasonAndCategoryAsync(_context, set.Match.Season.Id, set.Match.Category.Id);
            var isHomeTeam = set.Match.HomeMatchTeam.Team.Id == teamId;
            var setViewModel = GetSetViewModel(set, rule, isHomeTeam);
            //var gameId = new int[set.Games.Count];
            //var gameNumber = new int[set.Games.Count];
            //var awayAwayScore = new int?[set.Games.Count];
            //var awayHomeScore = new int?[set.Games.Count];
            //var homeAwayScore = new int?[set.Games.Count];
            //var homeHomeScore = new int?[set.Games.Count];
            //var awayAwayScoreStatus = new string[set.Games.Count];
            //var awayHomeScoreStatus = new string[set.Games.Count];
            //var homeAwayScoreStatus = new string[set.Games.Count];
            //var homeHomeScoreStatus = new string[set.Games.Count];

            //var i = 0;
            //foreach(var game in set.Games)
            //{
            //    gameId[i] = game.Id;
            //    gameNumber[i] = game.Number;
            //    awayAwayScore[i] = game.AwayTeamAwayTeamScore.Score;
            //    awayHomeScore[i] = game.AwayTeamHomeTeamScore.Score;
            //    homeAwayScore[i] = game.HomeTeamAwayTeamScore.Score;
            //    homeHomeScore[i] = game.HomeTeamHomeTeamScore.Score;
            //    awayAwayScoreStatus[i] = game.AwayTeamAwayTeamScore.ScoreStatus.Name;
            //    awayHomeScoreStatus[i] = game.AwayTeamHomeTeamScore.ScoreStatus.Name;
            //    homeAwayScoreStatus[i] = game.HomeTeamAwayTeamScore.ScoreStatus.Name;
            //    homeHomeScoreStatus[i] = game.HomeTeamHomeTeamScore.ScoreStatus.Name;
            //    i++;
            //}

            var viewModel = new GameProgressViewModel
            {
                //SetId = setId,
                TeamId = teamId,
                MatchId = set.Match.Id,
                Set = setViewModel,
                //GameId = gameId,
                //GameNumber = gameNumber,
                //GameTotal = set.Games.Count,
                //AwayAwaySore = awayAwayScore,
                //AwayHomeSore = awayHomeScore,
                //HomeAwaySore = homeAwayScore,
                //HomeHomeSore = homeHomeScore,
                //SetAwayResult = set.SetAwayResult.ResultType.Name,
                //SetHomeResult = set.SetHomeResult.ResultType.Name,
                //AwayAwaySoreStatus = awayAwayScoreStatus,
                //AwayHomeSoreStatus = awayHomeScoreStatus,
                //HomeAwaySoreStatus = homeAwayScoreStatus,
                //HomeHomeSoreStatus = homeHomeScoreStatus,
                //AwayGroup = set.AwayMatchTeamGroup.Group.Name,
                //HomeGroup = set.HomeMatchTeamGroup.Group.Name,
                //AwayTeamName = set.Match.AwayMatchTeam.Team.FullName,
                //HomeTeamName = set.Match.HomeMatchTeam.Team.FullName,
                //IsHomeTeam = set.Match.HomeMatchTeam.Team.Id == teamId,
                //SeasonId = set.Match.Season.Id,
                //CategoryId = set.Match.Category.Id,
                //TeamName = set.Match.HomeMatchTeam.Team.Id == teamId ? set.Match.HomeMatchTeam.Team.FullName : set.Match.AwayMatchTeam.Team.FullName
            };

            return viewModel;
        }
 
        public async Task<ServiceResult<Match>> ConcedeMatchAsync(ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);
            var matchTemplate = await GetMatchTemplateBySeasonAndCategoryAsync(_context, match.Season.Id, match.Category.Id);
            var rule = await GetRuleBySeasonAndCategoryAsync(_context, match.Season.Id, match.Category.Id);
            var resultTypes = await _context.ResultTypes.ToListAsync();
            var matchStatuses = await _context.MatchStatuses.ToListAsync();
            var scoreStatuses = await _context.ScoreStatuses.ToListAsync();
            var setStatuses = await _context.SetStatuses.ToListAsync();
            var matchStatusComplete = matchStatuses.Single(ms => ms.Name == Constants.MatchStatus.Complete);
            var setStatusComplete = setStatuses.Single(ss => ss.Name == Constants.SetStatus.Complete);
            var scoreStatusAccepted = scoreStatuses.Single(ss => ss.Name == Constants.ScoreStatus.Accepted);
            var resultTypeConceded = resultTypes.Single(rt => rt.Name == Constants.ResultType.Conceded);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);
            var isHomeTeam = match.HomeMatchTeam.Team.Id == teamId ? true : false;

            foreach(var set in match.Sets)
            {
                set.Games.ToList().ForEach(g => SetGameScore(g, rule, scoreStatusAccepted, resultTypes, 0, 0, isHomeTeam));

                if (isHomeTeam)
                {
                    for (var i = 0; i < set.MinimumGamesToWinSet; i++)
                    {
                        var game = SetGameScore(set.Games.ElementAt(i), rule, scoreStatusAccepted, resultTypes, matchTemplate.DefaultGameLossScore, matchTemplate.DefaultGameWinScore, isHomeTeam, true);
                    }

                    set.SetHomeResult.ResultType = resultTypeConceded;
                    set.SetAwayResult.ResultType = resultTypeWin;                    
                }
                else
                {
                    for (var i = 0; i < set.MinimumGamesToWinSet; i++)
                    {
                        var game = SetGameScore(set.Games.ElementAt(i), rule, scoreStatusAccepted, resultTypes, matchTemplate.DefaultGameWinScore, matchTemplate.DefaultGameLossScore, isHomeTeam, true);
                    }
                    set.SetAwayResult.ResultType = resultTypeConceded;
                    set.SetHomeResult.ResultType = resultTypeWin;
                }

                set.SetStatus = setStatusComplete;
            }

            //set match result
            match.MatchHomeResult.ResultType = isHomeTeam ? resultTypeConceded : resultTypeWin;
            match.MatchAwayResult.ResultType = isHomeTeam ? resultTypeWin : resultTypeConceded;

            match.MatchStatus = matchStatusComplete;

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            var serviceResult = new ServiceResult<Match>()
            {
                Success = true
            };

            return serviceResult;
        }

        public async Task<ServiceResult<GameProgressViewModel>> ConcedeGameAsync(ApplicationDbContext context, GameProgressViewModel viewModel)
        {
            if (_context == null) _context = context;

            var set = await GetSetByIdAsync(_context, viewModel.Set.Id);
            var matchTemplate = await GetMatchTemplateBySeasonAndCategoryAsync(_context, set.Match.Season.Id, set.Match.Category.Id);
            var rule = await GetRuleBySeasonAndCategoryAsync(_context, set.Match.Season.Id, set.Match.Category.Id);
            var scoreStatuses = await _context.ScoreStatuses.ToListAsync();
            var resultTypes = await _context.ResultTypes.ToListAsync();
            var setStatuses = await _context.SetStatuses.ToListAsync();
            var awayGroup = set.AwayMatchTeamGroup.Group.Name;
            var homeGroup = set.HomeMatchTeamGroup.Group.Name;
            var awayTeamName = set.Match.AwayMatchTeam.Team.FullName;
            var homeTeamName = set.Match.HomeMatchTeam.Team.FullName;            
            var resultTypeConceded = resultTypes.Single(rt => rt.Name == Constants.ResultType.Conceded);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);
            var scoreStatusAccepted = scoreStatuses.Single(ss => ss.Name == Constants.ScoreStatus.Accepted);

            var setStatusComplete = setStatuses.Single(ss => ss.Name == Constants.SetStatus.Complete);
            

            //set all the games in the set to 0;
            set.Games.ToList().ForEach(g => SetGameScore(g, rule, scoreStatusAccepted, resultTypes, 0, 0, viewModel.Set.IsHomeTeam));
            
            if ( viewModel.Set.IsHomeTeam)
            {
                for (var i=0; i<set.MinimumGamesToWinSet; i++)
                {
                    var game = SetGameScore(set.Games.ElementAt(i), rule, scoreStatusAccepted, resultTypes, matchTemplate.DefaultGameLossScore, matchTemplate.DefaultGameWinScore, viewModel.Set.IsHomeTeam, true);
                }

                set.SetHomeResult.ResultType = resultTypeConceded;
                set.SetAwayResult.ResultType = resultTypeWin;
            }
            else
            {
                for (var i = 0; i < set.MinimumGamesToWinSet; i++)
                {
                    var game = SetGameScore(set.Games.ElementAt(i), rule, scoreStatusAccepted, resultTypes, matchTemplate.DefaultGameWinScore, matchTemplate.DefaultGameLossScore, viewModel.Set.IsHomeTeam, true);
                }
                set.SetAwayResult.ResultType = resultTypeConceded;
                set.SetHomeResult.ResultType = resultTypeWin;
            }

            set.SetStatus = setStatusComplete;

            _context.Sets.Update(set);
            await _context.SaveChangesAsync();

            var successMessage = String.Format("{0} - {1} pair have conceded the game against {2} - {3} pair", awayTeamName, awayGroup, homeTeamName, homeGroup);
            if (viewModel.Set.IsHomeTeam) successMessage = String.Format("{0} - {1} pair have conceded the game against {2} - {3} pair", homeTeamName, homeGroup, awayTeamName, awayGroup);
            
            var serviceResult = new ServiceResult<GameProgressViewModel>()
            {
                Success = true,
                SuccessMessage = successMessage
            };
            return serviceResult;
        }
        
        private Game SetGameScore(Game game, Rule rule, ScoreStatus acceptedStatus, List<ResultType> resultTypes, int homeScore, int awayScore, bool isHomeTeam, bool concedeGame = false)
        {
            var gameViewModel = GetGameViewModel(game, rule, true);
            game.HomeTeamAwayTeamScore.Score = awayScore;
            game.AwayTeamAwayTeamScore.Score = awayScore;
            game.HomeTeamHomeTeamScore.Score = homeScore;
            game.AwayTeamHomeTeamScore.Score = homeScore;

            game.HomeTeamAwayTeamScore.ScoreStatus = acceptedStatus;
            game.AwayTeamAwayTeamScore.ScoreStatus = acceptedStatus;
            game.HomeTeamHomeTeamScore.ScoreStatus = acceptedStatus;
            game.AwayTeamHomeTeamScore.ScoreStatus = acceptedStatus;

            if (concedeGame)
            {
                if(isHomeTeam)
                {
                    game.GameHomeResult.ResultType = resultTypes.Single(rt => rt.Name == Constants.ResultType.Conceded);
                    game.GameAwayResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.AggregatedAwayResult);
                }
                else
                {
                    game.GameHomeResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.AggregatedHomeResult);
                    game.GameAwayResult.ResultType = resultTypes.Single(rt => rt.Name == Constants.ResultType.Conceded);
                }
            }
            else
            {
                game.GameHomeResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.AggregatedHomeResult);
                game.GameAwayResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.AggregatedAwayResult);
            }            

            return game;
        }

        public async Task<ServiceResult<GameProgressViewModel>> UpdateSetAsync(ApplicationDbContext context, GameProgressViewModel viewModel)
        {
            if (_context == null) _context = context;

            var set = await GetSetByIdAsync(_context, viewModel.Set.Id);
            var scoreStatusContested = await GetScoreStatusAsync(_context, Constants.ScoreStatus.Contested);
            var scoreStatusAccepted = await GetScoreStatusAsync(_context, Constants.ScoreStatus.Accepted);

            var resultTypes = await _context.ResultTypes.ToListAsync();
            var resultTypePending = resultTypes.Single(rt => rt.Name == Constants.ResultType.Pending);
            var resultTypeDraw = resultTypes.Single(rt => rt.Name == Constants.ResultType.Draw);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);
            var resultTypeLoss = resultTypes.Single(rt => rt.Name == Constants.ResultType.Loss);
            var resultTypeInvalid = resultTypes.Single(rt => rt.Name == Constants.ResultType.Invalid);

            var i = 0;
            foreach( var game in set.Games)
            {
                if( viewModel.Set.Games[i].Id == game.Id )
                {
                    if(viewModel.Set.IsHomeTeam)
                    {
                        //capture scores as entered by the home team
                        //only mark scores as being accepted on initial entry.
                        if (viewModel.Set.Games[i].HomeAwayScore != null)
                        {
                            if(game.HomeTeamAwayTeamScore.Score == null) game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.HomeTeamAwayTeamScore.Score = viewModel.Set.Games[i].HomeAwayScore.Value;
                            
                        }
                        if (viewModel.Set.Games[i].HomeHomeScore != null)
                        {
                            if(game.HomeTeamHomeTeamScore.Score == null) game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.HomeTeamHomeTeamScore.Score = viewModel.Set.Games[i].HomeHomeScore.Value;
                        }
                    }
                    else
                    {
                        //capture scores as entered by the away team
                        if (viewModel.Set.Games[i].AwayAwayScore != null)
                        {
                            if(game.AwayTeamAwayTeamScore.Score == null) game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.AwayTeamAwayTeamScore.Score = viewModel.Set.Games[i].AwayAwayScore.Value;
                        }
                        if (viewModel.Set.Games[i].AwayHomeScore != null)
                        {
                            if(game.AwayTeamHomeTeamScore.Score == null) game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.AwayTeamHomeTeamScore.Score = viewModel.Set.Games[i].AwayHomeScore.Value;
                        }
                    }

                    //if both teams have entered scores for away team but the scores are different then mark as contested otherwise mark as accepted.
                    if (game.AwayTeamAwayTeamScore.Score != null && game.HomeTeamAwayTeamScore.Score != null)
                    {
                        if(game.AwayTeamAwayTeamScore.Score != game.HomeTeamAwayTeamScore.Score)
                        {
                            game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusContested;
                            game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusContested;
                        }
                        else
                        {
                            game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                        }
                    }

                    //if both teams have entered scores for home team but the scores are different then mark as contested otherwise mark as accepted.
                    if (game.HomeTeamHomeTeamScore.Score != null && game.AwayTeamHomeTeamScore.Score != null)
                    {
                        if(game.HomeTeamHomeTeamScore.Score != game.AwayTeamHomeTeamScore.Score)
                        {
                            game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusContested;
                            game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusContested;
                        }
                        else
                        {
                            game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                        }
                    }                                        
                }
                i++;
            }

            _context.Sets.Update(set);
            await _context.SaveChangesAsync();

            //Update the match results
            var match = await UpdateMatchResultsAsync(viewModel.MatchId, viewModel.TeamId);

            var rule = await GetRuleBySeasonAndCategoryAsync(_context, set.Match.Season.Id, set.Match.Category.Id);
            var matchViewModel = GetMatchViewModel(match, rule, viewModel.TeamId, true, true );

            //update the viewmodel with the set result so that the controller can determine which view to redirect to
            //if the set it completed - redirect to the match progress screen, otherwise stay on the game progress screen.
            viewModel.Set.AwayResult = matchViewModel.Sets.Where(s => s.Id == set.Id).Select(s => s.AggregatedAwayResult).Single();
            viewModel.Set.HomeResult = matchViewModel.Sets.Where(s => s.Id == set.Id).Select(s => s.AggregatedHomeResult).Single();
            
            var serviceResult = new ServiceResult<GameProgressViewModel>()
            {
                Success = true,
                ReturnValue = viewModel
            };
            return serviceResult;
        }

        private async Task<Match> UpdateMatchResultsAsync(int matchId, int matchTeamId)
        {
            var match = await GetMatchByIdAsync(matchId);
            var resultTypes = await _context.ResultTypes.ToListAsync();
            var setStatuses = await _context.SetStatuses.ToListAsync();
            var matchStatuses = await _context.MatchStatuses.ToListAsync();
            var rule = await GetRuleBySeasonAndCategoryAsync(_context, match.Season.Id, match.Category.Id);

            var resultTypePending = resultTypes.Single(rt => rt.Name == Constants.ResultType.Pending);
            var resultTypeDraw = resultTypes.Single(rt => rt.Name == Constants.ResultType.Draw);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);
            var resultTypeLoss = resultTypes.Single(rt => rt.Name == Constants.ResultType.Loss);
            var resultTypeInvalid = resultTypes.Single(rt => rt.Name == Constants.ResultType.Invalid);

            var setStatusCompleted = setStatuses.Single(ss => ss.Name == Constants.SetStatus.Complete);
            var setStatusInProgress = setStatuses.Single(ss => ss.Name == Constants.SetStatus.InProgress);

            var matchStatusCompleted = matchStatuses.Single(ms => ms.Name == Constants.MatchStatus.Complete);
            var matchStatusInProgress = matchStatuses.Single(ms => ms.Name == Constants.MatchStatus.InProgress);
            
            foreach (var set in match.Sets)
            {
                foreach(var game in set.Games)
                {
                    var gameViewModel = GetGameViewModel(game, rule, true);

                    game.GameHomeResult.ResultType = resultTypePending;
                    game.GameAwayResult.ResultType = resultTypePending;

                    if ((game.HomeTeamHomeTeamScoreAccepted && game.HomeTeamAwayTeamScoreAccepted)
                        || (game.AwayTeamHomeTeamScoreAccepted && game.AwayTeamAwayTeamScoreAccepted))
                    {

                        if (game.HomeTeamHomeTeamScoreAccepted && game.HomeTeamAwayTeamScoreAccepted)
                        {
                            game.GameHomeResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.GetResultType(rule, Convert.ToInt32(game.HomeTeamHomeTeamScore.Score), Convert.ToInt32(game.HomeTeamAwayTeamScore.Score)));
                            game.GameAwayResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.GetResultType(rule, Convert.ToInt32(game.HomeTeamAwayTeamScore.Score), Convert.ToInt32(game.HomeTeamHomeTeamScore.Score)));
                        }

                        if (game.AwayTeamHomeTeamScoreAccepted && game.AwayTeamAwayTeamScoreAccepted)
                        {
                            game.GameHomeResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.GetResultType(rule, Convert.ToInt32(game.AwayTeamHomeTeamScore.Score), Convert.ToInt32(game.AwayTeamAwayTeamScore.Score)));
                            game.GameAwayResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.GetResultType(rule, Convert.ToInt32(game.AwayTeamAwayTeamScore.Score), Convert.ToInt32(game.AwayTeamHomeTeamScore.Score)));
                        }
                    }
                }

                //if any games have a score which is Accepted then the set is in progress.
                var setInProgress = set.Games.Where(g =>
                    g.AwayTeamAwayTeamScoreAccepted ||
                    g.AwayTeamHomeTeamScoreAccepted ||
                    g.HomeTeamAwayTeamScoreAccepted ||
                    g.HomeTeamHomeTeamScoreAccepted
                ).Any();

                if (set.SetStatus.Name != Constants.SetStatus.InProgress && setInProgress)
                {
                    set.SetStatus = setStatusInProgress;
                }

                // set result type to pending...
                set.SetHomeResult.ResultType = resultTypePending;
                set.SetAwayResult.ResultType = resultTypePending;

                // determine if the set can be marked as complete!
                var homeWinCount = set.Games.Where(g => g.GameHomeResult.ResultType.Name == Constants.ResultType.Win).Count();
                var awayWinCount = set.Games.Where(g => g.GameAwayResult.ResultType.Name == Constants.ResultType.Win).Count();

                if (homeWinCount >= set.MinimumGamesToWinSet || awayWinCount >= set.MinimumGamesToWinSet)
                {
                    set.SetStatus = setStatusCompleted;

                    if (homeWinCount >= set.MinimumGamesToWinSet)
                    {
                        set.SetHomeResult.ResultType = resultTypeWin;
                        set.SetAwayResult.ResultType = resultTypeLoss;
                    }

                    if (awayWinCount >= set.MinimumGamesToWinSet)
                    {
                        set.SetAwayResult.ResultType = resultTypeWin;
                        set.SetHomeResult.ResultType = resultTypeLoss;
                    }
                }
            }

            int homeSetWinCount = match.Sets.Where(s => s.SetHomeResult.ResultType.Name == Constants.ResultType.Win).Count();
            int awaySetWinCount = match.Sets.Where(s => s.SetAwayResult.ResultType.Name == Constants.ResultType.Win).Count();
            bool allSetsCompleted = match.Sets.Where(s => s.SetStatus.Name == Constants.SetStatus.Complete).Count() == match.Sets.Count();
            bool homeWin = homeSetWinCount > match.MinimumSetsToWinMatch;
            bool awayWin = awaySetWinCount > match.MinimumSetsToWinMatch;
            bool matchDrawn = (allSetsCompleted && !homeWin && !awayWin) ? true : false;

            match.MatchHomeResult.ResultType = resultTypePending;
            match.MatchAwayResult.ResultType = resultTypePending;

            if( (homeWin || awayWin) || allSetsCompleted)
            {
                if(matchDrawn)
                {
                    match.MatchAwayResult.ResultType = resultTypeDraw;
                    match.MatchHomeResult.ResultType = resultTypeDraw;
                }
                if(homeWin)
                {
                    match.MatchHomeResult.ResultType = resultTypeWin;
                    match.MatchAwayResult.ResultType = resultTypeLoss;
                }
                if(awayWin)
                {
                    match.MatchAwayResult.ResultType = resultTypeWin;
                    match.MatchHomeResult.ResultType = resultTypeLoss;
                }                
            }

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            return match;
        }
        
        public async Task<ServiceResult<MatchSummaryViewModel>> CompleteMatchAsync(ApplicationDbContext context, MatchSummaryViewModel viewModel)
        {
            if (_context == null) _context = context;

            var matchStatuses = await _context.MatchStatuses.ToListAsync();
            var matchStatusComplete = matchStatuses.Single(ms => ms.Name == Constants.SetStatus.Complete);
            var resultTypes = await _context.ResultTypes.ToListAsync();

            var match = await GetMatchByIdAsync(viewModel.MatchId);

            match.MatchStatus = matchStatusComplete;
            match.MatchHomeResult.ResultType = resultTypes.Single(rt => rt.Name == viewModel.Match.AggregatedHomeResult);
            match.MatchAwayResult.ResultType = resultTypes.Single(rt => rt.Name == viewModel.Match.AggregatedAwayResult);

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            var serviceResult = new ServiceResult<MatchSummaryViewModel>()
            {
                Success = true,
                ReturnValue = viewModel
            };
            return serviceResult;
        }
    }
}
