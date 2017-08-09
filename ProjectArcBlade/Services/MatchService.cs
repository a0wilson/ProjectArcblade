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

        public MatchViewModel GetMatchViewModel(Match match, int teamId, bool includeSets, bool includeGroups)
        {
            var isHomeTeam = match.HomeMatchTeam.Team.Id == teamId;

            var homeGroups = includeGroups ? match.HomeMatchTeam.HomeMatchTeamGroups.Select(hmtg => GetHomeGroupViewModel(hmtg, isHomeTeam)).ToArray() : null;
            var awayGroups = includeGroups ? match.AwayMatchTeam.AwayMatchTeamGroups.Select(amtg => GetAwayGroupViewModel(amtg, isHomeTeam)).ToArray() : null;
            var sets = includeSets ? match.Sets.Select(s => GetSetViewModel(s, isHomeTeam)).ToArray() : null;
            var homeTeamSignOff = match.HomeScoreSheet == null ? false : match.HomeScoreSheet.SignedOff;
            var awayTeamSignOff = match.AwayScoreSheet == null ? false : match.AwayScoreSheet.SignedOff;

            var matchViewModel = new MatchViewModel
            {
                MatchId = match.Id,
                CategoryId = match.Category.Id,
                SeasonId = match.Season.Id,
                TeamId = teamId,
                VenueName = match.Venue.Name,
                StartDate = Convert.ToDateTime(match.StartDate).ToString(Constants.DateFormat.Short),
                Category = match.Category.Name,
                Division = match.Division.Name,
                Season = match.Season.Name,
                MatchStatus = match.MatchStatus.Name,

                HomeMatchTeamId = match.HomeMatchTeam.Id,
                HomeTeamId = match.HomeMatchTeam.Team.Id,
                HomeTeamName = match.HomeMatchTeam.Team.FullName,
                HomeTeamStatus = match.HomeMatchTeam.TeamStatus.Name,
                HomeResult = match.MatchHomeResult.ResultType.Name,
                HomeTeamSignOff = homeTeamSignOff,

                AwayMatchTeamId = match.AwayMatchTeam.Id,
                AwayTeamId = match.AwayMatchTeam.Team.Id,
                AwayTeamName = match.AwayMatchTeam.Team.FullName,
                AwayTeamStatus = match.AwayMatchTeam.TeamStatus.Name,
                AwayResult = match.MatchAwayResult.ResultType.Name,
                AwayTeamSignOff = awayTeamSignOff,
                                
                HomeGroups = homeGroups,
                AwayGroups = awayGroups,
                Sets = sets
            };

            return matchViewModel; 
        }
        
        public SetViewModel GetSetViewModel(Set set, bool IsHomeTeam)
        {
            var games = set.Games.Select(g => GetGameViewModel(g, IsHomeTeam)).ToArray();
            var matchSignOff = IsHomeTeam ? set.Match.HomeScoreSheet.SignedOff : set.Match.AwayScoreSheet.SignedOff;

            var setViewModel = new SetViewModel
            {
                Id = set.Id,
                Number = set.Number,
                Status = set.SetStatus.Name,
                IsHomeTeam = IsHomeTeam,
                SeasonId = set.Match.Season.Id,
                CategoryId = set.Match.Category.Id,
                MatchSignOff = matchSignOff,

                HomeGroup = set.HomeMatchTeamGroup.Group.Name,
                HomeResult = set.SetHomeResult.ResultType.Name,
                HomeTeamId = set.HomeMatchTeamGroup.HomeMatchTeam.Team.Id,
                HomeTeam = set.HomeMatchTeamGroup.HomeMatchTeam.Team.FullName,

                AwayTeamId = set.AwayMatchTeamGroup.AwayMatchTeam.Team.Id,
                AwayResult = set.SetAwayResult.ResultType.Name,
                AwayGroup = set.AwayMatchTeamGroup.Group.Name,
                AwayTeam = set.AwayMatchTeamGroup.AwayMatchTeam.Team.FullName,
                
                Games = games
            };

            return setViewModel;
        }
        
        private GameViewModel GetGameViewModel (Game game, bool IsHomeTeam)
        {
            var gameViewModel = new GameViewModel
            {
                Id = game.Id,
                SetId = game.Set.Id,
                SetNumber = game.Set.Number,
                Rule = game.Rule,
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
                AwayHomeScoreStatus = game.AwayTeamHomeTeamScore.ScoreStatus.Name
            };

            return gameViewModel;
        }
        
        private HomeGroupViewModel GetHomeGroupViewModel(HomeMatchTeamGroup matchTeamGroup, bool isHomeTeam)
        {
            var sets = matchTeamGroup.Sets.Select(s => GetSetViewModel(s, isHomeTeam)).ToArray();
            var players = matchTeamGroup.HomeMatchTeamGroupPlayers.Select(hmtgp => GetHomePlayerViewModel(hmtgp)).ToArray();
            var homeGroupViewModel = new HomeGroupViewModel
            {
                Id = matchTeamGroup.Id,
                GroupId = matchTeamGroup.Group.Id,
                Name = matchTeamGroup.Group.Name,
                Sets = sets,
                Players = players
            };

            return homeGroupViewModel;
        }

        private AwayGroupViewModel GetAwayGroupViewModel(AwayMatchTeamGroup matchTeamGroup, bool isHomeTeam)
        {
            var sets = matchTeamGroup.Sets.Select(s => GetSetViewModel(s, isHomeTeam)).ToArray();
            var players = matchTeamGroup.AwayMatchTeamGroupPlayers.Select(amtgp => GetAwayPlayerViewModel(amtgp)).ToArray();
            var awayGroupViewModel = new AwayGroupViewModel
            {
                Id = matchTeamGroup.Id,
                GroupId = matchTeamGroup.Group.Id,
                Name = matchTeamGroup.Group.Name,
                Sets = sets,
                Players = players
            };

            return awayGroupViewModel;
        }

        private PlayerViewModel GetHomePlayerViewModel(HomeMatchTeamGroupPlayer player)
        {
            return new PlayerViewModel
            {
                MatchTeamGroupPlayerId = player.Id,
                GroupId = player.HomeMatchTeamGroup.Group.Id,
                GroupName = player.HomeMatchTeamGroup.Group.Name,
                FullName = player.ClubPlayer != null ? player.ClubPlayer.PlayerDetail.FullName : Constants.NotApplicable,
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
                FullName = player.ClubPlayer != null ? player.ClubPlayer.PlayerDetail.FullName : Constants.NotApplicable,
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
                        .Include(gt => gt.DefaultRule)
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
                .Include(m => m.HomeScoreSheet)
                .Include(m => m.AwayScoreSheet)
                .Where(m => (m.AwayMatchTeam.Team.Id == teamId || m.HomeMatchTeam.Team.Id == teamId)
                    && m.MatchType.Name == Constants.MatchType.League)
                .ToListAsync();

            return allLeagueMatches;
        }

        public async Task<PreviewMatchViewModel> GetPreviewMatchViewModelAsync(ApplicationDbContext context, int matchId, int teamId)
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

            var match = await GetMatchByIdAsync(matchId);

            await UpdateHomeTeamScoreSheetAsync(match, match.HomeMatchTeam.Team.Id, false);
            await UpdateAwayTeamScoreSheetAsync(match, match.AwayMatchTeam.Team.Id, false);

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
            //get game rules... will take too long to get individually!
            var rules = await GetRulesAsync(_context);

            var match = await  _context.Matches
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.TeamStatus)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.TeamStatus)
                .Include(m => m.MatchHomeResult).ThenInclude(mhr => mhr.ResultType)
                .Include(m => m.MatchAwayResult).ThenInclude(amr => amr.ResultType)
                .Include(m => m.HomeScoreSheet)
                .Include(m => m.AwayScoreSheet)
                .Include(m => m.MatchStatus)
                .Include(m => m.MatchType)
                .Include(m => m.Category)
                .Include(m => m.Season)
                .Include(m => m.Division)
                .Include(m => m.Venue)
                .Include(m => m.Sets)
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

            await _context.Entry(match.HomeScoreSheet)
                .Collection(hss => hss.HomeScoreSheetLines)
                .Query()
                .Include(hssl => hssl.HomeMatchTeamGroup)
                .ToListAsync();

            await _context.Entry(match.AwayScoreSheet)
                .Collection(ass => ass.AwayScoreSheetLines)
                .Query()
                .Include(assl => assl.AwayMatchTeamGroup)
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
                    .Include(g => g.Set).ThenInclude(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                    .Include(g => g.Set).ThenInclude(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                    .Include(g => g.Rule)
                    .OrderBy(g => g.Number)
                    .ToListAsync();

                //update the game rules.
                foreach( var game in set.Games) game.Rule = rules.Where(r => r.Id == game.Rule.Id).Single();
                
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

            //get game rules... will take too long to get individually!
            var rules = await GetRulesAsync(_context);

            var set = await _context.Sets
                .Include(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                .Include(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(s => s.SetAwayResult).ThenInclude(sar => sar.ResultType)
                .Include(s => s.SetHomeResult).ThenInclude(shr => shr.ResultType)
                .Include(s => s.SetStatus)
                .Include(s => s.Match).ThenInclude(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(s => s.Match).ThenInclude(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(s => s.Match).ThenInclude(m => m.HomeScoreSheet)
                .Include(s => s.Match).ThenInclude(m => m.AwayScoreSheet)
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
                .Include(g => g.Set).ThenInclude(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(g => g.Set).ThenInclude(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                .Include(g => g.Rule)
                .OrderBy(g => g.Number)
                .ToListAsync();

            //update the game rules.
            foreach (var game in set.Games) game.Rule = rules.Where(r => r.Id == game.Rule.Id).Single();

            //await _context.Entry(set.HomeMatchTeamGroup)
            //        .Collection(hmtg => hmtg.HomeMatchTeamGroupPlayers)
            //        .Query()
            //        .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
            //        .ToListAsync();

            //await _context.Entry(set.AwayMatchTeamGroup)
            //    .Collection(amtg => amtg.AwayMatchTeamGroupPlayers)
            //    .Query()
            //    .Include(amtgp => amtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
            //    .ToListAsync();
            
            return set;
        }

        public async Task<Game>GetGameByIdAsync(ApplicationDbContext context, int gameId)
        {
            if (_context == null) _context = context;

            var game = await _context.Games
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
                .Include(g => g.Set).ThenInclude(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(g => g.Set).ThenInclude(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                .Include(g => g.Rule)
                .Where(g => g.Id == gameId)
                .SingleAsync();

            game.Rule = await GetRuleByIdAsync(_context, game.Rule.Id);

            return game;
        }

        public async Task<Rule>GetRuleByIdAsync(ApplicationDbContext context,int ruleId)
        {
            if (_context == null) _context = context;

            var rule = _context.Rules.Find(ruleId);
            if( rule != null)
            {
                await _context.Entry(rule)
                .Collection(g => g.ResultRules)
                .Query()
                .Include(rr => rr.Condition)
                .Include(rr => rr.JoinCondition)
                .Include(rr => rr.Operator)
                .Include(rr => rr.ResultType)
                .ToListAsync();
                foreach (var resultRule in rule.ResultRules)
                {
                    await _context.Entry(resultRule)
                        .Collection(rr => rr.TargetRules)
                        .Query()
                        .Include(rre => rre.TargetRule)
                        .Include(rre => rre.ExceptionRule)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.Condition)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.JoinCondition)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.Operator)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.ResultType)
                        .ToListAsync();
                }
            }

            return rule;
        }

        public async Task<List<Rule>> GetRulesAsync(ApplicationDbContext context)
        {
            if (_context == null) _context = context;

            var rules = await _context.Rules.ToListAsync();

            foreach( var rule in rules)
            {
                await _context.Entry(rule)
                .Collection(g => g.ResultRules)
                .Query()
                .Include(rr => rr.Condition)
                .Include(rr => rr.JoinCondition)
                .Include(rr => rr.Operator)
                .Include(rr => rr.ResultType)
                .ToListAsync();

                foreach (var resultRule in rule.ResultRules)
                {
                    await _context.Entry(resultRule)
                        .Collection(rr => rr.TargetRules)
                        .Query()
                        .Include(rre => rre.TargetRule)
                        .Include(rre => rre.ExceptionRule)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.Condition)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.JoinCondition)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.Operator)
                        .Include(rre => rre.ExceptionRule).ThenInclude(er => er.ResultType)
                        .ToListAsync();
                }
            }
            
            return rules;
        }

        public async Task<MatchProgressViewModel> GetMatchProgressViewModelAsync(ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);
            var matchViewModel = GetMatchViewModel(match, teamId, true, true);

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
            var matchViewModel = GetMatchViewModel(match, teamId, true, true);

            var viewModel = new MatchSummaryViewModel
            {
                TeamId = teamId,
                MatchId = matchId,
                Match = matchViewModel,
                ContestedGames = matchViewModel.Sets.SelectMany(s => s.ContestedGames.Select(g => g)).ToArray()
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
            var isHomeTeam = set.Match.HomeMatchTeam.Team.Id == teamId;
            var setViewModel = GetSetViewModel(set, isHomeTeam);
            
            var viewModel = new GameProgressViewModel
            {
                TeamId = teamId,
                MatchId = set.Match.Id,
                Set = setViewModel,
            };

            return viewModel;
        }
 
        public async Task<ServiceResult<Match>> ConcedeMatchAsync(ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);
            var matchViewModel = GetMatchViewModel(match, teamId, true, true);
            var matchTemplate = await GetMatchTemplateBySeasonAndCategoryAsync(_context, match.Season.Id, match.Category.Id);
            var resultTypes = await _context.ResultTypes.ToListAsync();
            var matchStatuses = await _context.MatchStatuses.ToListAsync();
            var scoreStatuses = await _context.ScoreStatuses.ToListAsync();
            var setStatuses = await _context.SetStatuses.ToListAsync();
            var matchStatusComplete = matchStatuses.Single(ms => ms.Name == Constants.MatchStatus.Complete);
            var setStatusComplete = setStatuses.Single(ss => ss.Name == Constants.SetStatus.Complete);
            var resultTypeConceded = resultTypes.Single(rt => rt.Name == Constants.ResultType.Conceded);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);
            var isHomeTeam = match.HomeMatchTeam.Team.Id == teamId;
            
            foreach(var set in match.Sets)
            {
                set.Games.ToList().ForEach(g => UpdateGameScore(g,  scoreStatuses, 0, 0, isHomeTeam, true, true));
                
                if (isHomeTeam)
                {
                    for (var i = 0; i < set.MinimumGamesToWinSet; i++)
                    {
                        var game = set.Games.ElementAt(i);
                        var gameTemplate = matchTemplate.SetTemplates.Where(st => st.Number == set.Number).Select(st => st.GameTemplates.Where(gt => gt.Number == game.Number).Single()).Single();
                        game = UpdateGameScore(game, scoreStatuses, gameTemplate.DefaultLossScore, gameTemplate.DefaultWinScore, isHomeTeam, true, true);
                        game = UpdateGameResult(game, resultTypes, isHomeTeam, true);
                    }

                    set.SetHomeResult.ResultType = resultTypeConceded;
                    set.SetAwayResult.ResultType = resultTypeWin;                    
                }
                else
                {
                    for (var i = 0; i < set.MinimumGamesToWinSet; i++)
                    {
                        var game = set.Games.ElementAt(i);
                        var gameTemplate = matchTemplate.SetTemplates.Where(st => st.Number == set.Number).Select(st => st.GameTemplates.Where(gt => gt.Number == game.Number).Single()).Single();
                        game = UpdateGameScore(game, scoreStatuses, gameTemplate.DefaultWinScore, gameTemplate.DefaultLossScore, isHomeTeam, true, true);
                        game = UpdateGameResult(game, resultTypes, isHomeTeam, true);
                    }
                    set.SetAwayResult.ResultType = resultTypeConceded;
                    set.SetHomeResult.ResultType = resultTypeWin;
                }

                set.SetStatus = setStatusComplete;
            }

            //set match result
            match.MatchHomeResult.ResultType = isHomeTeam ? resultTypeConceded : resultTypeWin;
            match.MatchAwayResult.ResultType = isHomeTeam ? resultTypeWin : resultTypeConceded;
           
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            await UpdateHomeTeamScoreSheetAsync(match, teamId, true);
            await UpdateAwayTeamScoreSheetAsync(match, teamId, true);

            var serviceResult = new ServiceResult<Match>()
            {
                ReturnValue = match,
                Success = true
            };

            return serviceResult;
        }

        public async Task<ServiceResult<GameProgressViewModel>> ConcedeGameAsync(ApplicationDbContext context, GameProgressViewModel viewModel)
        {
            if (_context == null) _context = context;

            var set = await GetSetByIdAsync(_context, viewModel.Set.Id);
            var matchTemplate = await GetMatchTemplateBySeasonAndCategoryAsync(_context, set.Match.Season.Id, set.Match.Category.Id);
            var scoreStatuses = await _context.ScoreStatuses.ToListAsync();
            var resultTypes = await _context.ResultTypes.ToListAsync();
            var setStatuses = await _context.SetStatuses.ToListAsync();
            var awayGroup = set.AwayMatchTeamGroup.Group.Name;
            var homeGroup = set.HomeMatchTeamGroup.Group.Name;
            var awayTeamName = set.Match.AwayMatchTeam.Team.FullName;
            var homeTeamName = set.Match.HomeMatchTeam.Team.FullName;            
            var resultTypeConceded = resultTypes.Single(rt => rt.Name == Constants.ResultType.Conceded);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);

            var setStatusComplete = setStatuses.Single(ss => ss.Name == Constants.SetStatus.Complete);
           
            //set all the games in the set to 0;
            set.Games.ToList().ForEach(g => UpdateGameScore(g, scoreStatuses, 0, 0, viewModel.Set.IsHomeTeam, true, true));
            
            if ( viewModel.Set.IsHomeTeam)
            {
                for (var i=0; i<set.MinimumGamesToWinSet; i++)
                {
                    var game = set.Games.ElementAt(i);
                    var gameTemplate = matchTemplate.SetTemplates.Where(st => st.Number == set.Number).Select(st => st.GameTemplates.Where(gt => gt.Number == game.Number).Single()).Single();
                    game = UpdateGameScore(game, scoreStatuses, gameTemplate.DefaultLossScore, gameTemplate.DefaultWinScore, viewModel.Set.IsHomeTeam, true, true);
                    game = UpdateGameResult(game, resultTypes, viewModel.Set.IsHomeTeam, true);
                }

                set.SetHomeResult.ResultType = resultTypeConceded;
                set.SetAwayResult.ResultType = resultTypeWin;
            }
            else
            {
                for (var i = 0; i < set.MinimumGamesToWinSet; i++)
                {
                    var game = set.Games.ElementAt(i);
                    var gameTemplate = matchTemplate.SetTemplates.Where(st => st.Number == set.Number).Select(st => st.GameTemplates.Where(gt => gt.Number == game.Number).Single()).Single();
                    game = UpdateGameScore(set.Games.ElementAt(i), scoreStatuses, gameTemplate.DefaultWinScore, gameTemplate.DefaultLossScore, viewModel.Set.IsHomeTeam, true, true);
                    game = UpdateGameResult(game, resultTypes, viewModel.Set.IsHomeTeam, true);
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

        /// <summary>
        /// Accepts a game model and updates the home and away scores and then returns the updated game model.
        /// </summary>
        /// <param name="game">The game model to operate on</param>
        /// <param name="rule">The rules used to determine if the scores are valid</param>
        /// <param name="scoreStatuses">A list of scores statuses which can be applied</param>
        /// <param name="homeScore">The home score</param>
        /// <param name="awayScore">The away score</param>
        /// <param name="isHomeTeam">true if the currently active team is the home team</param>
        /// <param name="forHomeTeam">true if the home and away scores should be submitted on behalf of the home team</param>
        /// <param name="forAwayTeam">true if the home and away scores should be submitted on behalf of the away team</param>
        /// <returns>The updated game model</returns>
        private Game UpdateGameScore(Game game, List<ScoreStatus> scoreStatuses, int homeScore, int awayScore, bool isHomeTeam, bool forHomeTeam, bool forAwayTeam)
        {            
            if(forHomeTeam)
            {
                game.HomeTeamHomeTeamScore.Score = homeScore;
                game.HomeTeamAwayTeamScore.Score = awayScore;
            }
            
            if(forAwayTeam)
            {
                game.AwayTeamHomeTeamScore.Score = homeScore;
                game.AwayTeamAwayTeamScore.Score = awayScore;
            }

            //get the gameviewmodel after updating the store to get the correct results!
            var gameViewModel = GetGameViewModel(game, isHomeTeam);

            game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatuses.Single(ss => ss.Name == (game.HomeTeamAwayTeamScore.Score == null ? Constants.ScoreStatus.NoEntry : gameViewModel.AggregatedAwayScoreStatus) );
            game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatuses.Single(ss => ss.Name == (game.HomeTeamHomeTeamScore.Score == null ? Constants.ScoreStatus.NoEntry : gameViewModel.AggregatedHomeScoreStatus) );
            game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatuses.Single(ss => ss.Name == (game.AwayTeamAwayTeamScore.Score == null ? Constants.ScoreStatus.NoEntry : gameViewModel.AggregatedAwayScoreStatus) );            
            game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatuses.Single(ss => ss.Name == (game.AwayTeamHomeTeamScore.Score == null ? Constants.ScoreStatus.NoEntry : gameViewModel.AggregatedHomeScoreStatus) );
            
            return game;
        }

        /// <summary>
        /// Accepts a game model and updates the results for the game and then returns the model.
        /// </summary>
        /// <param name="game">The game model to operate on</param>
        /// <param name="rule">The rules used to determine if the scores are valid</param>
        /// <param name="resultTypes">A list of the result types which can be applied</param>
        /// <param name="isHomeTeam">true if the currently active team is the home team</param>
        /// <param name="concedeGame">true if the game should be marked as conceded for teh active team</param>
        /// <returns>The updated game model</returns>
        private Game UpdateGameResult(Game game, List<ResultType> resultTypes, bool isHomeTeam, bool concedeGame)
        {
            var gameViewModel = GetGameViewModel(game, isHomeTeam);

            if (concedeGame)
            {
                if (isHomeTeam)
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

        public async Task<ServiceResult<SetViewModel>> UpdateSetAsync(ApplicationDbContext context, GameProgressViewModel viewModel)
        {
            if (_context == null) _context = context;

            var set = await GetSetByIdAsync(_context, viewModel.Set.Id);

            var scoreStatuses = await _context.ScoreStatuses.ToListAsync();
            var scoreStatusContested = scoreStatuses.Single(ss => ss.Name == Constants.ScoreStatus.Contested);
            var scoreStatusAccepted = scoreStatuses.Single(ss => ss.Name == Constants.ScoreStatus.Accepted);
            var scoreStatusNoEntry = scoreStatuses.Single(ss => ss.Name == Constants.ScoreStatus.NoEntry);

            var resultTypes = await _context.ResultTypes.ToListAsync();
            var resultTypePending = resultTypes.Single(rt => rt.Name == Constants.ResultType.Pending);
            var resultTypeDraw = resultTypes.Single(rt => rt.Name == Constants.ResultType.Draw);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);
            var resultTypeLoss = resultTypes.Single(rt => rt.Name == Constants.ResultType.Loss);
            var resultTypeInvalid = resultTypes.Single(rt => rt.Name == Constants.ResultType.Invalid);

            var contestedGamesExist = false;

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
                        else
                        {
                            game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusNoEntry;
                            game.HomeTeamAwayTeamScore.Score = null;
                        }

                        if (viewModel.Set.Games[i].HomeHomeScore != null)
                        {
                            if(game.HomeTeamHomeTeamScore.Score == null) game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.HomeTeamHomeTeamScore.Score = viewModel.Set.Games[i].HomeHomeScore.Value;
                        }
                        else
                        {
                            game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusNoEntry;
                            game.HomeTeamHomeTeamScore.Score = null;
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
                        else
                        {
                            game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusNoEntry;
                            game.AwayTeamAwayTeamScore.Score = null;
                        }

                        if (viewModel.Set.Games[i].AwayHomeScore != null)
                        {
                            if(game.AwayTeamHomeTeamScore.Score == null) game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.AwayTeamHomeTeamScore.Score = viewModel.Set.Games[i].AwayHomeScore.Value;
                        }
                        else
                        {
                            game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusNoEntry;
                            game.AwayTeamHomeTeamScore.Score = null;
                        }
                    }

                    //if both teams have entered scores for away team but the scores are different then mark as contested otherwise mark as accepted.
                    if (game.AwayTeamAwayTeamScore.Score != null && game.HomeTeamAwayTeamScore.Score != null)
                    {
                        if(game.AwayTeamAwayTeamScore.Score != game.HomeTeamAwayTeamScore.Score)
                        {
                            game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusContested;
                            game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusContested;
                            contestedGamesExist = true;
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
                            contestedGamesExist = true;
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
            
            //If contested games exist after the scores have been updated then resind match sign-off if required!
            if(contestedGamesExist) await RescindMatchSignOff(viewModel.MatchId, viewModel.TeamId);

            var setViewModel = GetSetViewModel(set, viewModel.Set.IsHomeTeam);
            
            var serviceResult = new ServiceResult<SetViewModel>()
            {
                Success = true,
                ReturnValue = setViewModel
            };
            return serviceResult;
        }

        public async Task<ServiceResult<MatchSummaryViewModel>> SignOffMatchAsync(ApplicationDbContext context, MatchSummaryViewModel viewModel)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(viewModel.MatchId);

            //Complete the team score sheet. 
            await CompleteTeamScoreSheetAsync(match, viewModel.TeamId);
            
            var serviceResult = new ServiceResult<MatchSummaryViewModel>()
            {
                Success = true,
                ReturnValue = viewModel
            };
            return serviceResult;
        }

        public async Task<ServiceResult<MatchSummaryViewModel>> UpdateContestedGamesAsync(ApplicationDbContext context, MatchSummaryViewModel viewModel, bool useOppostionScores)
        {
            if (_context == null) _context = context;

            var scoreStatuses = await _context.ScoreStatuses.ToListAsync();

            GameViewModel[] contestedGames = viewModel.Match.Sets.SelectMany(s => s.ContestedGames).ToArray();
            
            foreach(var contestedGame in contestedGames)
            {
                //get game.
                var game = await GetGameByIdAsync(_context, contestedGame.Id);
                int? homeScore = null, awayScore = null;

                //work out home and away score to use for update...
                if(viewModel.Match.IsHomeTeam)
                {
                    if (useOppostionScores)
                    {
                        homeScore = contestedGame.AwayHomeScore;
                        awayScore = contestedGame.AwayAwayScore;
                    }
                    else
                    {
                        homeScore = contestedGame.HomeHomeScore;
                        awayScore = contestedGame.HomeAwayScore;
                    }
                    
                    //Only update game if score has changed!
                    if(game.HomeTeamHomeTeamScore.Score != homeScore || game.HomeTeamAwayTeamScore.Score != awayScore )
                    {
                        game = UpdateGameScore(game, scoreStatuses, homeScore.Value, awayScore.Value, viewModel.Match.IsHomeTeam, true, false);
                        _context.Games.Update(game);
                        await _context.SaveChangesAsync();
                    }                    
                }
                else
                {
                    if (useOppostionScores)
                    {
                        homeScore = contestedGame.HomeHomeScore;
                        awayScore = contestedGame.HomeAwayScore;                        
                    }
                    else
                    {
                        homeScore = contestedGame.AwayHomeScore;
                        awayScore = contestedGame.AwayAwayScore;
                    }
                    //Only update game if score has changed!
                    if (game.AwayTeamHomeTeamScore.Score != homeScore || game.AwayTeamAwayTeamScore.Score != awayScore)
                    {                        
                        UpdateGameScore(game, scoreStatuses, homeScore.Value, awayScore.Value, viewModel.Match.IsHomeTeam, false, true);
                        _context.Games.Update(game);
                        await _context.SaveChangesAsync();
                    }
                }                
            }
            
            var serviceResult = new ServiceResult<MatchSummaryViewModel>()
            {
                Success = true,
                ReturnValue = viewModel
            };
            return serviceResult;
        }
        
        private async Task<bool> CompleteMatchAsync(Match match, int teamId)
        {
            
            //only complete match if it has been signed off by both teams!
            if (match.HomeScoreSheet == null || match.AwayScoreSheet == null) return false;
            if (!match.HomeScoreSheet.SignedOff || !match.AwayScoreSheet.SignedOff) return false;

            var matchViewModel = GetMatchViewModel(match, teamId, true, true);

            var resultTypes = await _context.ResultTypes.ToListAsync();
            var setStatuses = await _context.SetStatuses.ToListAsync();
            var matchStatuses = await _context.MatchStatuses.ToListAsync();

            var matchStatusComplete = matchStatuses.Single(ms => ms.Name == Constants.SetStatus.Complete);
            var setStatusCompleted = setStatuses.Single(ss => ss.Name == Constants.SetStatus.Complete);

            var setStatusInProgress = setStatuses.Single(ss => ss.Name == Constants.SetStatus.InProgress);
            var matchStatusInProgress = matchStatuses.Single(ms => ms.Name == Constants.MatchStatus.InProgress);
            
            match.MatchStatus = matchStatusInProgress;

            foreach (var set in match.Sets)
            {
                var setViewModel = matchViewModel.Sets.Where(s => s.Id == set.Id).Single();
                set.SetStatus = setStatusInProgress;

                foreach (var game in set.Games)
                {
                    var gameViewModel = setViewModel.Games.Where(g => g.Id == game.Id).Single();

                    //Update the scores
                    game.AwayTeamAwayTeamScore.Score = gameViewModel.AggregatedAwayScore;
                    game.AwayTeamHomeTeamScore.Score = gameViewModel.AggregatedHomeScore;
                    game.HomeTeamAwayTeamScore.Score = gameViewModel.AggregatedAwayScore;
                    game.HomeTeamHomeTeamScore.Score = gameViewModel.AggregatedHomeScore;
                    
                    game.GameHomeResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.AggregatedHomeResult);
                    game.GameAwayResult.ResultType = resultTypes.Single(rt => rt.Name == gameViewModel.AggregatedAwayResult);
                }

                if (setViewModel.SetIsComplete) set.SetStatus = setStatusCompleted;
                set.SetHomeResult.ResultType = resultTypes.Single(rt => rt.Name == setViewModel.AggregatedHomeResult);
                set.SetAwayResult.ResultType = resultTypes.Single(rt => rt.Name == setViewModel.AggregatedAwayResult);
            }

            if (matchViewModel.MatchIsComplete) match.MatchStatus = matchStatusComplete;
            match.MatchHomeResult.ResultType = resultTypes.Single(rt => rt.Name == matchViewModel.AggregatedHomeResult); ;
            match.MatchAwayResult.ResultType = resultTypes.Single(rt => rt.Name == matchViewModel.AggregatedAwayResult); ;

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task CompleteTeamScoreSheetAsync (Match match, int teamId)
        {
            var matchViewModel = GetMatchViewModel(match, teamId, true, true);

            if ( matchViewModel.IsHomeTeam)
            {
                await UpdateHomeTeamScoreSheetAsync(match, teamId, true);
            }
            else
            {
                await UpdateAwayTeamScoreSheetAsync(match, teamId, true);
            }
        }

        private async Task UpdateHomeTeamScoreSheetAsync(Match match, int teamId, bool signedOff)
        {
            //get the latest match view model.
            var matchViewModel = GetMatchViewModel(match, teamId, true, true);

            if (match.HomeScoreSheet == null)
            {
                //Create the home scoresheet.
                var homeScoreSheet = new HomeScoreSheet
                {
                    Match = match,
                    HomeMatchTeam = match.HomeMatchTeam,
                    SignedOff = signedOff
                };

                _context.HomeScoreSheets.Add(homeScoreSheet);
                await _context.SaveChangesAsync();

                foreach (var homeMatchTeamGroup in match.HomeMatchTeam.HomeMatchTeamGroups)
                {
                    var setTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == homeMatchTeamGroup.Group.Id).Select(sl => sl.HomeSetWinTotal).Single() : 0;
                    var gameTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == homeMatchTeamGroup.Group.Id).Select(sl => sl.HomeGameWinTotal).Single() : 0;
                    var pointTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == homeMatchTeamGroup.Group.Id).Select(sl => sl.HomePointsTotal).Single() : 0;

                    var homeScoreSheetLine = new HomeScoreSheetLine
                    {
                        HomeScoreSheet = homeScoreSheet,
                        HomeMatchTeamGroup = homeMatchTeamGroup,
                        SetTotal = setTotal,
                        GameTotal = gameTotal,
                        PointTotal = pointTotal,
                    };
                    _context.HomeScoreSheetLines.Add(homeScoreSheetLine);
                }

                //add the totals scoresheet line.
                var homeScoreSheetLineSumTotals = new HomeScoreSheetLine
                {
                    HomeScoreSheet = homeScoreSheet,
                    SetTotal = signedOff ? matchViewModel.HomeSetWinTotal : 0,
                    GameTotal = signedOff ? matchViewModel.HomeGameWinTotal : 0,
                    PointTotal = signedOff ? matchViewModel.HomePointsTotal : 0
                };
                _context.HomeScoreSheetLines.Add(homeScoreSheetLineSumTotals);
            }
            else
            {
                //update the scoresheet.
                match.HomeScoreSheet.SignedOff = signedOff;
                _context.HomeScoreSheets.Update(match.HomeScoreSheet);
                await _context.SaveChangesAsync();

                //set the indidvidual lines.
                foreach (var homeMatchTeamGroup in match.HomeMatchTeam.HomeMatchTeamGroups)
                {
                    var homeScoreSheetLine = match.HomeScoreSheet.HomeScoreSheetLines.Where(hssl => hssl.HomeMatchTeamGroup != null).Single(hssl => hssl.HomeMatchTeamGroup.Id == homeMatchTeamGroup.Id);
                    homeScoreSheetLine.SetTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == homeMatchTeamGroup.Group.Id).Select(sl => sl.HomeSetWinTotal).Single() : 0;
                    homeScoreSheetLine.GameTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == homeMatchTeamGroup.Group.Id).Select(sl => sl.HomeGameWinTotal).Single() : 0;
                    homeScoreSheetLine.PointTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == homeMatchTeamGroup.Group.Id).Select(sl => sl.HomePointsTotal).Single() : 0;
                    _context.HomeScoreSheetLines.Update(homeScoreSheetLine);
                }

                var homeScoreSheetLineSumTotals = match.HomeScoreSheet.HomeScoreSheetLines.Where(hssl => hssl.HomeMatchTeamGroup == null && hssl.HomeScoreSheet.Id == match.HomeScoreSheet.Id).Single();
                homeScoreSheetLineSumTotals.SetTotal = signedOff ? matchViewModel.HomeSetWinTotal : 0;
                homeScoreSheetLineSumTotals.GameTotal = signedOff ? matchViewModel.HomeGameWinTotal : 0;
                homeScoreSheetLineSumTotals.PointTotal = signedOff ? matchViewModel.HomePointsTotal : 0;
                _context.HomeScoreSheetLines.Update(homeScoreSheetLineSumTotals);
            }
            
            await _context.SaveChangesAsync();

            await CompleteMatchAsync(match, teamId);
        }

        private async Task UpdateAwayTeamScoreSheetAsync(Match match, int teamId, bool signedOff)
        {
            //get the latest match view model
            var matchViewModel = GetMatchViewModel(match, teamId, true, true);

            if ( match.AwayScoreSheet == null)
            {
                // create the awayteam scoresheet.
                var awayScoreSheet = new AwayScoreSheet
                {
                    Match = match,
                    AwayMatchTeam = match.AwayMatchTeam,
                    SignedOff = signedOff
                };

                _context.AwayScoreSheets.Add(awayScoreSheet);
                await _context.SaveChangesAsync();

                foreach (var awayMatchTeamGroup in match.AwayMatchTeam.AwayMatchTeamGroups)
                {
                    var setTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == awayMatchTeamGroup.Group.Id).Select(sl => sl.AwaySetWinTotal).Single() : 0;
                    var gameTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == awayMatchTeamGroup.Group.Id).Select(sl => sl.AwayGameWinTotal).Single() : 0;
                    var pointTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == awayMatchTeamGroup.Group.Id).Select(sl => sl.AwayPointsTotal).Single() : 0;

                    var awayScoreSheetLine = new AwayScoreSheetLine
                    {
                        AwayScoreSheet = awayScoreSheet,
                        AwayMatchTeamGroup = awayMatchTeamGroup,
                        SetTotal = setTotal,
                        GameTotal = gameTotal,
                        PointTotal = pointTotal,
                    };
                    _context.AwayScoreSheetLines.Add(awayScoreSheetLine);
                }

                //add the totals scoresheet line.
                var awayScoreSheetLineSumTotals = new AwayScoreSheetLine
                {
                    AwayScoreSheet = awayScoreSheet,
                    SetTotal = signedOff ? matchViewModel.AwaySetWinTotal : 0,
                    GameTotal = signedOff ? matchViewModel.AwayGameWinTotal : 0,
                    PointTotal = signedOff ? matchViewModel.AwayPointsTotal : 0
                };
                _context.AwayScoreSheetLines.Add(awayScoreSheetLineSumTotals);
            }
            else
            {
                //update the scoresheet.
                match.AwayScoreSheet.SignedOff = signedOff;
                _context.AwayScoreSheets.Update(match.AwayScoreSheet);
                await _context.SaveChangesAsync();

                //set the indidvidual lines.
                foreach (var awayMatchTeamGroup in match.AwayMatchTeam.AwayMatchTeamGroups)
                {
                    var awayScoreSheetLine = match.AwayScoreSheet.AwayScoreSheetLines.Where(assl => assl.AwayMatchTeamGroup != null).Single(assl => assl.AwayMatchTeamGroup.Id == awayMatchTeamGroup.Id);
                    awayScoreSheetLine.SetTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == awayMatchTeamGroup.Group.Id).Select(sl => sl.AwaySetWinTotal).Single() : 0;
                    awayScoreSheetLine.GameTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == awayMatchTeamGroup.Group.Id).Select(sl => sl.AwayGameWinTotal).Single() : 0;
                    awayScoreSheetLine.PointTotal = signedOff ? matchViewModel.ScoresheetLines.Where(sl => sl.GroupId == awayMatchTeamGroup.Group.Id).Select(sl => sl.AwayPointsTotal).Single() : 0;
                    _context.AwayScoreSheetLines.Update(awayScoreSheetLine);
                }

                var awayScoreSheetLineSumTotals = match.AwayScoreSheet.AwayScoreSheetLines.Where(hssl => hssl.AwayMatchTeamGroup == null && hssl.AwayScoreSheet.Id == match.HomeScoreSheet.Id).Single();
                awayScoreSheetLineSumTotals.SetTotal = signedOff ? matchViewModel.AwaySetWinTotal : 0;
                awayScoreSheetLineSumTotals.GameTotal = signedOff ? matchViewModel.AwayGameWinTotal : 0;
                awayScoreSheetLineSumTotals.PointTotal = signedOff ? matchViewModel.AwayPointsTotal : 0;
                _context.AwayScoreSheetLines.Update(awayScoreSheetLineSumTotals);
            }
            
            await _context.SaveChangesAsync();

            await CompleteMatchAsync(match, teamId);
        }

        private async Task RescindMatchSignOff(int matchId, int teamId)
        {
            var match = await GetMatchByIdAsync(matchId);
            var matchViewModel = GetMatchViewModel(match, teamId, true, true);

            if( matchViewModel.HomeTeamSignOff) await UpdateHomeTeamScoreSheetAsync(match, match.HomeMatchTeam.Team.Id, false);
            if (matchViewModel.AwayTeamSignOff) await UpdateAwayTeamScoreSheetAsync(match, match.AwayMatchTeam.Team.Id, false);
        }
    }
}
