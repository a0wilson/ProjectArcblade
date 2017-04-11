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
            return View(await _context.Teams.ToListAsync());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = await _context.Teams
                .Include( t => t.LeagueClub).ThenInclude( lc => lc.Club).ThenInclude(c => c.ClubUsers)
                .Include( t=> t.TeamPlayers)
                .Include( t=> t.Category)
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

            var assignedTeamPlayers =
                await _context.TeamPlayers
                    .Where(tp => tp.Team.Id == id)
                    .Select(tp => new SelectListItem { Value = tp.Id.ToString(), Text = String.Format("{0} {1} ({2})", tp.ClubUser.UserDetail.FirstName, tp.ClubUser.UserDetail.LastName, tp.Group.Name) })
                    .ToListAsync();

            var assignedPlayerIds = team.TeamPlayers.Where(tp => tp.Team.Id == id).Select(tp => tp.ClubUser.Id).ToList();
            var availableTeamPlayers = new List<SelectListItem>();
            switch (team.Category.Id)
            {
                case Constants.Category.Mens:
                    availableTeamPlayers =
                         await _context.ClubUsers
                            .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && !assignedPlayerIds.Contains(cu.Id) && cu.UserDetail.Gender.Id == Constants.Gender.Male)
                            .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                            .ToListAsync();
                    break;
                case Constants.Category.Womens:
                    availableTeamPlayers =
                         await _context.ClubUsers
                            .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && !assignedPlayerIds.Contains(cu.Id) && cu.UserDetail.Gender.Id == Constants.Gender.Female)
                            .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                            .ToListAsync();
                    break;
                default:
                    availableTeamPlayers =
                         await _context.ClubUsers
                            .Where(cu => cu.Club.Id == team.LeagueClub.Club.Id && !assignedPlayerIds.Contains(cu.Id))
                            .Select(cu => new SelectListItem { Value = cu.Id.ToString(), Text = String.Format("{0} {1}", cu.UserDetail.FirstName, cu.UserDetail.LastName) })
                            .ToListAsync();
                    break;
            }


            var manageTeamPlayersViewModel = new ManageTeamPlayersViewModel
            {
                Teams = teams,
                Groups = groups,
                AssignedTeamPlayers = assignedTeamPlayers,
                AvailableTeamPlayers = availableTeamPlayers                
            };

            return View(manageTeamPlayersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(ManageTeamPlayersViewModel manageTeamPlayersViewModel)
        {
            if (ModelState.IsValid)
            {
                if( !(manageTeamPlayersViewModel.AvailableTeamPlayerIds == null))
                {
                    foreach (int id in manageTeamPlayersViewModel.AvailableTeamPlayerIds)
                    {
                        var newTeamPlayer = new TeamPlayer
                        {
                            ClubUser = await _context.ClubUsers.FindAsync(id),
                            Group = await _context.Groups.FindAsync(manageTeamPlayersViewModel.GroupId),
                            Team = await _context.Teams.FindAsync(manageTeamPlayersViewModel.TeamId)
                        };
                        _context.TeamPlayers.Add(newTeamPlayer);
                        await _context.SaveChangesAsync();
                    }
                }
                
                return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId });
            }
            return View(manageTeamPlayersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reassign(ManageTeamPlayersViewModel manageTeamPlayersViewModel)
        {
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
                return RedirectToAction("Details", new { id = manageTeamPlayersViewModel.TeamId });
            }
            return View(manageTeamPlayersViewModel);
        }
        // GET: Team/Create
        public async Task<IActionResult> Create(int leagueClubId)
        {
            var leagueClub = await _context.LeagueClubs
                .Include(lc => lc.Club)
                .Include(lc => lc.League)
                .Where(lc => lc.Id == leagueClubId).FirstOrDefaultAsync();
            
            CreateTeamViewModel createTeamViewModel = new CreateTeamViewModel
            {
                LeagueClubs = new List<SelectListItem> { new SelectListItem { Value = leagueClub.Id.ToString(), Text = leagueClub.Club.Name } },
                Categories = await _context.Categories
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync(),
                Divisions = await _context.Divisions
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync(),
                Seasons = await _context.Seasons
                    .Where(s => s.IsActive == true && s.League.Id == leagueClub.League.Id)
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync()
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
                    LeagueClub = await _context.LeagueClubs.FindAsync(createTeamViewModel.LeagueClubId)
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
