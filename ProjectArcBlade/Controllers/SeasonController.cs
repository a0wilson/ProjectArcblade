using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;
using ProjectArcBlade.Services;
using ProjectArcBlade.Models.SeasonViewModels;

namespace ProjectArcBlade.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeasonController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Seasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seasons.ToListAsync());
        }

        // GET: Seasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons
                .SingleOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // GET: Seasons/Create
        public IActionResult Create(int? leagueId)
        {
            var league = _context.Leagues.Where(l => l.Id == leagueId ).FirstOrDefault();
            return View(new Season {League=league});
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate")] Season season)
        {
            if (ModelState.IsValid)
            {
                _context.Add(season);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(season);
        }

        //GET
        public async Task<IActionResult> ScheduleAllMatches (int seasonId)
        {
            var matches = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.Division)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.Category)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.Division)
                .Include(m => m.AwayMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.Category)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .ToListAsync();

            var divisions = matches
                .GroupBy(ms => ms.HomeMatchTeam.Team.Division.Name)
                .Select(ms => new NameValuePair { Name = ms.First().HomeMatchTeam.Team.Division.Name, Value = ms.Count().ToString() })
                .ToList();

            var viewModel = new ScheduleAllMatchesViewModel
            {
                Matches = matches,
                Divisions = divisions
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ScheduleAllMatches(MatchSchedulingService matchSchedulingService, int seasonId)
        {
            var matchSchedules = matchSchedulingService.ScheduleMatches(_context, seasonId);
            return RedirectToAction("ScheduleAllMatches", new { seasonId = seasonId });
        }
        // GET: Seasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons.SingleOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }
            return View(season);
        }

        // POST: Seasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate")] Season season)
        {
            if (id != season.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(season);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeasonExists(season.Id))
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
            return View(season);
        }

        // GET: Seasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons
                .SingleOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var season = await _context.Seasons.SingleOrDefaultAsync(m => m.Id == id);
            _context.Seasons.Remove(season);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SeasonExists(int id)
        {
            return _context.Seasons.Any(e => e.Id == id);
        }
    }
}
