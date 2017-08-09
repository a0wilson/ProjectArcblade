using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;
using ProjectArcBlade.Models.MatchViewModels;
using ProjectArcBlade.Models.TeamViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProjectArcBlade.Data.Constants;

namespace ProjectArcBlade.Services
{
    public class TeamService
    {
        private ApplicationDbContext _context;
        private MatchService _matchService;

        public async Task<Models.TeamStatus> GetTeamStatusAsync(ApplicationDbContext context, string value)
        {
            if (_context == null) _context = context;
            return await _context.TeamStatuses.SingleAsync(lookup => lookup.Name == value);
        }

        private async Task UpdateHomeMatchTeamCaptainAsync(int homeMatchTeamId, int captainId)
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

        private async Task<Team> GetTeamByIdAsync(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Category)
                .Include(t => t.Season)
                .Include(t => t.Division)
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club).ThenInclude(c => c.ClubPlayers)
                .Include(t => t.TeamCaptain)
                .Include(t => t.TeamStatus)
                .Where(t => t.Id == teamId)
                .SingleOrDefaultAsync();

            return team;
        }

        private async Task<List<ClubPlayer>> GetClubPlayersAvailableForNomination(int clubId, int categoryId, int seasonId, int teamId)
        {
            List<ClubPlayer> clubPlayers = new List<ClubPlayer>();

            //return all clubplayers if category is mixed
            if (categoryId == Constants.Category.Mixed)
            {
                clubPlayers = await _context.ClubPlayers
                    .Include(cp => cp.PlayerDetail)
                    .Where(cp => cp.Club.Id == clubId && cp.IsActive)
                    .OrderBy(cp => cp.PlayerDetail.FirstName)
                    .ToListAsync();
            }

            // return just men / women otherwise.
            var genderFilterId = categoryId == Constants.Category.Mens ? Constants.Gender.Male : Constants.Gender.Female;
            clubPlayers = await _context.ClubPlayers
                .Include(cp => cp.PlayerDetail)
                .Where(cp => cp.Club.Id == clubId && cp.IsActive && cp.PlayerDetail.Gender.Id == genderFilterId)
                .OrderBy(cp => cp.PlayerDetail.FirstName)
                .ToListAsync();

            foreach (var clubPlayer in clubPlayers)
            {
                await _context.Entry(clubPlayer)
                    .Collection(cp => cp.TeamPlayers)
                    .Query()
                    .Include(tp => tp.Team).ThenInclude(t => t.Season)
                    .Include(tp => tp.Team).ThenInclude(t => t.Category)
                    .ToListAsync();
            }

            //further filter the list of club players to players who are not members of teams in this category for the current season.
            clubPlayers = clubPlayers.Where(cp => cp.TeamPlayers.Count(tp => tp.Team.Season.Id == seasonId && tp.Team.Category.Id == categoryId && tp.Team.Id != teamId) == 0).ToList();

            return clubPlayers;
        }

        private async Task<List<ClubPlayer>> GetClubPlayersByClubAndCategoryAsync(int clubId, int categoryId)
        {
            List<ClubPlayer> clubPlayers = new List<ClubPlayer>();

            //return all clubplayers if category is mixed
            if (categoryId == Constants.Category.Mixed)
            {
                clubPlayers = await _context.ClubPlayers
                    .Include(cp => cp.PlayerDetail)                    
                    .Where(cp => cp.Club.Id == clubId && cp.IsActive)
                    .OrderBy(cp => cp.PlayerDetail.FirstName)
                    .ToListAsync();
            }

            // return just men / women otherwise.
            var genderFilterId = categoryId == Constants.Category.Mens ? Constants.Gender.Male : Constants.Gender.Female;
            clubPlayers = await _context.ClubPlayers
                .Include(cp => cp.PlayerDetail)
                .Where(cp => cp.Club.Id == clubId && cp.IsActive && cp.PlayerDetail.Gender.Id == genderFilterId)
                .OrderBy(cp => cp.PlayerDetail.FirstName)
                .ToListAsync();
            
            return clubPlayers;
        }

        private async Task<List<TeamPlayer>> GetTeamPlayersByTeamAsync(int teamId)
        {
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

        private async Task<TeamCaptain> GetTeamCaptainByTeamAsync(int teamId)
        {
            var teamCaptain = await _context.TeamCaptains
                .Include(tc => tc.ClubPlayer)
                .Include(tc => tc.Team)
                .Where(tc => tc.Team.Id == teamId)
                .SingleOrDefaultAsync();

            return teamCaptain;
        }
        
        private async Task<List<Team>> GetAllTeamsAsync()
        {
            var allTeams = await _context.Teams
                .Include(t => t.Category)
                .Include(t => t.Division)
                .Include(t => t.Season)
                .Include(t => t.TeamCaptain)
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .ToListAsync();

            return allTeams;
        }
          

        private async Task UpdateAwayMatchTeamWithManageMatchTeamViewTeamModelAsync(ManageMatchTeamViewModel viewModel)
        {
            ClubPlayer clubPlayer;

            for (var i = 0; i < viewModel.MatchClubPlayerIds.Count(); i++)
            {
                var teamPlayer = await _context.AwayMatchTeamGroupPlayers
                    .Include(amtgp => amtgp.ClubPlayer)
                    .Where(amtgp => amtgp.Id == viewModel.MatchPlayerIds[i])
                    .SingleOrDefaultAsync();

                clubPlayer = viewModel.MatchClubPlayerIds[i] == 0 ? null : await _context.ClubPlayers.FindAsync(viewModel.MatchClubPlayerIds[i]);
                teamPlayer.ClubPlayer = clubPlayer;
                _context.AwayMatchTeamGroupPlayers.Update(teamPlayer);
                await _context.SaveChangesAsync();
            }

            //set the team status
            var awayMatchTeam = await _context.AwayMatchTeams
                .Include(hmt => hmt.TeamStatus)
                .Where(hmt => hmt.Id == viewModel.AwayMatchTeamId)
                .SingleAsync();

            string teamStatusName;
            if (viewModel.MatchClubPlayerIds.Count() == viewModel.MatchClubPlayerIds.Count(value => value == 0))
            {
                teamStatusName = Constants.TeamStatus.New;
            }
            else
            {
                teamStatusName = viewModel.MatchClubPlayerIds.Count() == viewModel.MatchClubPlayerIds.Count(value => value != 0) ? Constants.TeamStatus.Complete : Constants.TeamStatus.InProgress;
            }
            var teamStatus = await GetTeamStatusAsync(_context, teamStatusName);
            awayMatchTeam.TeamStatus = teamStatus;
            _context.AwayMatchTeams.Update(awayMatchTeam);

            //if the current captain has been removed from the list of nominated players then clear the captain id!
            if (!viewModel.MatchClubPlayerIds.Contains(viewModel.CaptainId)) viewModel.CaptainId = 0;

            //update/create the team captain.
            var teamCaptain = await _context.AwayMatchTeamCaptains
                .Include(tc => tc.AwayMatchTeam).ThenInclude(hmt => hmt.Team)
                .Include(tc => tc.ClubPlayer)
                .Where(tc => tc.AwayMatchTeam.Id == viewModel.AwayMatchTeamId)
                .SingleOrDefaultAsync();

            clubPlayer = viewModel.CaptainId == 0 ? null : await _context.ClubPlayers.FindAsync(viewModel.CaptainId);

            if (teamCaptain == null)
            {
                teamCaptain = new AwayMatchTeamCaptain
                {
                    AwayMatchTeam = await _context.AwayMatchTeams.FindAsync(viewModel.AwayMatchTeamId),
                    ClubPlayer = clubPlayer
                };
                _context.AwayMatchTeamCaptains.Add(teamCaptain);
            }
            else
            {
                teamCaptain.ClubPlayer = clubPlayer;
                _context.AwayMatchTeamCaptains.Update(teamCaptain);
            }

            //save all changes!
            await _context.SaveChangesAsync();
        }

        private async Task UpdateHomeMatchTeamWithManageMatchTeamViewTeamModelAsync(ManageMatchTeamViewModel viewModel)
        {
            ClubPlayer clubPlayer;

            for (var i = 0; i < viewModel.MatchClubPlayerIds.Count(); i++)
            {
                var teamPlayer = await _context.HomeMatchTeamGroupPlayers
                    .Include(hmtgp => hmtgp.ClubPlayer)
                    .Where(hmtgp => hmtgp.Id == viewModel.MatchPlayerIds[i])
                    .SingleOrDefaultAsync();

                clubPlayer = viewModel.MatchClubPlayerIds[i] == 0 ? null : await _context.ClubPlayers.FindAsync(viewModel.MatchClubPlayerIds[i]);
                teamPlayer.ClubPlayer = clubPlayer;
                _context.HomeMatchTeamGroupPlayers.Update(teamPlayer);
                await _context.SaveChangesAsync();
            }

            //set the team status
            var homeMatchTeam = await _context.HomeMatchTeams
                .Include(hmt => hmt.TeamStatus)
                .Where(hmt => hmt.Id == viewModel.HomeMatchTeamId)
                .SingleAsync();

            string teamStatusName;
            if (viewModel.MatchClubPlayerIds.Count() == viewModel.MatchClubPlayerIds.Count(value => value == 0))
            {
                teamStatusName = Constants.TeamStatus.New;
            }else
            {
                teamStatusName = viewModel.MatchClubPlayerIds.Count() == viewModel.MatchClubPlayerIds.Count(value => value != 0) ? Constants.TeamStatus.Complete : Constants.TeamStatus.InProgress;
            }            
            var teamStatus = await GetTeamStatusAsync(_context, teamStatusName);
            homeMatchTeam.TeamStatus = teamStatus;
            _context.HomeMatchTeams.Update(homeMatchTeam);
            
            //if the current captain has been removed from the list of nominated players then clear the captain id!
            if (!viewModel.MatchClubPlayerIds.Contains(viewModel.CaptainId)) viewModel.CaptainId = 0;

            //update/create the team captain.
            var teamCaptain = await _context.HomeMatchTeamCaptains
                .Include(tc => tc.HomeMatchTeam).ThenInclude(hmt => hmt.Team)
                .Include(tc => tc.ClubPlayer)
                .Where(tc => tc.HomeMatchTeam.Id == viewModel.HomeMatchTeamId)
                .SingleOrDefaultAsync();

            clubPlayer = viewModel.CaptainId == 0 ? null : await _context.ClubPlayers.FindAsync(viewModel.CaptainId);

            if (teamCaptain == null)
            {
                teamCaptain = new HomeMatchTeamCaptain
                {
                    HomeMatchTeam = await _context.HomeMatchTeams.FindAsync(viewModel.HomeMatchTeamId),
                    ClubPlayer = clubPlayer
                };
                _context.HomeMatchTeamCaptains.Add(teamCaptain);
            }
            else
            {
                teamCaptain.ClubPlayer = clubPlayer;
                _context.HomeMatchTeamCaptains.Update(teamCaptain);
            }

            //save all changes!
            await _context.SaveChangesAsync();
        }

        private async Task<ManageMatchTeamViewModel> GetManageMatchTeamViewModelForHomeTeamAsync(int homeMatchTeamId)
        {
            var homeMatchTeam = await GetHomeMatchTeamByIdAsync(_context, homeMatchTeamId);

            var matchPlayers = new List<HomeMatchTeamGroupPlayer>();
            homeMatchTeam.HomeMatchTeamGroups.ToList()
                .ForEach(hmtg => hmtg.HomeMatchTeamGroupPlayers.ToList()
                    .ForEach(hmtgp => { if(!matchPlayers.Select(mp => mp.Rank.Id).ToList().Contains(hmtgp.Rank.Id)) matchPlayers.Add(hmtgp); })
                );
            var teamCaptain = homeMatchTeam.HomeMatchTeamCaptain;
            var matchTemplate = await _matchService.GetMatchTemplateBySeasonAndCategoryAsync(_context, homeMatchTeam.Team.Season.Id, homeMatchTeam.Team.Category.Id);

            //if no club players have been selected at for the match so far then default the match selection to the nominated team!
            var noSelection = matchPlayers.Count() == matchPlayers.Count(mp => mp.ClubPlayer == null);
            var nominatedTeamComplete = matchPlayers.Count() == homeMatchTeam.Team.TeamPlayers.Count(tp => tp.ClubPlayer != null);
            if(noSelection && nominatedTeamComplete)
            {
                for(var i=0; i<matchPlayers.Count(); i++)
                {
                    matchPlayers[i].ClubPlayer = homeMatchTeam.Team.TeamPlayers.Where(tp => tp.Rank.Id == matchPlayers[i].Rank.Id).Select(tp => tp.ClubPlayer).Single();
                }

                //also set the captain if there is one!
                if (homeMatchTeam.Team.TeamCaptain != null)
                {
                    teamCaptain = new HomeMatchTeamCaptain
                    {
                        ClubPlayer = homeMatchTeam.Team.TeamCaptain.ClubPlayer,
                        HomeMatchTeam = homeMatchTeam
                    };
                }
            }

            

            int[] matchClubPlayerIds = new int[matchPlayers.Count];
            int[] matchPlayerGroupIds = new int[matchPlayers.Count];
            int[] matchPlayerRankIds = new int[matchPlayers.Count];
            int[] matchPlayerIds = new int[matchPlayers.Count];

            int index = 0;
            foreach (var matchPlayer in matchPlayers)
            {
                // if a team player has been nominated then use that player instead of no entry.
                //var teamClubPlayerId = homeMatchTeam.Team.TeamPlayers.Where(tp => tp.Rank.Id == matchPlayer.Rank.Id).Select(tp => tp.ClubPlayer.Id).SingleOrDefault();
                //matchClubPlayerIds[index] = matchPlayer.ClubPlayer != null ? matchPlayer.ClubPlayer.Id : teamClubPlayer != null ? teamClubPlayer.Id : 0;

                matchClubPlayerIds[index] = matchPlayer.ClubPlayer != null ? matchPlayer.ClubPlayer.Id : 0;

                matchPlayerGroupIds[index] = matchPlayer.HomeMatchTeamGroup.Group.Id;
                matchPlayerRankIds[index] = matchPlayer.Rank.Id;
                matchPlayerIds[index] = matchPlayer.Id;
                index++;
            }

            var clubPlayers = await GetClubPlayersByClubAndCategoryAsync(homeMatchTeam.Team.LeagueClub.Club.Id, homeMatchTeam.Team.Category.Id);

            var availablePlayers = clubPlayers
                .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = cp.PlayerDetail.FullName })
                .ToList();

            var teamPlayers = matchPlayers
                .Where(mp => mp.ClubPlayer != null)
                .GroupBy(mp => mp.ClubPlayer.Id)
                .Select(mp => new SelectListItem { Value = mp.First().ClubPlayer.Id.ToString(), Text = mp.First().ClubPlayer.PlayerDetail.FullName })
                .ToList();

            var captainId = teamCaptain == null ? 0 : teamCaptain.ClubPlayer == null ? 0 : teamCaptain.ClubPlayer.Id;

            //Work out if there are any warnings.
            var warnings = new List<NameValuePair>();

            //check if anyone has been assigned to the team multiple times
            var playerSelectedMultipleTimes = matchPlayers
                .Where(np => np.ClubPlayer != null)
                .GroupBy(np => np.ClubPlayer.Id)
                .Where(np => np.Count() > 1)
                .Select(np => new NameValuePair
                {
                    Name = String.Format(Constants.TeamStrings.AssignedMultipleTimes, np.First().ClubPlayer.PlayerDetail.FullName),
                    Value = np.Count().ToString()
                })
                .ToList();
            playerSelectedMultipleTimes.ForEach(nvp => warnings.Add(nvp));

            //check if users who are in a higher divisions who should not be playing this match.
            //check for you users who have played up too many times.

            return new ManageMatchTeamViewModel
            {
                MatchId = homeMatchTeam.Match.Id,
                HomeTeamId = homeMatchTeam.Team.Id, 
                HomeMatchTeamId = homeMatchTeam.Id,
                TeamName = homeMatchTeam.Team.FullName,
                TeamStatus = homeMatchTeam.TeamStatus,
                Opponents = homeMatchTeam.Match.AwayMatchTeam.Team,
                TeamId = homeMatchTeam.Team.Id,
                MatchTemplate = matchTemplate,
                AvailablePlayers = availablePlayers,
                MatchClubPlayerIds = matchClubPlayerIds,
                MatchPlayerGroupIds = matchPlayerGroupIds,
                MatchPlayerRankIds = matchPlayerRankIds,
                MatchPlayerIds = matchPlayerIds,
                MatchPlayers = teamPlayers,
                CaptainId = captainId,
                Warnings = warnings
            };
        }

        private async Task<ManageMatchTeamViewModel> GetManageMatchTeamViewModelForAwayTeamAsync(int awayMatchTeamId)
        {
            var awayMatchTeam = await GetAwayMatchTeamByIdAsync(_context, awayMatchTeamId);

            var matchPlayers = new List<AwayMatchTeamGroupPlayer>();
            awayMatchTeam.AwayMatchTeamGroups.ToList()
                .ForEach(amtg => amtg.AwayMatchTeamGroupPlayers.ToList()
                    .ForEach(amtgp => { if (!matchPlayers.Select(mp => mp.Rank.Id).ToList().Contains(amtgp.Rank.Id)) matchPlayers.Add(amtgp); })
                );
            var teamCaptain = awayMatchTeam.AwayMatchTeamCaptain;
            var matchTemplate = await _matchService.GetMatchTemplateBySeasonAndCategoryAsync(_context, awayMatchTeam.Team.Season.Id, awayMatchTeam.Team.Category.Id);

            int[] matchClubPlayerIds = new int[matchPlayers.Count];
            int[] matchPlayerGroupIds = new int[matchPlayers.Count];
            int[] matchPlayerRankIds = new int[matchPlayers.Count];
            int[] matchPlayerIds = new int[matchPlayers.Count];

            int index = 0;
            foreach (var matchPlayer in matchPlayers)
            {
                matchClubPlayerIds[index] = matchPlayer.ClubPlayer != null ? matchPlayer.ClubPlayer.Id : 0;
                matchPlayerGroupIds[index] = matchPlayer.AwayMatchTeamGroup.Group.Id;
                matchPlayerRankIds[index] = matchPlayer.Rank.Id;
                matchPlayerIds[index] = matchPlayer.Id;
                index++;
            }

            var clubPlayers = await GetClubPlayersByClubAndCategoryAsync(awayMatchTeam.Team.LeagueClub.Club.Id, awayMatchTeam.Team.Category.Id);

            var availablePlayers = clubPlayers
                .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = cp.PlayerDetail.FullName })
                .ToList();

            var teamPlayers = matchPlayers
                .Where(np => np.ClubPlayer != null)
                .GroupBy(np => np.ClubPlayer.Id)
                .Select(np => new SelectListItem { Value = np.First().ClubPlayer.Id.ToString(), Text = np.First().ClubPlayer.PlayerDetail.FullName })
                .ToList();

            var captainId = teamCaptain == null ? 0 : teamCaptain.ClubPlayer == null ? 0 : teamCaptain.ClubPlayer.Id;

            //Work out if there are any warnings.
            var warnings = new List<NameValuePair>();

            //check if anyone has been assigned to the team multiple times
            var playerSelectedMultipleTimes = matchPlayers
                .Where(np => np.ClubPlayer != null)
                .GroupBy(np => np.ClubPlayer.Id)
                .Where(np => np.Count() > 1)
                .Select(np => new NameValuePair
                {
                    Name = String.Format(Constants.TeamStrings.AssignedMultipleTimes, np.First().ClubPlayer.PlayerDetail.FullName),
                    Value = np.Count().ToString()
                })
                .ToList();
            playerSelectedMultipleTimes.ForEach(nvp => warnings.Add(nvp));

            //check if users who are in a higher divisions who should not be playing this match.
            //check for you users who have played up too many times.

            return new ManageMatchTeamViewModel
            {
                MatchId = awayMatchTeam.Match.Id,
                AwayMatchTeamId = awayMatchTeam.Id,
                AwayTeamId = awayMatchTeam.Team.Id,
                TeamName = awayMatchTeam.Team.FullName,
                TeamStatus = awayMatchTeam.TeamStatus,
                Opponents = awayMatchTeam.Match.HomeMatchTeam.Team,
                TeamId = awayMatchTeam.Team.Id,
                MatchTemplate = matchTemplate,
                AvailablePlayers = availablePlayers,
                MatchClubPlayerIds = matchClubPlayerIds,
                MatchPlayerGroupIds = matchPlayerGroupIds,
                MatchPlayerRankIds = matchPlayerRankIds,
                MatchPlayerIds = matchPlayerIds,
                MatchPlayers = teamPlayers,
                CaptainId = captainId,
                Warnings = warnings
            };
        }

        public async Task UpdateNominationsWithManageNominationsViewModelAsync(ApplicationDbContext context, ManageNominationsViewModel viewModel)
        {
            if (_context == null) _context = context;

            ClubPlayer clubPlayer;

            for(var i=0; i< viewModel.NominatedClubPlayerIds.Count(); i++)
            {

                var teamPlayer = await _context.TeamPlayers.FindAsync(viewModel.NomintatedPlayerIds[i]);
                //var teamPlayer = await _context.TeamPlayers
                //.Include(tp => tp.Team)
                //.Include(tp => tp.Group)
                //.Include(tp => tp.Rank)
                //.Include(tp => tp.ClubPlayer)
                //.Where(tp => tp.Team.Id == viewModel.TeamId &&
                //    tp.Group.Id == viewModel.NominatedPlayerGroupIds[i] &&
                //    tp.Rank.Id == viewModel.NominatedPlayerRankIds[i])
                //.SingleAsync();

                clubPlayer = viewModel.NominatedClubPlayerIds[i] == 0 ? null : await _context.ClubPlayers.FindAsync(viewModel.NominatedClubPlayerIds[i]);

                if (teamPlayer == null)
                {
                    teamPlayer = new TeamPlayer
                    {
                        Team = await _context.Teams.FindAsync(viewModel.TeamId),
                        Group = await _context.Groups.FindAsync(viewModel.NominatedPlayerGroupIds[i]),
                        Rank = await _context.Ranks.FindAsync(viewModel.NominatedPlayerRankIds[i]),
                        ClubPlayer = clubPlayer
                    };
                    _context.TeamPlayers.Add(teamPlayer);
                }
                else
                {
                    teamPlayer.ClubPlayer = clubPlayer;
                    _context.TeamPlayers.Update(teamPlayer);
                }

                await _context.SaveChangesAsync();
                
                
            }

            //if the current captain has been removed from the list of nominated players then clear the captain id!
            if (!viewModel.NominatedClubPlayerIds.Contains(viewModel.CaptainId)) viewModel.CaptainId = 0;

            //update/create the team captain.
            var teamCaptain = await _context.TeamCaptains
                .Include(tc => tc.Team)
                .Include(tc => tc.ClubPlayer)
                .Where(tc => tc.Team.Id == viewModel.TeamId)
                .SingleOrDefaultAsync();

            clubPlayer = viewModel.CaptainId == 0 ? null : await _context.ClubPlayers.FindAsync(viewModel.CaptainId);

            if (teamCaptain == null)
            {
                teamCaptain = new TeamCaptain
                {
                    Team = await _context.Teams.FindAsync(viewModel.TeamId),
                    ClubPlayer = clubPlayer
                };
                _context.TeamCaptains.Add(teamCaptain);
            }
            else
            {
                teamCaptain.ClubPlayer = clubPlayer;
                _context.TeamCaptains.Update(teamCaptain);
            }
            
            //save all changes!
            await _context.SaveChangesAsync();
        }

        public async Task<ManageNominationsViewModel> GetManageNominationsViewModelAsync(ApplicationDbContext context, MatchService matchService, int teamId)
        {
            if (_context == null) _context = context;

            var team = await GetTeamByIdAsync(teamId);
            var nominatedPlayers = await GetTeamPlayersByTeamAsync(teamId);
            var teamCaptain = await GetTeamCaptainByTeamAsync(teamId);
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

                        //only add if this rank doesn't already exist.
                        if( !nominatedPlayers.Select(np => np.Rank.Id).ToList().Contains(rt.Rank.Id))
                        {
                            nominatedPlayers.Add(nominnatedPLayer);
                        }
                    }
                }
            }

            int[] nominatedPlayerIds = new int[nominatedPlayers.Count];
            int[] nominatedPlayerGroupIds = new int[nominatedPlayers.Count];
            int[] nominatedPlayerRankIds = new int[nominatedPlayers.Count];
            int[] nominatedPlayerTeamIds = new int[nominatedPlayers.Count];

            int index = 0;
            foreach (var nominatedPlayer in nominatedPlayers)
            {
                nominatedPlayerIds[index] = nominatedPlayer.ClubPlayer != null ? nominatedPlayer.ClubPlayer.Id : 0;
                nominatedPlayerGroupIds[index] = nominatedPlayer.Group.Id;
                nominatedPlayerRankIds[index] = nominatedPlayer.Rank.Id;
                nominatedPlayerTeamIds[index] = nominatedPlayer.Id;
                index++;
            }

            var clubPlayers = await GetClubPlayersAvailableForNomination(team.LeagueClub.Club.Id, team.Category.Id, team.Season.Id, teamId);

            var availablePlayers = clubPlayers
                .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = cp.PlayerDetail.FullName })
                .ToList();

            var teamPlayers = nominatedPlayers
                .Where(np => np.ClubPlayer != null)
                .GroupBy(np => np.ClubPlayer.Id)
                .Select(np => new SelectListItem { Value = np.First().ClubPlayer.Id.ToString(), Text = np.First().ClubPlayer.PlayerDetail.FullName })
                .ToList();

            var captainId = teamCaptain == null ? 0 : teamCaptain.ClubPlayer == null ? 0 : teamCaptain.ClubPlayer.Id;

            //Work out if there are any warnings.
            var warnings = new List<NameValuePair>();
            var errors = new List<NameValuePair>();

            //check if anyone has been assigned to the team multiple times
            var playerSelectedMultipleTimes = nominatedPlayers
                .Where( np => np.ClubPlayer != null)
                .GroupBy(np => np.ClubPlayer.Id)
                .Where(np => np.Count() > 1 )
                .Select(np => new NameValuePair
                {
                    Name = String.Format(Constants.TeamStrings.AssignedMultipleTimes, np.First().ClubPlayer.PlayerDetail.FullName),
                    Value = np.Count().ToString()
                })
                .ToList();
            foreach (var nvp in playerSelectedMultipleTimes) errors.Add(nvp);

            //check if users who are in a higher divisions who should not be playing this match.
            //check for you users who have played up too many times.

            return new ManageNominationsViewModel
            {
                Team = team,
                TeamId = team.Id,
                MatchTemplate = matchTemplate,
                AvailablePlayers = availablePlayers,
                NominatedClubPlayerIds = nominatedPlayerIds,
                NominatedPlayerGroupIds = nominatedPlayerGroupIds,
                NominatedPlayerRankIds = nominatedPlayerRankIds,
                NomintatedPlayerIds = nominatedPlayerTeamIds,
                TeamPlayers = teamPlayers,
                CaptainId = captainId,
                Warnings = warnings,
                Errors = errors
            };
        }
        
        public async Task<TeamDashboardViewModel> GetTeamDashboardViewModelAsync(ApplicationDbContext context, MatchService matchService, int teamId)
        {
            if (_context == null) _context = context;

            var team = await GetTeamByIdAsync(teamId);            
            
            var allLeagueMatches = await matchService.GetAllLeagueMatchesByTeamAsync(_context, teamId);
            var allLeagueMatchViewModels = allLeagueMatches.Select(m => matchService.GetMatchViewModel(m,  teamId, false, false));
                   
            var availableTeams = (await GetAllTeamsAsync()).Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.FullName }).ToList();
            
            var inProgressMatches = allLeagueMatchViewModels.Where(m => m.MatchStatus == Constants.MatchStatus.InProgress).ToList();

            var upcomingMatches = allLeagueMatchViewModels.Where(m => m.MatchStatus == Constants.MatchStatus.New).ToList();
                
            var recentMatches = allLeagueMatchViewModels.Where(m => m.MatchStatus == Constants.MatchStatus.Complete).ToList();

            var leaguePosition = 0;
            var leaguePoints = 0;
            var totalMatchesWon = allLeagueMatchViewModels
                .Where(m => (m.IsHomeTeam && m.HomeResult == Constants.ResultType.Win) ||
                    ((!m.IsHomeTeam) && m.AwayResult == Constants.ResultType.Win))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesLost = allLeagueMatchViewModels
                .Where(m => (m.IsHomeTeam && m.HomeResult == Constants.ResultType.Loss) ||
                    ((!m.IsHomeTeam) && m.AwayResult == Constants.ResultType.Loss))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesDrawn = allLeagueMatchViewModels
                .Where(m => (m.IsHomeTeam && m.HomeResult == Constants.ResultType.Draw) ||
                    ((!m.IsHomeTeam) && m.AwayResult == Constants.ResultType.Draw))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesConceded = allLeagueMatchViewModels
                .Where(m => (m.IsHomeTeam && m.HomeResult == Constants.ResultType.Conceded) ||
                    ((!m.IsHomeTeam) && m.AwayResult == Constants.ResultType.Conceded))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesPlayed = allLeagueMatchViewModels
                .Where(m => (m.IsHomeTeam && m.HomeResult != Constants.ResultType.NoEntry) ||
                    ((!m.IsHomeTeam) && m.AwayResult != Constants.ResultType.NoEntry))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatchesRemaining = allLeagueMatchViewModels
                .Where(m => (m.IsHomeTeam && m.HomeResult == Constants.ResultType.NoEntry) ||
                    ((!m.IsHomeTeam) && m.AwayResult == Constants.ResultType.NoEntry))
                .Select(m => new { count = 1 })
                .Sum(m => m.count);
            var totalMatches = allLeagueMatches.Count.ToString();


            var overview = new List<NameValuePair>
            {
                new NameValuePair {Name=OverviewStrings.LeaguePostion, Value = leaguePosition.ToString()},
                new NameValuePair {Name=OverviewStrings.LeaguePoints, Value = leaguePoints.ToString()},
                new NameValuePair {Name=OverviewStrings.TotalWon, Value = totalMatchesWon.ToString()},
                new NameValuePair {Name=OverviewStrings.TotalLost, Value = totalMatchesLost.ToString()},
                new NameValuePair {Name=OverviewStrings.TotalDrawn, Value = totalMatchesDrawn.ToString()},
                new NameValuePair {Name=OverviewStrings.TotalConceded, Value = totalMatchesConceded.ToString()},
                new NameValuePair {Name=OverviewStrings.TotalPlayed, Value = totalMatchesPlayed.ToString()},
                new NameValuePair {Name=OverviewStrings.TotalRemaining, Value = totalMatchesRemaining.ToString()},
                new NameValuePair {Name=OverviewStrings.TotalMatches, Value = totalMatches.ToString()},
            };

            var viewModel = new TeamDashboardViewModel
            {
                TeamId = teamId,
                TeamName = team.FullName,
                AvailableTeams = availableTeams,
                UpcomingMatches = upcomingMatches,
                CompletedMatches = recentMatches,
                InProgressMatches = inProgressMatches,
                Overview = overview
            };

            return viewModel;
        }

        public async Task<ManageMatchTeamViewModel> GetManageMatchTeamViewModelAsync(ApplicationDbContext context, MatchService matchService, TeamType teamType, int matchTeamId)
        {
            var viewModel = new ManageMatchTeamViewModel();

            if( _context == null) _context = context;
            if (_matchService == null) _matchService = matchService;

            switch(teamType)
            {
                case TeamType.Home:
                    return await GetManageMatchTeamViewModelForHomeTeamAsync(matchTeamId);
                case TeamType.Away:
                    return await GetManageMatchTeamViewModelForAwayTeamAsync(matchTeamId);
                default:
                    return null;
            }            
        }
                
        public async Task UpdateMatchTeamsWithManageMatchTeamViewModelAsync(ApplicationDbContext context, ManageMatchTeamViewModel viewModel)
        {
            if (_context == null) _context = context;
            if (viewModel.HomeMatchTeamId != 0) await UpdateHomeMatchTeamWithManageMatchTeamViewTeamModelAsync(viewModel);
            if (viewModel.AwayMatchTeamId != 0) await UpdateAwayMatchTeamWithManageMatchTeamViewTeamModelAsync(viewModel);
        }

        public async Task<HomeMatchTeam> GetHomeMatchTeamByIdAsync(ApplicationDbContext context, int id)
        {
            if (_context == null) _context = context;

            var homeMatchTeam = await _context.HomeMatchTeams
                        .Include(hmt => hmt.Match).ThenInclude(m => m.AwayMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.Season)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.Category)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.TeamCaptain)
                        .Include(hmt => hmt.TeamStatus)
                        .Include(hmt => hmt.HomeMatchTeamGroups)
                        .Include(hmt => hmt.HomeMatchTeamCaptain).ThenInclude(hmtc => hmtc.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                        .Where(hmt => hmt.Id == id)
                        .SingleOrDefaultAsync();

            if (homeMatchTeam != null)
            {                
                //get any nominated team players.
                await _context.Entry(homeMatchTeam.Team)
                    .Collection(t => t.TeamPlayers)
                    .Query()
                    .Include(tp => tp.ClubPlayer)
                    .Include(tp => tp.Rank)
                    .Include(tp => tp.Group)
                    .ToListAsync();

                //get all the related players.
                foreach (var homeMatchTeamGroup in homeMatchTeam.HomeMatchTeamGroups)
                {
                    await _context.Entry(homeMatchTeamGroup)
                        .Collection(hmtg => hmtg.HomeMatchTeamGroupPlayers)
                        .Query()
                        .Include(hmtgp => hmtgp.Rank)
                        .Include(hmtgp => hmtgp.HomeMatchTeamGroup).ThenInclude(hmtgp => hmtgp.Group)
                        .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                        .ToListAsync();
                }
            }
            return homeMatchTeam;
        }

        public async Task<AwayMatchTeam> GetAwayMatchTeamByIdAsync(ApplicationDbContext context, int id)
        {
            if (_context == null) _context = context;

            var awayMatchTeam = await _context.AwayMatchTeams
                        .Include(amt => amt.Match).ThenInclude(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                        .Include(amt => amt.Team).ThenInclude(t => t.Season)
                        .Include(amt => amt.Team).ThenInclude(t => t.Category)
                        .Include(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                        .Include(amt => amt.Team).ThenInclude(t => t.TeamCaptain)
                        .Include(amt => amt.TeamStatus)
                        .Include(amt => amt.AwayMatchTeamGroups)
                        .Include(amt => amt.AwayMatchTeamCaptain).ThenInclude(hmtc => hmtc.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                        .Where(amt => amt.Id == id)
                        .SingleOrDefaultAsync();

            if (awayMatchTeam != null)
            {
                //get any nominated team players.
                await _context.Entry(awayMatchTeam.Team)
                    .Collection(t => t.TeamPlayers)
                    .Query()
                    .Include(tp => tp.ClubPlayer)
                    .Include(tp => tp.Rank)
                    .Include(tp => tp.Group)
                    .ToListAsync();

                //get all the related players.
                foreach (var awayMatchTeamGroup in awayMatchTeam.AwayMatchTeamGroups)
                {
                    await _context.Entry(awayMatchTeamGroup)
                        .Collection(amtg => amtg.AwayMatchTeamGroupPlayers)
                        .Query()
                        .Include(amtgp => amtgp.Rank)
                        .Include(amtgp => amtgp.AwayMatchTeamGroup).ThenInclude(amtg => amtg.Group)
                        .Include(amtgp => amtgp.ClubPlayer).ThenInclude(cp => cp.PlayerDetail)
                        .ToListAsync();
                }
            }
            return awayMatchTeam;
        }

        public async Task<bool> ActivateHomeMatchTeam(ApplicationDbContext context, int homeMatchTeamId)
        {
            if (_context == null) _context = context;

            var homeMatchTeam = await _context.HomeMatchTeams
                .Include(hmt => hmt.TeamStatus)
                .Where(hmt => hmt.Id == homeMatchTeamId)
                .SingleAsync();

            if( homeMatchTeam.TeamStatus.Name == Constants.TeamStatus.Complete )
            {
                homeMatchTeam.TeamStatus = await GetTeamStatusAsync(context, Constants.TeamStatus.Active);
                _context.HomeMatchTeams.Update(homeMatchTeam);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> ActivateAwayMatchTeam(ApplicationDbContext context, int awayMatchTeamId)
        {
            if (_context == null) _context = context;

            var awayMatchTeam = await _context.AwayMatchTeams
                .Include(amt => amt.TeamStatus)
                .Where(amt => amt.Id == awayMatchTeamId)
                .SingleAsync();

            if (awayMatchTeam.TeamStatus.Name == Constants.TeamStatus.Complete)
            {
                awayMatchTeam.TeamStatus = await GetTeamStatusAsync(context, Constants.TeamStatus.Active);
                _context.AwayMatchTeams.Update(awayMatchTeam);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
