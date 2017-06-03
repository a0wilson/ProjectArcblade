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
                //.Include(mtl => mtl.Season)
                //.Include(mtl => mtl.Category)
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

        public async Task<MatchProgressViewModel> GetMatchProgressViewModelAsync( ApplicationDbContext context, int matchId, int teamId)
        {
            if (_context == null) _context = context;

            var match = await GetMatchByIdAsync(matchId);

            var setNumber = new int[match.Sets.Count];
            var setId = new int[match.Sets.Count];
            var homeGroupName = new string[match.Sets.Count];
            var awayGroupName = new string[match.Sets.Count];
            var setStatus = new string[match.Sets.Count];
            var homeScore = new int[match.Sets.Count]; //home games won
            var awayScore = new int[match.Sets.Count]; //away games won
            var setHomeResult = new string[match.Sets.Count];
            var setAwayResult = new string[match.Sets.Count];

            var isHomeTeam = match.HomeMatchTeam.Team.Id == teamId;

            var index = 0;
            foreach(var set in match.Sets)
            {
                setId[index] = set.Id;
                setNumber[index] = set.Number;
                setStatus[index] = set.SetStatus.Name;
                homeGroupName[index] = set.HomeMatchTeamGroup.Group.Name;
                awayGroupName[index] = set.AwayMatchTeamGroup.Group.Name;
                homeScore[index] = set.Games.Where(g => g.GameHomeResult.ResultType.Name == Constants.ResultType.Win).Count();
                awayScore[index] = set.Games.Where(g => g.GameAwayResult.ResultType.Name == Constants.ResultType.Win).Count();
                setHomeResult[index] = set.SetHomeResult.ResultType.Name;
                setAwayResult[index] = set.SetAwayResult.ResultType.Name;

                index++;
            }

            var homeWin = match.Sets.Where(s => s.SetHomeResult.ResultType.Name == Constants.ResultType.Win).Count() >= match.MinimumSetsToWinMatch;
            var awayWin = match.Sets.Where(s => s.SetAwayResult.ResultType.Name == Constants.ResultType.Win).Count() >= match.MinimumSetsToWinMatch;

            var viewModel = new MatchProgressViewModel
            {
                MatchId = matchId,
                TeamName = isHomeTeam ? match.HomeMatchTeam.Team.FullName : match.AwayMatchTeam.Team.FullName,
                IsHomeTeam = isHomeTeam,
                TeamId = teamId,
                SetId = setId,
                SetNumber = setNumber,
                SetStatus = setStatus,
                SetAwayResult = setAwayResult,
                SetHomeResult = setHomeResult,
                SetTotal = match.Sets.Count,
                HomeTeamName = match.HomeMatchTeam.Team.FullName,
                AwayTeamName = match.AwayMatchTeam.Team.FullName,
                AwayScore = awayScore,
                HomeScore = homeScore,
                HomeGroupName = homeGroupName,
                AwayGroupName = awayGroupName,
                AllowMatchCompletion = homeWin || awayWin
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
            var awayAwayScore = new int?[set.Games.Count];
            var awayHomeScore = new int?[set.Games.Count];
            var homeAwayScore = new int?[set.Games.Count];
            var homeHomeScore = new int?[set.Games.Count];
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
                SeasonId = set.Match.Season.Id,
                CategoryId = set.Match.Category.Id,
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
            var resultTypeWin = await GetResultTypeAsync(_context, Constants.ResultType.Win);
            var resultTypeLoss = await GetResultTypeAsync(_context, Constants.ResultType.Loss);
            var resultTypeDraw = await GetResultTypeAsync(_context, Constants.ResultType.Draw);
            var resultTypePending = await GetResultTypeAsync(_context, Constants.ResultType.Pending);

            var i = 0;
            foreach( var game in set.Games)
            {
                if( viewModel.GameId[i] == game.Id )
                {
                    if(viewModel.IsHomeTeam)
                    {
                        //capture scores as entered by the home team
                        //only mark scores as being accepted on initial entry.
                        if (viewModel.HomeAwaySore[i] != null)
                        {
                            if(game.HomeTeamAwayTeamScore.Score == null) game.HomeTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.HomeTeamAwayTeamScore.Score = viewModel.HomeAwaySore[i];
                            
                        }
                        if (viewModel.HomeHomeSore[i] != null)
                        {
                            if(game.HomeTeamHomeTeamScore.Score == null) game.HomeTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.HomeTeamHomeTeamScore.Score = viewModel.HomeHomeSore[i];
                        }
                    }
                    else
                    {
                        //capture scores as entered by the away team
                        if (viewModel.AwayAwaySore[i] != null)
                        {
                            if(game.AwayTeamAwayTeamScore.Score == null) game.AwayTeamAwayTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.AwayTeamAwayTeamScore.Score = viewModel.AwayAwaySore[i];
                        }
                        if (viewModel.AwayHomeSore[i] != null)
                        {
                            if(game.AwayTeamHomeTeamScore.Score == null) game.AwayTeamHomeTeamScore.ScoreStatus = scoreStatusAccepted;
                            game.AwayTeamHomeTeamScore.Score = viewModel.AwayHomeSore[i];
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

                    game.GameHomeResult.ResultType = resultTypePending;
                    game.GameAwayResult.ResultType = resultTypePending;

                    if ((game.HomeTeamHomeTeamScoreAccepted && game.HomeTeamAwayTeamScoreAccepted) 
                        || (game.AwayTeamHomeTeamScoreAccepted && game.AwayTeamAwayTeamScoreAccepted))
                    {

                        if(game.HomeTeamHomeTeamScoreAccepted && game.HomeTeamAwayTeamScoreAccepted)
                        {
                            game.GameHomeResult.ResultType = await GetResultTypeAsync(_context, game.Set.Match.Season.Id, game.Set.Match.Category.Id, Convert.ToInt32(game.HomeTeamHomeTeamScore.Score), Convert.ToInt32(game.HomeTeamAwayTeamScore.Score));
                            game.GameAwayResult.ResultType = await GetResultTypeAsync(_context, game.Set.Match.Season.Id, game.Set.Match.Category.Id, Convert.ToInt32(game.HomeTeamAwayTeamScore.Score), Convert.ToInt32(game.HomeTeamHomeTeamScore.Score));
                        }

                        if (game.AwayTeamHomeTeamScoreAccepted && game.AwayTeamAwayTeamScoreAccepted)
                        {
                            game.GameHomeResult.ResultType = await GetResultTypeAsync(_context, game.Set.Match.Season.Id, game.Set.Match.Category.Id, Convert.ToInt32(game.AwayTeamHomeTeamScore.Score), Convert.ToInt32(game.AwayTeamAwayTeamScore.Score));
                            game.GameAwayResult.ResultType = await GetResultTypeAsync(_context, game.Set.Match.Season.Id, game.Set.Match.Category.Id, Convert.ToInt32(game.AwayTeamAwayTeamScore.Score), Convert.ToInt32(game.AwayTeamHomeTeamScore.Score));
                        }
                    }
                }
                i++;
            }

            //if any games have a score which is Accepted then the set is in progress.
            var setInProgress = set.Games.Where(g =>
                g.AwayTeamAwayTeamScoreAccepted ||
                g.AwayTeamHomeTeamScoreAccepted ||
                g.HomeTeamAwayTeamScoreAccepted ||
                g.HomeTeamHomeTeamScoreAccepted
            ).Any();

            if( set.SetStatus.Name != Constants.SetStatus.InProgress && setInProgress)
            {
                set.SetStatus = await GetSetStatusAsync(_context, Constants.SetStatus.InProgress);
            }

            // set result type to pending...
            set.SetHomeResult.ResultType = resultTypePending;
            set.SetAwayResult.ResultType = resultTypePending;

            // determine if the set can be marked as complete!
            var homeWinCount = set.Games.Where(g => g.GameHomeResult.ResultType.Name == Constants.ResultType.Win).Count();
            var awayWinCount = set.Games.Where(g => g.GameAwayResult.ResultType.Name == Constants.ResultType.Win).Count();

            if( homeWinCount >= set.MinimumGamesToWinSet || awayWinCount >= set.MinimumGamesToWinSet )
            {
                set.SetStatus = await GetSetStatusAsync(_context, Constants.SetStatus.Complete);

                if(homeWinCount >= set.MinimumGamesToWinSet)
                {
                    set.SetHomeResult.ResultType = resultTypeWin;
                    set.SetAwayResult.ResultType = resultTypeLoss;
                }

                if(awayWinCount >= set.MinimumGamesToWinSet)
                {
                    set.SetAwayResult.ResultType = resultTypeWin;
                    set.SetHomeResult.ResultType = resultTypeLoss;
                }
            }

            _context.Sets.Update(set);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a result type for score1 based on score2.
        /// Uses the ResltRules attached to a matchTemplate as the conditions.
        /// Will return result of win, loss, or draw, if unable to establish result, return pending.
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="categoryId"></param>
        /// <param name="score1"></param>
        /// <param name="score2"></param>
        /// <returns></returns>
        public async Task<ResultType> GetResultTypeAsync(ApplicationDbContext context, int seasonId, int categoryId, int score1, int score2)
        {
            var rule = await GetRuleBySeasonAndCategoryAsync(_context, seasonId, categoryId);
            var resultTypes = await _context.ResultTypes.ToListAsync();
            var resultTypePending = resultTypes.Single(rt => rt.Name == Constants.ResultType.Pending);
            var resultTypeDraw = resultTypes.Single(rt => rt.Name == Constants.ResultType.Draw);
            var resultTypeWin = resultTypes.Single(rt => rt.Name == Constants.ResultType.Win);
            var resultTypeLoss = resultTypes.Single(rt => rt.Name == Constants.ResultType.Loss);
            var resultTypeInvalid = resultTypes.Single(rt => rt.Name == Constants.ResultType.Invalid);

            if (rule != null)
            {
                //first check for invalid, then draw then loss then win otherwise pending (default)
                var invalidResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Invalid).ToList();
                var drawResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Draw).ToList();
                var lossResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Loss).ToList();
                var winResultRules = rule.ResultRules.Where(rr => rr.ResultType.Name == Constants.ResultType.Win).ToList();

                if (ValidateResultRule(invalidResultRules, score1, score2)) return resultTypeInvalid;
                if (ValidateResultRule(drawResultRules, score1, score2)) return resultTypeDraw;
                if (ValidateResultRule(lossResultRules, score1, score2)) return resultTypeLoss;
                if (ValidateResultRule(winResultRules, score1, score2)) return resultTypeWin;
            }

            return resultTypePending;
        }

        private bool ValidateResultRule(List<ResultRule> resultRules, int score1, int score2)
        {
            var ruleResult = new bool[resultRules.Count];
            string joinCondition = Constants.JoinCondition.And; //default to and.

            var i = 0;
            foreach(var rule in resultRules)
            {
                var ruleOperand = 0;

                joinCondition = rule.JoinCondition.Name;
                if (rule.UseOperator)
                {
                    switch(rule.Operator.Name)
                    {
                        case Constants.Operator.Add:
                            ruleOperand = score1 + score2;
                            break;
                        case Constants.Operator.Subtract:
                            ruleOperand = score1 - score2;
                            break;
                        case Constants.Operator.Divide:
                            ruleOperand = score1 / score2;
                            break;
                        case Constants.Operator.Multiply:
                            ruleOperand = score1 * score2;
                            break;
                        default:
                            break;
                    }
                }
                else
                {                    
                    if (rule.ScoreOne) ruleOperand = score1;
                    if (rule.ScoreTwo) ruleOperand = score2;
                }

                switch(rule.Condition.Name)
                {
                    case Constants.Condition.Equal:
                        ruleResult[i] = ruleOperand == rule.Value;
                        break;
                    case Constants.Condition.NotEqual:
                        ruleResult[i] = ruleOperand != rule.Value;
                        break;
                    case Constants.Condition.LessThan:
                        ruleResult[i] = ruleOperand < rule.Value;
                        break;
                    case Constants.Condition.GreaterThan:
                        ruleResult[i] = ruleOperand > rule.Value;
                        break;
                    default:
                        break;
                }

                i++;
            }

            switch(joinCondition)
            {
                case Constants.JoinCondition.And:
                    return ruleResult.Where(r => r == true).Count() == ruleResult.Count();
                case Constants.JoinCondition.Or:
                    return ruleResult.Where(r => r == true).Count() > 0;                    
            }
            
            return false;
        }
    }
}
