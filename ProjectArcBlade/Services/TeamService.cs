using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Interfaces;
using ProjectArcBlade.Models;
using ProjectArcBlade.Models.TeamViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class TeamService
    {
        private ApplicationDbContext _context;
                
        public async Task UpdateMatchTeamCaptainAsync<T>(ApplicationDbContext context, int matchTeamId, int captainId) where T : IMatchTeam
        {
            if (_context == null) _context = context;

            if (typeof(T).Equals(typeof(HomeMatchTeam))) //Home Match Team
            {
                await UpdateHomeMatchTeamCaptainAsync(matchTeamId, captainId);
            }
            else // assume away team.
            {
                await UpdateAwayMatchTeamCaptainAsync(matchTeamId, captainId);
            }
        }

        /// <summary>
        /// Updates the home match team captain - creates record if it does not exist.
        /// </summary>
        /// <param name="homeMatchTeamId"></param>
        /// <param name="captainId"></param>
        /// <returns></returns>
        private async Task UpdateHomeMatchTeamCaptainAsync( int homeMatchTeamId, int captainId)
        {
            var homeMatchTeam = await _context.HomeMatchTeams.FindAsync(homeMatchTeamId);
            var homeMatchTeamCaptain = await GetHomeMatchTeamCaptainAsync(homeMatchTeamId, homeMatchTeam);

            if(captainId != 0)
            {
                var clubPlayer = await _context.ClubPlayers.FindAsync(captainId);
                homeMatchTeamCaptain.ClubPlayer = clubPlayer;
                _context.HomeMatchTeamCaptains.Update(homeMatchTeamCaptain);
            }
            else
            {
                //if 0 then remove the club player as captain.
                homeMatchTeamCaptain.ClubPlayer = null;
                _context.HomeMatchTeamCaptains.Update(homeMatchTeamCaptain);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the away match team captain - creates the record if it does not exist.
        /// </summary>
        /// <param name="awayMatchTeamId"></param>
        /// <param name="captainId"></param>
        /// <returns></returns>
        private async Task UpdateAwayMatchTeamCaptainAsync(int awayMatchTeamId, int captainId)
        {
            var awayMatchTeam = await _context.AwayMatchTeams.FindAsync(awayMatchTeamId);
            var awayMatchTeamCaptain = await GetAwayMatchTeamCaptainAsync(awayMatchTeamId, awayMatchTeam);

            if (captainId != 0)
            {
                var clubPlayer = await _context.ClubPlayers.FindAsync(captainId);
                awayMatchTeamCaptain.ClubPlayer = clubPlayer;
                _context.AwayMatchTeamCaptains.Update(awayMatchTeamCaptain);
            }
            else
            {
                //if 0 then remove the club player as captain.
                awayMatchTeamCaptain.ClubPlayer = null;
                _context.AwayMatchTeamCaptains.Update(awayMatchTeamCaptain);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asyncrhonously gets the home match team captain 
        /// If a record does not exist it creates it setting the home match team in the process.
        /// </summary>
        /// <param name="homeMatchTeamId"></param>
        /// <returns></returns>
        private async Task<HomeMatchTeamCaptain> GetHomeMatchTeamCaptainAsync(int homeMatchTeamId, HomeMatchTeam homeMatchTeam )
        {
            if (homeMatchTeamId != 0)
            {
                var homeMatchTeamCaptain = await _context.HomeMatchTeamCaptains
                    .Include(hmtc => hmtc.ClubPlayer)
                    .Include(hmtc => hmtc.HomeMatchTeam)
                    .Where(hmtc => hmtc.HomeMatchTeamId == homeMatchTeamId)
                    .FirstOrDefaultAsync();

                if (homeMatchTeamCaptain == null)
                {
                    homeMatchTeamCaptain = new HomeMatchTeamCaptain
                    {
                        HomeMatchTeam = homeMatchTeam
                    };
                    _context.HomeMatchTeamCaptains.Add(homeMatchTeamCaptain);
                }
                await _context.SaveChangesAsync();

                return homeMatchTeamCaptain;
            }
            return null;
        }

        /// <summary>
        /// Asyncrhonously gets the away match team captain 
        /// If a record does not exist it creates it setting the away match team in the process.
        /// </summary>
        /// <param name="awayMatchTeamId"></param>
        /// <returns></returns>
        private async Task<AwayMatchTeamCaptain> GetAwayMatchTeamCaptainAsync(int awayMatchTeamId, AwayMatchTeam awayMatchTeam)
        {
            if (awayMatchTeamId != 0)
            {
                var awayMatchTeamCaptain = await _context.AwayMatchTeamCaptains
                    .Include(amtc => amtc.ClubPlayer)
                    .Include(amtc => amtc.AwayMatchTeam)
                    .Where(amtc => amtc.AwayMatchTeamId == awayMatchTeamId)
                    .FirstOrDefaultAsync();

                if (awayMatchTeamCaptain == null)
                {
                    awayMatchTeamCaptain = new AwayMatchTeamCaptain
                    {
                        AwayMatchTeam = awayMatchTeam
                    };
                    _context.AwayMatchTeamCaptains.Add(awayMatchTeamCaptain);
                }
                await _context.SaveChangesAsync();

                return awayMatchTeamCaptain;
            }
            return null;
        }

        public async Task<Team> GetTeamByIdAsync(ApplicationDbContext context, int teamId)
        {
            if (_context == null) _context = context;

            var team = await _context.Teams
                .Include(t => t.Category)
                .Include(t => t.Season)
                .Include(t => t.Division)
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club).ThenInclude(c => c.ClubPlayers)
                .Include(t => t.TeamCaptain)                
                .Where(t => t.Id == teamId)
                .SingleOrDefaultAsync();

            return team;
        }

        public async Task<List<ClubPlayer>> GetClubPlayersByClubAndCategoryAsync(ApplicationDbContext context, int clubId, int categoryId)
        {
            if (_context == null) _context = context;

            //return all clubplayers if category is mixed
            if (categoryId == Constants.Category.Mixed)
            {
                return await _context.ClubPlayers
                    .Include(cp => cp.PlayerDetail)
                    .Where(cp => cp.Club.Id == clubId && cp.IsActive)
                    .OrderBy(cp => cp.PlayerDetail.FirstName)
                    .ToListAsync();
            }

            // return just men / women otherwise.
            var genderFilterId = categoryId == Constants.Category.Mens ? Constants.Gender.Male : Constants.Gender.Female;
            return await _context.ClubPlayers
                .Include(cp => cp.PlayerDetail)
                .Where(cp => cp.Club.Id == clubId && cp.IsActive && cp.PlayerDetail.Gender.Id == genderFilterId)
                .OrderBy(cp => cp.PlayerDetail.FirstName)
                .ToListAsync();
        }

        public async Task<List<TeamPlayer>> GetTeamPlayersByTeamIdAsync(ApplicationDbContext context, int teamId)
        {
            if (_context == null) _context = context;

            var teamPlayers = await _context.TeamPlayers
                .Include(tp => tp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                .Include(tp => tp.ClubPlayer).ThenInclude(cp => cp.Club)
                .Include(tp => tp.Team)
                .Include(tp => tp.Rank)
                .Include(tp => tp.Group)
                .Where(tp => tp.Team.Id == teamId)
                .ToListAsync();

            return teamPlayers;
        }

        public async Task UpdateNominationsWithManageNominationsViewModelAsync(ApplicationDbContext context, ManageNominationsViewModel viewModel)
        {
            if (_context == null) _context = context;

            for(var i=0; i< viewModel.NominatedPlayerIds.Count(); i++)
            {
                TeamPlayer teamPlayer;
                if(viewModel.NominatedPlayerTeamIds[i] == 0)
                {
                    //this is a new entry so create the new team player
                    teamPlayer = new TeamPlayer
                    {
                        Team = await _context.Teams.FindAsync(viewModel.TeamId),
                        Group = await _context.Groups.FindAsync(viewModel.NominatedPlayerGroupIds[i]),
                        Rank = await _context.Ranks.FindAsync(viewModel.NominatedPlayerRankIds[i])
                    };
                    
                    // and the player details if one has been selected.
                    if (viewModel.NominatedPlayerIds[i] != 0) teamPlayer.ClubPlayer = await _context.ClubPlayers.FindAsync(viewModel.NominatedPlayerIds[i]);

                    _context.TeamPlayers.Add(teamPlayer);
                    await _context.SaveChangesAsync();                                     
                }
                else
                {
                    //otherwise get the team player and update the player.
                    teamPlayer = await _context.TeamPlayers
                        .Include(tp => tp.ClubPlayer)
                        .SingleAsync(tp => tp.Id == viewModel.NominatedPlayerTeamIds[i]);
                    
                    teamPlayer.ClubPlayer = viewModel.NominatedPlayerIds[i] != 0 ? await _context.ClubPlayers.FindAsync(viewModel.NominatedPlayerIds[i]) : null;
                    _context.TeamPlayers.Update(teamPlayer);
                    await _context.SaveChangesAsync();
                }
            }
        }
        
        public async Task<List<Team>> GetAllTeamsAsync(ApplicationDbContext context)
        {
            if (_context == null) _context = context;

            var allTeams = await _context.Teams
                .Include(t => t.Category)
                .Include(t => t.Division)
                .Include(t => t.Season)
                .Include(t => t.TeamCaptain)
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .ToListAsync();

            return allTeams;
        }

        public async Task<TeamDashboardViewModel> GetTeamDashboardViewModelAsync(ApplicationDbContext context, MatchService matchService, int teamId)
        {
            if (_context == null) _context = context;

            var team = await GetTeamByIdAsync(_context, teamId);
            var allLeagueMatches = await matchService.GetAllLeagueMatchesByTeamAsycn(_context, teamId);
            var availableTeams = (await GetAllTeamsAsync(_context)).Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.FullName }).ToList();

            //set unmapped property - isHomeTeam
            foreach (Match m in allLeagueMatches) m.isHomeTeam = m.HomeMatchTeam.Team.Id == team.Id ? true : false;

            var upcomingMatches = allLeagueMatches.Where(m => m.StartDate >= DateTime.Now).ToList();

            var recentMatches = allLeagueMatches.Where(m => m.StartDate < DateTime.Now).ToList();

            var leaguePosition = 0;
            var leaguePoints = 0;
            var totalMatchesWon = allLeagueMatches
                .Where(m => (m.isHomeTeam && m.HomeMatchTeam.ResultType.Id == Constants.ResultType.Win) ||
                    ((!m.isHomeTeam) && m.AwayMatchTeam.ResultType.Id == Constants.ResultType.Win))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesLost = allLeagueMatches
                .Where(m => (m.isHomeTeam && m.HomeMatchTeam.ResultType.Id == Constants.ResultType.Loss) ||
                    ((!m.isHomeTeam) && m.AwayMatchTeam.ResultType.Id == Constants.ResultType.Loss))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesDrawn = allLeagueMatches
                .Where(m => (m.isHomeTeam && m.HomeMatchTeam.ResultType.Id == Constants.ResultType.Draw) ||
                    ((!m.isHomeTeam) && m.AwayMatchTeam.ResultType.Id == Constants.ResultType.Draw))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesConceded = allLeagueMatches
                .Where(m => (m.isHomeTeam && m.HomeMatchTeam.ResultType.Id == Constants.ResultType.Conceded) ||
                    ((!m.isHomeTeam) && m.AwayMatchTeam.ResultType.Id == Constants.ResultType.Conceded))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesForfeited = allLeagueMatches
                .Where(m => (m.isHomeTeam && m.HomeMatchTeam.ResultType.Id == Constants.ResultType.Forfeit) ||
                    ((!m.isHomeTeam) && m.AwayMatchTeam.ResultType.Id == Constants.ResultType.Forfeit))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesPlayed = allLeagueMatches
                .Where(m => (m.isHomeTeam && m.HomeMatchTeam.ResultType.Id != Constants.ResultType.NoEntry) ||
                    ((!m.isHomeTeam) && m.AwayMatchTeam.ResultType.Id != Constants.ResultType.NoEntry))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesRemaining = allLeagueMatches
                .Where(m => (m.isHomeTeam && m.HomeMatchTeam.ResultType.Id == Constants.ResultType.NoEntry) ||
                    ((!m.isHomeTeam) && m.AwayMatchTeam.ResultType.Id == Constants.ResultType.NoEntry))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatches = allLeagueMatches.Count.ToString();


            var overview = new List<NameValuePair>
            {
                new NameValuePair {Name=Constants.OverviewStrings.LeaguePostion, Value = leaguePosition.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.LeaguePoints, Value = leaguePoints.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalWon, Value = totalMatchesWon.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalLost, Value = totalMatchesLost.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalDrawn, Value = totalMatchesDrawn.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalConceded, Value = totalMatchesConceded.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalForfeited, Value = totalMatchesForfeited.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalPlayed, Value = totalMatchesPlayed.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalRemaining, Value = totalMatchesRemaining.ToString()},
                new NameValuePair {Name=Constants.OverviewStrings.TotalMatches, Value = totalMatches.ToString()},
            };

            var viewModel = new TeamDashboardViewModel
            {
                Team = team,
                SelectedTeamId = teamId,
                AvailableTeams = availableTeams,
                UpcomingMatches = upcomingMatches,
                RecentMatches = recentMatches,
                Overview = overview
            };

            return viewModel;
        }

        public async Task<ManageNominationsViewModel> GetManageNominationsViewModelAsync(ApplicationDbContext context, MatchService matchService, int teamId)
        {
            if (_context == null) _context = context;

            var team = await GetTeamByIdAsync(_context, teamId);
            var nominatedPlayers = await GetTeamPlayersByTeamIdAsync(_context, teamId);
            var matchTemplate = await matchService.GetMatchTemplateBySeasonAndCategoryAsync(_context, team.Season.Id, team.Category.Id);

            if (nominatedPlayers.Count == 0)
            {
                // if new then use templates to generate the team players.
                foreach (var gt in matchTemplate.GroupTemplates)
                {
                    foreach (var rt in gt.RankTemplates)
                    {
                        var nominnatedPLayer = new TeamPlayer
                        {
                            Team = team,
                            Rank = rt.Rank,
                            Group = gt.Group
                        };
                        nominatedPlayers.Add(nominnatedPLayer);
                    }
                }
            }

            int[] nominatedPlayerIds = new int[nominatedPlayers.Count];
            int[] nominatedPlayerGroupIds = new int[nominatedPlayers.Count];
            int[] nominatedPlayerRankIds = new int[nominatedPlayers.Count];
            int[] nominatedPlayerTeamIds = new int[nominatedPlayers.Count];

            int index = 0;
            foreach(var nominatedPlayer in nominatedPlayers)
            {
                nominatedPlayerIds[index] = nominatedPlayer.ClubPlayer != null ? nominatedPlayer.ClubPlayer.Id : 0;
                nominatedPlayerGroupIds[index] = nominatedPlayer.Group.Id;
                nominatedPlayerRankIds[index] = nominatedPlayer.Rank.Id;
                nominatedPlayerTeamIds[index] = nominatedPlayer.Id;                        
                index++;
            }

            var clubPlayers = await GetClubPlayersByClubAndCategoryAsync(_context, team.LeagueClub.Club.Id, team.Category.Id);

            var availablePlayers = clubPlayers
                .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = cp.PlayerDetail.FullName })
                .ToList();

            return new ManageNominationsViewModel
            {
                Team = team,
                TeamId = team.Id,
                MatchTemplate = matchTemplate,
                AvailablePlayers = availablePlayers,
                NominatedPlayerIds = nominatedPlayerIds,
                NominatedPlayerGroupIds = nominatedPlayerGroupIds,
                NominatedPlayerRankIds = nominatedPlayerRankIds,
                NominatedPlayerTeamIds = nominatedPlayerTeamIds
            };
        }
        
    }
}
