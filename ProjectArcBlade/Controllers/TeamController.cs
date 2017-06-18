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

        //POST: Team/Dashboard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dashboard(TeamService teamService, TeamDashboardViewModel viewModel)
        {
            return RedirectToAction("Dashboard", new { id = viewModel.TeamId });
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
        public async Task<IActionResult> ManageMatchTeam(TeamService teamService, MatchService matchService, int matchTeamId, TeamType teamType)
        {
            var viewModel = await teamService.GetManageMatchTeamViewModelAsync(_context, matchService, teamType, matchTeamId);
            return View(viewModel);
        }

        //POST: Team/ManageNominations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageMatchTeam(TeamService teamService, ManageMatchTeamViewModel viewModel, string btnBack, string btnSave, string btnSubmit )
        {
            if( btnBack != null)
            {
                return RedirectToAction("Preview", "Match", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId });
            }
            
            if(btnSave != null)
            {
                if (ModelState.IsValid)
                {
                    await teamService.UpdateMatchTeamsWithManageMatchTeamViewModelAsync(_context, viewModel);
                    TempData["successMessage"] = "Match team updated!";
                }

                return RedirectToAction("ManageMatchTeam", new { matchTeamId = viewModel.MatchTeamId, teamType = viewModel.TeamType });
            }

            if(btnSubmit != null)
            {
                
                await teamService.UpdateMatchTeamsWithManageMatchTeamViewModelAsync(_context, viewModel);

                switch (viewModel.TeamType)
                {
                    case TeamType.Home:                        
                        if (await teamService.ActivateHomeMatchTeam(_context, viewModel.MatchTeamId)) return RedirectToAction("Dashboard", new { id = viewModel.TeamId });
                        return RedirectToAction("ManageMatchTeam", new { matchTeamId = viewModel.MatchTeamId, teamType = viewModel.TeamType });

                    case TeamType.Away:
                        if (await teamService.ActivateAwayMatchTeam(_context, viewModel.MatchTeamId)) return RedirectToAction("Dashboard", new { id = viewModel.TeamId });
                        return RedirectToAction("ManageMatchTeam", new { matchTeamId = viewModel.MatchTeamId, teamType = viewModel.TeamType });
                }
            }

            return RedirectToAction("ManageMatchTeam", new { matchTeamId = viewModel.MatchTeamId, teamType = viewModel.TeamType });
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
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

            
            var groups = 
                await _context.Groups
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
            
            var manageTeamPlayersViewModel = new ManageTeamPlayersViewModel
            {
                GroupId = groupId,
                Teams = teams,
                Groups = groups,
                AssignedTeamPlayers = assignedTeamPlayers,
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
