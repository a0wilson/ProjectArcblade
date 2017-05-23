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
using static ProjectArcBlade.Data.Constants;

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
        public async Task<ActionResult> Dashboard(TeamService teamService, MatchService matchService, int? id)
        {
            if (id == null) return NotFound();

            var viewModel = await teamService.GetTeamDashboardViewModelAsync(_context, matchService, Convert.ToInt32(id));

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BackToDashboard(int teamId)
        {
            return RedirectToAction("Dashboard", new { id = teamId });
        }
        
        //POST: Team/ManageNominations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dashboard(TeamService teamService, TeamDashboardViewModel viewModel)
        {
            return RedirectToAction("Dashboard", new { id = viewModel.SelectedTeamId });
        }

        // GET: Team/ConfirmHomeMatchTeam/5
        public async Task<ActionResult> ConfirmHomeMatchTeam(TeamService teamService, int homeMatchTeamId)
        {
            var viewModel = await teamService.GetHomeMatchTeamByIdAsync(_context, homeMatchTeamId);
            return View(viewModel);
        }

        // GET: Team/ConfirmAwayMatchTeam/5
        public async Task<ActionResult> ConfirmAwayMatchTeam(TeamService teamService, int awayMatchTeamId)
        {
            var viewModel = await teamService.GetAwayMatchTeamByIdAsync(_context, awayMatchTeamId);
            return View(viewModel);
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.Include(t=>t.TeamStatus).Include(t=>t.LeagueClub).ThenInclude(lc=>lc.Club).ToListAsync());
        }
        
        //GET: Team/ManageNominations
        public async Task<IActionResult> ManageNominations(TeamService teamService, MatchService matchService, int id)
        {
            var viewModel = await teamService.GetManageNominationsViewModelAsync(_context, matchService, id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateHomeMatchTeam(TeamService teamService, int homeMatchTeamId, int teamId)
        {
            if (await teamService.ActivateHomeMatchTeam(_context, homeMatchTeamId) )
            {
                return RedirectToAction("Dashboard", new { id = teamId });
            }
            return RedirectToAction("ConfirmHomeMatchTeam", new { homeMatchTeamId = homeMatchTeamId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateAwayMatchTeam(TeamService teamService, int awayMatchTeamId, int teamId)
        {
            if (await teamService.ActivateAwayMatchTeam(_context, awayMatchTeamId))
            {
                return RedirectToAction("Dashboard", new { id = teamId });
            }
            return RedirectToAction("ConfirmAwayMatchTeam", new { awayMatchTeamId = awayMatchTeamId });
        }

        //POST: Team/ManageNominations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageNominations(TeamService teamService, ManageNominationsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await teamService.UpdateNominationsWithManageNominationsViewModelAsync(_context, viewModel);
                TempData["successMessage"] = "Player nominations updated!";
            }
            return RedirectToAction("ManageNominations", new { id = viewModel.TeamId });
        }
        
        //GET: Team/ManageNominations
        public async Task<IActionResult> ManageMatchTeam(TeamService teamService, MatchService matchService, int? awayMatchTeamId, int? homeMatchTeamId)
        {
            var id = Convert.ToInt32( awayMatchTeamId == null ? homeMatchTeamId : awayMatchTeamId );
            var teamType = awayMatchTeamId == null ? TeamType.Home : TeamType.Away;
            var viewModel = await teamService.GetManageMatchTeamViewModelAsync(_context, matchService, teamType, id);
            return View(viewModel);
        }

        //POST: Team/ManageNominations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageMatchTeam(TeamService teamService, ManageMatchTeamViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await teamService.UpdateMatchTeamsWithManageMatchTeamViewModelAsync(_context, viewModel);
                TempData["successMessage"] = "Match team updated!";
            }
            
            if(viewModel.IsHomeMatch)
            {
                return RedirectToAction( "ManageMatchTeam", new { homeMatchTeamId = viewModel.HomeMatchTeamId });
            }
            else
            {
                return RedirectToAction("ManageMatchTeam", new { awayMatchTeamId = viewModel.AwayMatchTeamId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BackToManageMatchTeam( int? homeMatchTeamId, int? awayMatchTeamId)
        {
            return RedirectToAction("ManageMatchTeam", new { homeMatchTeamId = homeMatchTeamId, awayMatchTeamId = awayMatchTeamId });
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

            //var clubPlayers = await GetClubPlayersByClubAndCategoryAsync(_context, team.LeagueClub.Club.Id, team.Category.Id);

            //var availableTeamPlayers = clubPlayers
            //    .Select(cp => new SelectListItem { Value = cp.Id.ToString(), Text = String.Format("{0} {1}", cp.PlayerDetail.FirstName, cp.PlayerDetail.LastName) })
            //    .ToList();

            var manageTeamPlayersViewModel = new ManageTeamPlayersViewModel
            {
                GroupId = groupId,
                Teams = teams,
                Groups = groups,
                AssignedTeamPlayers = assignedTeamPlayers,
                //AvailableTeamPlayers = availableTeamPlayers,
                TeamStatus = team.TeamStatus
            };

            return View(manageTeamPlayersViewModel);
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
