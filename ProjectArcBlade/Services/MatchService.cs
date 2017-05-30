using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;
using ProjectArcBlade.Models.MatchViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class MatchService
    {
        ApplicationDbContext _context;

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
                .Include(m => m.MatchHomeResult).ThenInclude(mhr => mhr.ResultType)
                .Include(m => m.MatchAwayResult).ThenInclude(amr => amr.ResultType)
                .Include(m => m.MatchStatus)
                .Where(m => m.Id == matchId)
                .SingleAsync();

            await _context.Entry(match)
                .Collection(m => m.Sets)
                .Query()
                .Include(s => s.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Include(s => s.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                .Include(s => s.SetHomeResult).ThenInclude(shr => shr.ResultType)
                .Include(s => s.SetAwayResult).ThenInclude(shr => shr.ResultType)
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

        public async Task<MatchProgressViewModel> GetMatchProgressViewModelAsync( ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);

            var setNumber = new int[match.Sets.Count];
            var setId = new int[match.Sets.Count];
            var homeGroupName = new string[match.Sets.Count];
            var awayGroupName = new string[match.Sets.Count];
            var homeScore = new int[match.Sets.Count]; //home games won
            var awayScore = new int[match.Sets.Count]; //away games won
            
            var isHomeTeam = match.HomeMatchTeam.Team.Id == teamId;

            var index = 0;
            foreach(var set in match.Sets)
            {
                setId[index] = set.Id;
                setNumber[index] = set.Number;
                homeGroupName[index] = set.HomeMatchTeamGroup.Group.Name;
                awayGroupName[index] = set.AwayMatchTeamGroup.Group.Name;

                int homeWinTotal = 0, awayWinTotal = 0;
                foreach (var game in set.Games)
                {
                    if (game.GameAwayResult.ResultType.Name == Constants.ResultType.Win) awayWinTotal++;
                    if (game.GameHomeResult.ResultType.Name == Constants.ResultType.Win) homeWinTotal++;
                }

                homeScore[index] = homeWinTotal;
                awayScore[index] = awayWinTotal;

                index++;
            }

            var viewModel = new MatchProgressViewModel
            {
                MatchId = matchId,
                TeamName = isHomeTeam ? match.HomeMatchTeam.Team.FullName : match.AwayMatchTeam.Team.FullName,
                IsHomeTeam = isHomeTeam,
                TeamId = teamId,
                SetId = setId,
                SetNumber = setNumber,
                SetTotal = match.Sets.Count,
                HomeTeamName = match.HomeMatchTeam.Team.FullName,
                AwayTeamName = match.AwayMatchTeam.Team.FullName,
                AwayScore = awayScore,
                HomeScore = homeScore,
                HomeGroupName = homeGroupName,
                AwayGroupName = awayGroupName
            };

            return viewModel;
        }

        public async Task UpdateMatchProgress(ApplicationDbContext context, MatchProgressViewModel viewModel)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(viewModel.MatchId);

            var isHomeGame = match.HomeMatchTeam.Team.Id == viewModel.TeamId;

            var index = 0;
            foreach (var game in match.Sets)
            {
                
                index++;
            }

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

        }

        public async Task<GameProgressViewModel> GetGameProgressViewModelAsync( ApplicationDbContext context, int setId, int teamId)
        {
            if (_context == null) _context = context;

            var set = await GetSetByIdAsync(context, setId);

            var gameId = new int[set.Games.Count];
            var gameNumber = new int[set.Games.Count];
            var awayAwayScore = new int[set.Games.Count];
            var awayHomeScore = new int[set.Games.Count];
            var homeAwayScore = new int[set.Games.Count];
            var homeHomeScore = new int[set.Games.Count];
            var awayAwayScoreStatus = new string[set.Games.Count];
            var awayHomeScoreStatus = new string[set.Games.Count];
            var homeAwayScoreStatus = new string[set.Games.Count];
            var homeHomeScoreStatus = new string[set.Games.Count];

            var i = 0;
            foreach(var game in set.Games)
            {
                gameId[i] = game.Id;
                gameNumber[i] = game.Number;
                awayAwayScore[i] = game.AwayTeamAwayTeamScore.Score;
                awayHomeScore[i] = game.AwayTeamHomeTeamScore.Score;
                homeAwayScore[i] = game.HomeTeamAwayTeamScore.Score;
                homeHomeScore[i] = game.HomeTeamHomeTeamScore.Score;
                awayAwayScoreStatus[i] = game.AwayTeamAwayTeamScore.ScoreStatus.Name;
                awayHomeScoreStatus[i] = game.AwayTeamHomeTeamScore.ScoreStatus.Name;
                homeAwayScoreStatus[i] = game.HomeTeamAwayTeamScore.ScoreStatus.Name;
                homeHomeScoreStatus[i] = game.HomeTeamHomeTeamScore.ScoreStatus.Name;
                i++;
            }

            var viewModel = new GameProgressViewModel
            {
                SetId = setId,
                TeamId = teamId,
                MatchId = set.Match.Id,
                GameId = gameId,
                GameNumber = gameNumber,
                GameTotal = set.Games.Count,
                AwayAwaySore = awayAwayScore,
                AwayHomeSore = awayHomeScore,
                HomeAwaySore = homeAwayScore,
                HomeHomeSore = homeHomeScore,
                AwayAwaySoreStatus = awayAwayScoreStatus,
                AwayHomeSoreStatus = awayHomeScoreStatus,
                HomeAwaySoreStatus = homeAwayScoreStatus,
                HomeHomeSoreStatus = homeHomeScoreStatus,
                AwayGroup = set.AwayMatchTeamGroup.Group.Name,
                HomeGroup = set.HomeMatchTeamGroup.Group.Name,
                AwayTeamName = set.Match.AwayMatchTeam.Team.FullName,
                HomeTeamName = set.Match.HomeMatchTeam.Team.FullName,
                IsHomeTeam = set.Match.HomeMatchTeam.Team.Id == teamId,
                TeamName = set.Match.HomeMatchTeam.Team.Id == teamId ? set.Match.HomeMatchTeam.Team.FullName : set.Match.AwayMatchTeam.Team.FullName
            };

            return viewModel;
        }

        public async Task UpdateGameProgressAsync(ApplicationDbContext context, GameProgressViewModel viewModel)
        {
            if (_context == null) _context = context;

            var set = await GetSetByIdAsync(_context, viewModel.SetId);
            var scoreStatusContested = await GetScoreStatusAsync(_context, Constants.ScoreStatus.Contested);
            var scoreStatusAccepted = await GetScoreStatusAsync(_context, Constants.ScoreStatus.Accepted);
            
            var i = 0;
            foreach( var game in set.Games)
            {
                if( viewModel.GameId[i] == game.Id )
                {
                    if(viewModel.IsHomeTeam)
                    {
                        if (viewModel.HomeAwaySore[i] != 0)
                        {
                            game.HomeTeamAwayTeamScore.Score = viewModel.HomeAwaySore[i];
                            game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                        }
                        if (viewModel.HomeHomeSore[i] != 0)
                        {
                            game.HomeTeamHomeTeamScore.Score = viewModel.HomeHomeSore[i];
                            game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                        }
                    }
                    else
                    {
                        if (viewModel.AwayAwaySore[i] != 0)
                        {
                            game.AwayTeamAwayTeamScore.Score = viewModel.AwayAwaySore[i];
                            game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                        }
                        if (viewModel.AwayHomeSore[i] != 0)
                        {
                            game.AwayTeamHomeTeamScore.Score = viewModel.AwayHomeSore[i];
                            game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                        }
                    }

                    if (game.AwayTeamAwayTeamScore.Score != 0 && game.HomeTeamAwayTeamScore.Score != 0)
                    {
                        if(game.AwayTeamAwayTeamScore.Score != game.HomeTeamAwayTeamScore.Score)
                        {
                            game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusContested;
                            game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusContested;
                        }
                    }

                    if(game.HomeTeamHomeTeamScore.Score != 0 && game.AwayTeamHomeTeamScore.Score != 0)
                    {
                        if(game.HomeTeamHomeTeamScore.Score != game.AwayTeamHomeTeamScore.Score)
                        {
                            game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusContested;
                            game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusContested;
                        }
                    }
                }
                i++;
            }

            //if any games have a score which is not zero then the set is in progress.
            var setInProgress = set.Games.Where(g =>
                g.AwayTeamAwayTeamScore.Score != 0 ||
                g.AwayTeamHomeTeamScore.Score != 0 ||
                g.HomeTeamAwayTeamScore.Score != 0 ||
                g.HomeTeamHomeTeamScore.Score != 0
            ).Any();

            if( set.SetStatus.Name != Constants.SetStatus.InProgress && setInProgress)
            {
                set.SetStatus = await GetSetStatusAsync(_context, Constants.SetStatus.InProgress);
            }

            _context.Sets.Update(set);
            await _context.SaveChangesAsync();
        }
    }
}
