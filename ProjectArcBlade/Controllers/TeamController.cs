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

        // GET: Team
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.Include(t=>t.LeagueClub).ThenInclude(lc=>lc.Club).ToListAsync());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(SettingsService settingsService, int? id, bool filterAvailablePlayers)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = await _context.Teams
                .Include( t => t.LeagueClub).ThenInclude( lc => lc.Club).ThenInclude(c => c.ClubUsers)
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
                    .Select(tp => new SelectListItem { Value = tp.Id.ToString(), Text = String.Format("{0} {1} ({2})", tp.ClubUser.UserDetail.FirstName, tp.ClubUser.UserDetail.LastName, tp.Group.Name) })
                    .ToListAsync();

            var assignedPlayerIds = await _context.TeamPlayers
                .Where(tp => tp.Team.Season.Id == team.Season.Id && tp.Team.Category.Id == team.Category.Id)
                .Select(tp => tp.ClubUser.Id)
                .ToListAsync();
                
            var availableTeamPlayers = new List<SelectListItem>();
            switch (team.Category.Id)
            {
                case Constants.Category.Mens:
                    availableTeamPlayers = filterAvailablePlayers 
                        ?
                            await _context.ClubUsers
                                .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && !assignedPlayerIds.Contains(cu.Id) && cu.UserDetail.Gender.Id == Constants.Gender.Male)
                                .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                                .ToListAsync()
                        :
                            await _context.ClubUsers
                                .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && cu.UserDetail.Gender.Id == Constants.Gender.Male)
                                .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                                .ToListAsync();
                    break;
                case Constants.Category.Womens:
                    availableTeamPlayers = filterAvailablePlayers
                        ?
                            await _context.ClubUsers
                                .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && !assignedPlayerIds.Contains(cu.Id) && cu.UserDetail.Gender.Id == Constants.Gender.Female)
                                .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                                .ToListAsync()
                        :
                            await _context.ClubUsers
                                .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && cu.UserDetail.Gender.Id == Constants.Gender.Female)
                                .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                                .ToListAsync();
                    break;
                default:
                    availableTeamPlayers = filterAvailablePlayers
                        ?
                            await _context.ClubUsers
                                .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && !assignedPlayerIds.Contains(cu.Id))
                                .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                                .ToListAsync()
                        :
                            await _context.ClubUsers
                                .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id)
                                .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                                .ToListAsync();
                    break;
            }
            
            var manageTeamPlayersViewModel = new ManageTeamPlayersViewModel
            {
                GroupId = groupId,
                Teams = teams,
                Groups = groups,
                AssignedTeamPlayers = assignedTeamPlayers,
                AvailableTeamPlayers = availableTeamPlayers,
                FilterAvailablePlayers = filterAvailablePlayers,
                TeamStatus = team.TeamStatus
            };

            return View(manageTeamPlayersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RefreshAvailablePlayers(ManageTeamPlayersViewModel manageTeamPlayersViewModel)
        {
            TempData["groupId"] = manageTeamPlayersViewModel.GroupId;
            return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId, filterAvailablePlayers = manageTeamPlayersViewModel.FilterAvailablePlayers });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(SettingsService settingsService, ManageTeamPlayersViewModel manageTeamPlayersViewModel)
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
                        return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId, filterAvailablePlayers = manageTeamPlayersViewModel.FilterAvailablePlayers });
                    }

                    //update the team status accordingly 
                    var teamStatusId = Constants.TeamStatus.New;
                    teamStatusId = currentTeamPlayerCount + currentTeamPlayerGroupCount == completedTeamValue ? Constants.TeamStatus.Complete : Constants.TeamStatus.InProgress;
                    team.TeamStatus = _context.TeamStatuses.Find(teamStatusId);
                                        
                    foreach (int id in manageTeamPlayersViewModel.AvailableTeamPlayerIds)
                    {
                        var newTeamPlayer = new TeamPlayer
                        {
                            ClubUser = await _context.ClubUsers.FindAsync(id),
                            Group = group,
                            Team = team                                                        
                        };
                        _context.TeamPlayers.Add(newTeamPlayer);                        
                    }

                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId, filterAvailablePlayers = manageTeamPlayersViewModel.FilterAvailablePlayers });
            }
            return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId, filterAvailablePlayers = manageTeamPlayersViewModel.FilterAvailablePlayers });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reassign(ManageTeamPlayersViewModel manageTeamPlayersViewModel)
        {
            TempData["groupId"] = manageTeamPlayersViewModel.GroupId;

            if (ModelState.IsValid)
            {

                if(!(manageTeamPlayersViewModel.AssignedTeamPlayerIds == null))
                {
                    foreach(int id in manageTeamPlayersViewModel.AssignedTeamPlayerIds)
                    {
                        var teamPlayer = await _context.TeamPlayers.Where(tp => tp.Id == id).SingleOrDefaultAsync();
                        _context.TeamPlayers.Remove(teamPlayer);
                    }                    
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId, filterAvailablePlayers = manageTeamPlayersViewModel.FilterAvailablePlayers });
            }
            return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId, filterAvailablePlayers = manageTeamPlayersViewModel.FilterAvailablePlayers });
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
