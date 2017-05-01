using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;
using ProjectArcBlade.Models.TeamViewModels;
using ProjectArcBlade.Services;

namespace ProjectArcBlade.Controllers
{
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Team/Dashboard/5
        public async Task<ActionResult> Dashboard(int? id)
        {
            if (id == null) return NotFound();

            Team team = await _context.Teams
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club).ThenInclude(c => c.ClubPlayers)
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.League)
                .Include(t => t.TeamPlayers)
                .Include(t => t.Category)
                .Include(t => t.Season)
                .Include(t => t.TeamStatus)
                .FirstOrDefaultAsync(m => m.Id == id);

            var allLeagueMatches = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.ResultType)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.ResultType)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(m => (m.AwayMatchTeam.Team.Id == team.Id || m.HomeMatchTeam.Team.Id == team.Id)
                    && m.MatchType.Id == Constants.MatchType.League)
                .ToListAsync();

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
            
            var viewModel = new DashboardTeamViewModel
            {
                Team = team,
                UpcomingMatches = upcomingMatches,
                RecentMatches = recentMatches,
                Overview = overview
            };

            return View(viewModel);
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.Include(t=>t.TeamStatus).Include(t=>t.LeagueClub).ThenInclude(lc=>lc.Club).ToListAsync());
        }
        
        //GET: Team/Organise/5
        public IActionResult Organise(int? awayMatchTeamId, int? homeMatchTeamId)
        {
           
            if(homeMatchTeamId != null) return RedirectToAction("OrganiseHomeMatchTeam", new { id = homeMatchTeamId });

            if(awayMatchTeamId != null) return RedirectToAction("OrganiseAwayMatchTeam", new { id = awayMatchTeamId });

            return RedirectToAction("Index");
        }

        //GET: Team/OrganiseAwayMatchTeam/5
        public async Task<IActionResult> OrganiseAwayMatchTeam(SettingsService settingsService, int id)
        {
            var awayMatchTeam = await _context.AwayMatchTeams
                .Include(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club).ThenInclude(c => c.ClubPlayers)
                .Include(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.League)
                .Include(amt => amt.Team).ThenInclude(t => t.Category)
                .Include(amt => amt.TeamStatus)
                .Include(amt => amt.Match)
                .Where(amt => amt.Id == id)
                .SingleAsync();

            //check rules
            var maxGroupRuleValue = settingsService.GetSettingValue(_context, Constants.Setting.MaxGroupsPerTeam, awayMatchTeam.Team.LeagueClub.League.Id, awayMatchTeam.Team.Category.Id).Value;

            var groups =
                await _context.Groups
                    .Where(g => g.Id <= maxGroupRuleValue)
                    .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                    .ToListAsync();
            // set selected group id
            var groupId = TempData["groupId"] == null ? 0 : int.Parse(TempData["groupId"].ToString());

            var clubPlayers = await GetClubPlayersByClubAndCategoryAsync(awayMatchTeam.Team.LeagueClub.Club.Id, awayMatchTeam.Team.Category.Id);

            var matchPlayers = await _context.AwayMatchTeamGroupPlayers
                .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cu => cu.PlayerDetail)
                .Include(hmtgp => hmtgp.AwayMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Where(hmtgp => hmtgp.AwayMatchTeamGroup.AwayMatchTeam.Match.Id == awayMatchTeam.Match.Id)
                .ToListAsync();

            var assignedTeamPlayers = matchPlayers
                    .Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = String.Format("{0} {1} ({2})", mp.ClubPlayer.PlayerDetail.FirstName, mp.ClubPlayer.PlayerDetail.LastName, mp.AwayMatchTeamGroup.Group.Name) })
                    .ToList();

            var assignedPlayerIds = matchPlayers
                .Select(mp => mp.ClubPlayer.Id)
                .ToArray();

            var availablePlayers = clubPlayers
                .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = String.Format("{0} {1}", cp.PlayerDetail.FirstName, cp.PlayerDetail.LastName) })
                .ToList();

            //Work out if there are any warnings.
            var warnings = new List<NameValuePair>();

            //check if anyone has been assigned to the team multiple times
            var playerSelectedMultipleTimes = matchPlayers
                .GroupBy(mp => mp.ClubPlayer.Id)
                .Where(atp => atp.Count() > 1)
                .Select(atp => new NameValuePair
                {
                    Name = String.Format(Constants.TeamStrings.AssignedMultipleTimes, atp.First().ClubPlayer.PlayerDetail.FirstName, atp.First().ClubPlayer.PlayerDetail.LastName),
                    Value = atp.Count().ToString()
                })
                .ToList();
            foreach (var nvp in playerSelectedMultipleTimes) warnings.Add(nvp);

            //check if users who are in a higher divisions who should not be playing this match.

            //check for you users who have played up too many times.

            var viewModel = new OrganiseMatchTeamViewModel
            {
                TeamId = awayMatchTeam.Team.Id,
                AwayMatchTeamId = awayMatchTeam.Id,
                Name = String.Format("{0} - {1}", awayMatchTeam.Team.LeagueClub.Club.Name, awayMatchTeam.Team.Name),
                Status = awayMatchTeam.TeamStatus.Name,
                Groups = groups,
                GroupId = groupId,
                AssignedMatchPlayers = assignedTeamPlayers,
                AvailableMatchlayers = availablePlayers,
                Warnings = warnings
            };

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToAwayMatch(SettingsService settingsService, OrganiseMatchTeamViewModel organiseMatchTeamViewModel)
        {
            TempData["groupId"] = organiseMatchTeamViewModel.GroupId;
            
            if (ModelState.IsValid)
            {
                if (!(organiseMatchTeamViewModel.AvailableMatchPlayerIds == null))
                {
                    var awayMatchTeam = await _context.AwayMatchTeams
                        .Include(hmt => hmt.Match)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.League)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.Category)
                        .Where(hmt => hmt.Id == organiseMatchTeamViewModel.AwayMatchTeamId)
                        .SingleAsync();

                    var group = await _context.Groups.FindAsync(organiseMatchTeamViewModel.GroupId);

                    var settings = settingsService.GetSettingValues(_context, Constants.Setting.MaxPlayersPerGroup, awayMatchTeam.Team.LeagueClub.League.Id);
                    var maxPlayersPerGroupRuleValue = settings.Where(r => r.Id == Constants.Setting.MaxPlayersPerGroup).Single().Value;
                    var maxGroupsPerTeamRuleValue = settings.Where(r => r.Id == Constants.Setting.MaxGroupsPerTeam).Single().Value;
                    var completedTeamValue = maxGroupsPerTeamRuleValue * maxPlayersPerGroupRuleValue;

                    //determine how many current players there are and how many are being added.
                    var currentTeamPlayers = await _context.AwayMatchTeamGroupPlayers
                        .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cu => cu.PlayerDetail)
                        .Include(hmtgp => hmtgp.AwayMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                        .Where(hmtgp => hmtgp.AwayMatchTeamGroup.AwayMatchTeam.Match.Id == awayMatchTeam.Match.Id)
                        .ToListAsync();
                    var currentTeamPlayerCount = currentTeamPlayers.Count();
                    var currentTeamPlayerGroupCount = currentTeamPlayers.Where(ctp => ctp.AwayMatchTeamGroup.Group.Id == group.Id).Count();
                    var newTeamPlayersCount = organiseMatchTeamViewModel.AvailableMatchPlayerIds.Count();

                    //if too many are being added show an error message.
                    if ((currentTeamPlayerGroupCount + newTeamPlayersCount) > maxPlayersPerGroupRuleValue)
                    {
                        TempData["errorMessage"] = String.Format("The maximum amount of players allowed in each group is {0}", maxPlayersPerGroupRuleValue);
                        return RedirectToAction("OrganiseAwayMatchTeam", new { id = organiseMatchTeamViewModel.AwayMatchTeamId });
                    }

                    //update the team status accordingly 
                    var teamStatusId = Constants.TeamStatus.New;
                    teamStatusId = currentTeamPlayerCount + newTeamPlayersCount == completedTeamValue ? Constants.TeamStatus.Complete : Constants.TeamStatus.InProgress;
                    awayMatchTeam.TeamStatus = _context.TeamStatuses.Find(teamStatusId);

                    //get the current homeMatchTeamGroup
                    var awayMatchTeamGroup = await _context.AwayMatchTeamGroups
                        .Where(hmtg => hmtg.AwayMatchTeam.Id == awayMatchTeam.Id && hmtg.Group.Id == group.Id)
                        .FirstOrDefaultAsync();

                    //if it does not exist create it!
                    if (awayMatchTeamGroup == null)
                    {
                        awayMatchTeamGroup = new AwayMatchTeamGroup
                        {
                            AwayMatchTeam = awayMatchTeam,
                            Group = group
                        };
                        _context.AwayMatchTeamGroups.Add(awayMatchTeamGroup);
                    }
                    await _context.SaveChangesAsync(); //save all changes so far.

                    foreach (int id in organiseMatchTeamViewModel.AvailableMatchPlayerIds)
                    {
                        var awayMatchTeamGroupPlayer = new AwayMatchTeamGroupPlayer
                        {
                            ClubPlayer = await _context.ClubPlayers.FindAsync(id),
                            AwayMatchTeamGroup = awayMatchTeamGroup
                        };
                        _context.AwayMatchTeamGroupPlayers.Add(awayMatchTeamGroupPlayer);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("OrganiseAwayMatchTeam", new { id = organiseMatchTeamViewModel.AwayMatchTeamId });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReassignFromAwayMatch(OrganiseMatchTeamViewModel organiseMatchTeamViewModel)
        {
            TempData["groupId"] = organiseMatchTeamViewModel.GroupId;

            if (ModelState.IsValid)
            {

                if (!(organiseMatchTeamViewModel.AssignedMatchPlayerIds == null))
                {
                    var awayMatchTeam = await _context.AwayMatchTeams
                        .Include(hmt => hmt.Match)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.League)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.Category)
                        .Where(hmt => hmt.Id == organiseMatchTeamViewModel.AwayMatchTeamId)
                        .SingleAsync();

                    var currentTeamPlayers = await _context.AwayMatchTeamGroupPlayers
                        .Include(amtgp => amtgp.ClubPlayer).ThenInclude(cu => cu.PlayerDetail)
                        .Include(amtgp => amtgp.AwayMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                        .Where(amtgp => amtgp.AwayMatchTeamGroup.AwayMatchTeam.Match.Id == awayMatchTeam.Match.Id)
                        .ToListAsync();

                    var teamStatusId = currentTeamPlayers.Count() - organiseMatchTeamViewModel.AssignedMatchPlayerIds.Count() == 0 ? Constants.TeamStatus.New : Constants.TeamStatus.InProgress;
                    awayMatchTeam.TeamStatus = _context.TeamStatuses.Find(teamStatusId);

                    foreach (int id in organiseMatchTeamViewModel.AssignedMatchPlayerIds)
                    {
                        var awayMatchTeamGroupPlayer = await _context.AwayMatchTeamGroupPlayers.Where(amtgp => amtgp.Id == id).SingleOrDefaultAsync();
                        _context.AwayMatchTeamGroupPlayers.Remove(awayMatchTeamGroupPlayer);
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("OrganiseAwayMatchTeam", new { id = organiseMatchTeamViewModel.AwayMatchTeamId });
        }


        //GET: Team/OrganiseHomeMatchTeam/5
        public async Task<IActionResult> OrganiseHomeMatchTeam(SettingsService settingsService, int id)
        {
            var homeMatchTeam = await _context.HomeMatchTeams
                .Include(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club).ThenInclude(c => c.ClubPlayers)
                .Include(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.League)
                .Include(hmt => hmt.Team).ThenInclude(t => t.Category)
                .Include(hmt => hmt.TeamStatus)
                .Include(hmt => hmt.Match)
                .Where(hmt => hmt.Id == id)
                .SingleAsync();


            //check rules
            var maxGroupRuleValue = settingsService.GetSettingValue(_context, Constants.Setting.MaxGroupsPerTeam, homeMatchTeam.Team.LeagueClub.League.Id, homeMatchTeam.Team.Category.Id).Value;

            var groups =
                await _context.Groups
                    .Where(g => g.Id <= maxGroupRuleValue)
                    .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                    .ToListAsync();
            // set selected group id
            var groupId = TempData["groupId"] == null ? 0 : int.Parse(TempData["groupId"].ToString());

            var clubPlayers = await GetClubPlayersByClubAndCategoryAsync(homeMatchTeam.Team.LeagueClub.Club.Id, homeMatchTeam.Team.Category.Id);

            var matchPlayers = await _context.HomeMatchTeamGroupPlayers
                .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cu => cu.PlayerDetail)
                .Include(hmtgp => hmtgp.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                .Where(hmtgp => hmtgp.HomeMatchTeamGroup.HomeMatchTeam.Match.Id == homeMatchTeam.Match.Id)
                .ToListAsync();

            var assignedTeamPlayers = matchPlayers
                    .Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = String.Format("{0} {1} ({2})", mp.ClubPlayer.PlayerDetail.FirstName, mp.ClubPlayer.PlayerDetail.LastName, mp.HomeMatchTeamGroup.Group.Name) })
                    .ToList();

            var assignedPlayerIds = matchPlayers                
                .Select(mp => mp.ClubPlayer.Id)
                .ToArray();

            var availablePlayers = clubPlayers
                .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = String.Format("{0} {1}", cp.PlayerDetail.FirstName, cp.PlayerDetail.LastName)})
                .ToList();

            //Work out if there are any warnings.
            var warnings = new List<NameValuePair>();

            //check if anyone has been assigned to the team multiple times
            var playerSelectedMultipleTimes = matchPlayers
                .GroupBy(mp => mp.ClubPlayer.Id)
                .Where( atp => atp.Count() > 1)
                .Select(atp => new NameValuePair
                    {
                        Name = String.Format( Constants.TeamStrings.AssignedMultipleTimes, atp.First().ClubPlayer.PlayerDetail.FirstName, atp.First().ClubPlayer.PlayerDetail.LastName),
                        Value = atp.Count().ToString()
                    })
                .ToList();
            foreach (var nvp in playerSelectedMultipleTimes) warnings.Add(nvp);

            //check if users who are in a higher divisions who should not be playing this match.

            //check for you users who have played up too many times.

            var viewModel = new OrganiseMatchTeamViewModel
            {
                TeamId = homeMatchTeam.Team.Id,
                HomeMatchTeamId = homeMatchTeam.Id,
                Name = String.Format("{0} - {1}", homeMatchTeam.Team.LeagueClub.Club.Name, homeMatchTeam.Team.Name),
                Status = homeMatchTeam.TeamStatus.Name,
                Groups = groups,
                GroupId = groupId,
                AssignedMatchPlayers = assignedTeamPlayers,
                AvailableMatchlayers = availablePlayers,
                Warnings = warnings
            };

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToHomeMatch(SettingsService settingsService, OrganiseMatchTeamViewModel organiseMatchTeamViewModel)
        {
            TempData["groupId"] = organiseMatchTeamViewModel.GroupId;

            if (ModelState.IsValid)
            {
                if (!(organiseMatchTeamViewModel.AvailableMatchPlayerIds == null))
                {
                    var homeMatchTeam = await _context.HomeMatchTeams
                        .Include(hmt => hmt.Match)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.League)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.Category)
                        .Where(hmt => hmt.Id == organiseMatchTeamViewModel.HomeMatchTeamId)
                        .SingleAsync();
                    
                    var group = await _context.Groups.FindAsync(organiseMatchTeamViewModel.GroupId);

                    var settings = settingsService.GetSettingValues(_context, Constants.Setting.MaxPlayersPerGroup, homeMatchTeam.Team.LeagueClub.League.Id);
                    var maxPlayersPerGroupRuleValue = settings.Where(r => r.Id == Constants.Setting.MaxPlayersPerGroup).Single().Value;
                    var maxGroupsPerTeamRuleValue = settings.Where(r => r.Id == Constants.Setting.MaxGroupsPerTeam).Single().Value;
                    var completedTeamValue = maxGroupsPerTeamRuleValue * maxPlayersPerGroupRuleValue;

                    //determine how many current players there are and how many are being added.
                    var currentTeamPlayers = await _context.HomeMatchTeamGroupPlayers
                        .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cu => cu.PlayerDetail)
                        .Include(hmtgp => hmtgp.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                        .Where(hmtgp => hmtgp.HomeMatchTeamGroup.HomeMatchTeam.Match.Id == homeMatchTeam.Match.Id)
                        .ToListAsync();
                    var currentTeamPlayerCount = currentTeamPlayers.Count();
                    var currentTeamPlayerGroupCount = currentTeamPlayers.Where(ctp => ctp.HomeMatchTeamGroup.Group.Id == group.Id).Count();
                    var newTeamPlayersCount = organiseMatchTeamViewModel.AvailableMatchPlayerIds.Count();

                    //if too many are being added show an error message.
                    if ((currentTeamPlayerGroupCount + newTeamPlayersCount) > maxPlayersPerGroupRuleValue)
                    {
                        TempData["errorMessage"] = String.Format("The maximum amount of players allowed in each group is {0}", maxPlayersPerGroupRuleValue);
                        return RedirectToAction("OrganiseHomeMatchTeam", new { id = organiseMatchTeamViewModel.HomeMatchTeamId });
                    }

                    //update the team status accordingly 
                    var teamStatusId = Constants.TeamStatus.New;
                    teamStatusId = currentTeamPlayerCount + newTeamPlayersCount == completedTeamValue ? Constants.TeamStatus.Complete : Constants.TeamStatus.InProgress;
                    homeMatchTeam.TeamStatus = _context.TeamStatuses.Find(teamStatusId);

                    //get the current homeMatchTeamGroup
                    var homeMatchTeamGroup = await _context.HomeMatchTeamGroups
                        .Where(hmtg => hmtg.HomeMatchTeam.Id == homeMatchTeam.Id && hmtg.Group.Id == group.Id)
                        .FirstOrDefaultAsync();

                    //if it does not exist create it!
                    if (homeMatchTeamGroup == null)
                    {
                        homeMatchTeamGroup = new HomeMatchTeamGroup
                        {
                            HomeMatchTeam = homeMatchTeam,
                            Group = group
                        };
                        _context.HomeMatchTeamGroups.Add(homeMatchTeamGroup);
                    }
                    await _context.SaveChangesAsync(); //save all changes so far.

                    foreach (int id in organiseMatchTeamViewModel.AvailableMatchPlayerIds)
                    {
                        var homeMatchTeamGroupPlayer = new HomeMatchTeamGroupPlayer
                        {
                            ClubPlayer = await _context.ClubPlayers.FindAsync(id),
                            HomeMatchTeamGroup = homeMatchTeamGroup                            
                        };
                        _context.HomeMatchTeamGroupPlayers.Add(homeMatchTeamGroupPlayer);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("OrganiseHomeMatchTeam", new { id = organiseMatchTeamViewModel.HomeMatchTeamId });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReassignFromHomeMatch(OrganiseMatchTeamViewModel organiseMatchTeamViewModel)
        {
            TempData["groupId"] = organiseMatchTeamViewModel.GroupId;
            
            if (ModelState.IsValid)
            {   
                if (!(organiseMatchTeamViewModel.AssignedMatchPlayerIds == null))
                {
                    var homeMatchTeam = await _context.HomeMatchTeams
                        .Include(hmt => hmt.Match)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.League)
                        .Include(hmt => hmt.Team).ThenInclude(t => t.Category)
                        .Where(hmt => hmt.Id == organiseMatchTeamViewModel.HomeMatchTeamId)
                        .SingleAsync();

                    var currentTeamPlayers = await _context.HomeMatchTeamGroupPlayers
                        .Include(hmtgp => hmtgp.ClubPlayer).ThenInclude(cu => cu.PlayerDetail)
                        .Include(hmtgp => hmtgp.HomeMatchTeamGroup).ThenInclude(hmtg => hmtg.Group)
                        .Where(hmtgp => hmtgp.HomeMatchTeamGroup.HomeMatchTeam.Match.Id == homeMatchTeam.Match.Id)
                        .ToListAsync();

                    var teamStatusId = currentTeamPlayers.Count() - organiseMatchTeamViewModel.AssignedMatchPlayerIds.Count() == 0 ? Constants.TeamStatus.New : Constants.TeamStatus.InProgress;
                    homeMatchTeam.TeamStatus = _context.TeamStatuses.Find(teamStatusId);

                    foreach (int id in organiseMatchTeamViewModel.AssignedMatchPlayerIds)
                    {
                        var homeMatchTeamGroupPlayer = await _context.HomeMatchTeamGroupPlayers.Where(hmtgp => hmtgp.Id == id).SingleOrDefaultAsync();
                        _context.HomeMatchTeamGroupPlayers.Remove(homeMatchTeamGroupPlayer);
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("OrganiseHomeMatchTeam", new { id = organiseMatchTeamViewModel.HomeMatchTeamId });
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(SettingsService settingsService, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = await _context.Teams
                .Include( t => t.LeagueClub).ThenInclude( lc => lc.Club).ThenInclude(c => c.ClubPlayers)
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.League)
                .Include( t=> t.TeamPlayers)
                .Include( t=> t.Category)
                .Include( t=> t.Season)
                .Include( t=> t.TeamStatus)

                .FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            var teams = new List<SelectListItem> { new SelectListItem { Value = team.Id.ToString(), Text = team.Name } };

            //check rules
            var maxGroupRuleValue = settingsService.GetSettingValue(_context, Constants.Setting.MaxGroupsPerTeam, team.LeagueClub.League.Id, team.Category.Id).Value;

            var groups = 
                await _context.Groups
                    .Where(g => g.Id <= maxGroupRuleValue)
                    .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                    .ToListAsync();
            // set selected group id
            var groupId = TempData["groupId"] == null? 0: int.Parse(TempData["groupId"].ToString());
            
            var assignedTeamPlayers =
                await _context.TeamPlayers
                    .Where(tp => tp.Team.Id == id)
                    .Select(tp => new SelectListItem { Value = tp.Id.ToString(), Text = String.Format("{0} {1} ({2})", tp.ClubPlayer.PlayerDetail.FirstName, tp.ClubPlayer.PlayerDetail.LastName, tp.Group.Name) })
                    .ToListAsync();

            var assignedPlayerIds = await _context.TeamPlayers
                .Where(tp => tp.Team.Id == id)
                .Select(tp => tp.ClubPlayer.Id)
                .ToListAsync();

            var clubPlayers = await GetClubPlayersByClubAndCategoryAsync(team.LeagueClub.Club.Id, team.Category.Id);

            var availableTeamPlayers = clubPlayers
                .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = String.Format("{0} {1}", cp.PlayerDetail.FirstName, cp.PlayerDetail.LastName) })
                .ToList();

            var manageTeamPlayersViewModel = new ManageTeamPlayersViewModel
            {
                GroupId = groupId,
                Teams = teams,
                Groups = groups,
                AssignedTeamPlayers = assignedTeamPlayers,
                AvailableTeamPlayers = availableTeamPlayers,
                TeamStatus = team.TeamStatus
            };

            return View(manageTeamPlayersViewModel);
        }

        private async Task<List<ClubPlayer>> GetClubPlayersByClubAndCategoryAsync(int clubId, int categoryId)
        {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToTeam(SettingsService settingsService, ManageTeamPlayersViewModel manageTeamPlayersViewModel)
        {
            TempData["groupId"] = manageTeamPlayersViewModel.GroupId;

            if (ModelState.IsValid)
            {
                if( !(manageTeamPlayersViewModel.AvailableTeamPlayerIds == null))
                {
                    var team = await _context.Teams.Include(t=>t.LeagueClub).ThenInclude(lc=>lc.League).Include(t=>t.Category).Where(t=> t.Id == manageTeamPlayersViewModel.TeamId).SingleAsync();
                    var group = await _context.Groups.FindAsync(manageTeamPlayersViewModel.GroupId);
                    var settings = settingsService.GetSettingValues(_context, Constants.Setting.MaxPlayersPerGroup, team.LeagueClub.League.Id);
                    var maxPlayersPerGroupRuleValue = settings.Where(r => r.Id == Constants.Setting.MaxPlayersPerGroup).Single().Value;
                    var maxGroupsPerTeamRuleValue = settings.Where(r => r.Id == Constants.Setting.MaxGroupsPerTeam).Single().Value;
                    var completedTeamValue = maxGroupsPerTeamRuleValue * maxPlayersPerGroupRuleValue;

                    //determine how many current players there are and how many are being added.
                    var currentTeamPlayers = await _context.TeamPlayers.Include(tp => tp.Group).Where(t => t.Team.Id == team.Id).ToListAsync();
                    var currentTeamPlayerCount = currentTeamPlayers.Count();
                    var currentTeamPlayerGroupCount = currentTeamPlayers.Where( ctp => ctp.Group.Id == group.Id).Count();
                    var newTeamPlayersCount = manageTeamPlayersViewModel.AvailableTeamPlayerIds.Count();

                    //if too many are being added show an error message.
                    if((currentTeamPlayerGroupCount + newTeamPlayersCount) > maxPlayersPerGroupRuleValue)
                    {
                        TempData["errorMessage"] = String.Format("The maximum amount of players allowed in each group is {0}", maxPlayersPerGroupRuleValue);
                        return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId });
                    }

                    //update the team status accordingly 
                    var teamStatusId = Constants.TeamStatus.New;
                    teamStatusId = currentTeamPlayerCount + newTeamPlayersCount == completedTeamValue ? Constants.TeamStatus.Complete : Constants.TeamStatus.InProgress;
                    team.TeamStatus = _context.TeamStatuses.Find(teamStatusId);
                                        
                    foreach (int id in manageTeamPlayersViewModel.AvailableTeamPlayerIds)
                    {
                        var newTeamPlayer = new TeamPlayer
                        {
                            ClubPlayer = await _context.ClubPlayers.FindAsync(id),
                            Group = group,
                            Team = team                                                        
                        };
                        _context.TeamPlayers.Add(newTeamPlayer);                        
                    }

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReassignFromTeam(ManageTeamPlayersViewModel manageTeamPlayersViewModel)
        {
            TempData["groupId"] = manageTeamPlayersViewModel.GroupId;

            if (ModelState.IsValid)
            {

                if(!(manageTeamPlayersViewModel.AssignedTeamPlayerIds == null))
                {
                    var team = await _context.Teams.FindAsync(manageTeamPlayersViewModel.TeamId);
                    var currentTeamPlayers = await _context.TeamPlayers.Include(tp => tp.Group).Where(t => t.Team.Id == team.Id).ToListAsync();
                    var teamStatusId = currentTeamPlayers.Count() - manageTeamPlayersViewModel.AssignedTeamPlayerIds.Count() == 0 ? Constants.TeamStatus.New : Constants.TeamStatus.InProgress;
                    team.TeamStatus = _context.TeamStatuses.Find(teamStatusId);

                    foreach (int id in manageTeamPlayersViewModel.AssignedTeamPlayerIds)
                    {
                        var teamPlayer = await _context.TeamPlayers.Where(tp => tp.Id == id).SingleOrDefaultAsync();
                        _context.TeamPlayers.Remove(teamPlayer);
                    }                    
                }
                
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId });
        }

        // GET: Team/Create
        public async Task<IActionResult> Create(int leagueClubId)
        {
            var leagueClub = await _context.LeagueClubs
                .Include(lc => lc.Club)
                .Include(lc => lc.League)
                .Where(lc => lc.Id == leagueClubId).FirstOrDefaultAsync();

            var leagueClubs = new List<SelectListItem> { new SelectListItem { Value = leagueClub.Id.ToString(), Text = leagueClub.Club.Name } };

            var categories = await _context.Categories
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync();

            var divisions = await _context.Divisions
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync();

            var seasons = await _context.Seasons
                    .Where(s => s.IsActive == true && s.League.Id == leagueClub.League.Id)
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync();

            CreateTeamViewModel createTeamViewModel = new CreateTeamViewModel
            {
                LeagueClubs = leagueClubs,
                Categories = categories,
                Divisions = divisions,
                Seasons = seasons
            };
            return View(createTeamViewModel);
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeamViewModel createTeamViewModel)
        {            
            if (ModelState.IsValid)
            {

                Team team = new Team
                {
                    Name = createTeamViewModel.Name,
                    Category = await _context.Categories.FindAsync(createTeamViewModel.CategoryId),
                    Division = await _context.Divisions.FindAsync(createTeamViewModel.DivisionId),
                    Season = await _context.Seasons.FindAsync(createTeamViewModel.SeasonId),
                    LeagueClub = await _context.LeagueClubs.FindAsync(createTeamViewModel.LeagueClubId),
                    TeamStatus = _context.TeamStatuses.Find(Constants.TeamStatus.New)
                };

                _context.Teams.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(createTeamViewModel);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.SingleOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .SingleOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.SingleOrDefaultAsync(m => m.Id == id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
