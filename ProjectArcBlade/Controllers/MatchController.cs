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
using ProjectArcBlade.Models.MatchViewModels;

namespace ProjectArcBlade.Controllers
{
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Match
        public async Task<IActionResult> Index()
        {
            return View(await _context.Matches.ToListAsync());
        }

        // GET: Match/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .SingleOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }


        // GET: Match/CreateStep1
        public async Task<IActionResult> CreateStep1(AppData appData)
        {
            var seasons = await _context.Seasons
                .Where(s => s.IsActive == true && s.League.Id == appData.LeagueId)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToListAsync();

            var categories = await _context.Categories
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync();

            var divisions = await _context.Divisions
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToListAsync();

            var viewlModel = new CreateStep1ViewModel()
            {
                Seasons = seasons,
                Divisions = divisions,
                Categories = categories
            };

            return View(viewlModel);
        }

        // POST: Match/CreateStep1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStep1(CreateStep1ViewModel createStep1ViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData[Constants.CreateMatchStrings.CategoryId] = createStep1ViewModel.CategoryId;
                TempData[Constants.CreateMatchStrings.DivisionId] = createStep1ViewModel.DivisionId;
                TempData[Constants.CreateMatchStrings.SeasonId] = createStep1ViewModel.SeasonId;
                TempData[Constants.CreateMatchStrings.IsCupMatch] = createStep1ViewModel.IsCupMatch;
                TempData.Keep();
                
                return RedirectToAction("CreateStep2");
            }
            return View();
        }

        // GET: Match/CreateStep2
        public async Task<IActionResult> CreateStep2(AppData appData)
        {
            var divisionId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.DivisionId).ToString());
            var categoryId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.CategoryId).ToString());
            var seasonId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.SeasonId).ToString());
            var isCupMatch = bool.Parse(TempData.Peek(Constants.CreateMatchStrings.IsCupMatch).ToString());

            var teams = await _context.Teams
                .Where(t => t.Division.Id == divisionId
                    && t.Category.Id == categoryId
                    && t.Season.Id == seasonId)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                .ToListAsync();

            var cups = await _context.Cups.Where(c => c.League.Id == appData.LeagueId)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync();

            var viewlModel = new CreateStep2ViewModel()
            {
                HomeTeams = teams,
                Cups = cups,
                IsCupMatch = bool.Parse(isCupMatch.ToString())
            };

            return View(viewlModel);
        }

        // POST: Match/CreateStep2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStep2(CreateStep2ViewModel createSetp2ViewModel, string btnNext, string btnPrevious)
        {

            if (btnPrevious != null)
            {
                return RedirectToAction("CreateStep1");
            }

            if (ModelState.IsValid)
            {
                TempData[Constants.CreateMatchStrings.HomeTeamId] = createSetp2ViewModel.HomeTeamId;
                TempData[Constants.CreateMatchStrings.HomeTeamHandicap] = createSetp2ViewModel.HomeTeamHandicap;
                TempData[Constants.CreateMatchStrings.StartTime] = createSetp2ViewModel.StartTime;
                TempData[Constants.CreateMatchStrings.ScheduledDate] = createSetp2ViewModel.ScheduledDate;

                return RedirectToAction("CreateStep3");
            }
            return View();
        }

        // GET: Match/CreateStep3
        public async Task<IActionResult> CreateStep3(AppData appData)
        {
            var divisionId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.DivisionId).ToString());
            var categoryId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.CategoryId).ToString());
            var seasonId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.SeasonId).ToString());
            var homeTeamId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.HomeTeamId).ToString());
            var isCupMatch = bool.Parse(TempData.Peek(Constants.CreateMatchStrings.IsCupMatch).ToString());
            TempData.Keep();

            var teams = await _context.Teams
                .Where(t => t.Division.Id == divisionId
                    && t.Category.Id == categoryId
                    && t.Season.Id == seasonId
                    && t.Id != homeTeamId)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
                .ToListAsync();
                       
            var viewlModel = new CreateStep3ViewModel()
            {
                AwayTeams = teams,
                IsCupMatch = isCupMatch
            };

            return View(viewlModel);
        }

        // POST: Match/CreateStep3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStep3(CreateStep3ViewModel createSetp3ViewModel, string btnNext, string btnPrevious)
        {

            if (btnPrevious != null)
            {
                return RedirectToAction("CreateStep2");
            }

            if (ModelState.IsValid)
            {
                TempData[Constants.CreateMatchStrings.AwayTeamId] = createSetp3ViewModel.AwayTeamId;
                TempData[Constants.CreateMatchStrings.AwayTeamHandicap] = createSetp3ViewModel.AwayTeamHandicap;

                return RedirectToAction("Create");
            }
            return View();
        }

        // GET: Match/Create
        public async Task<IActionResult> Create()
        {
            var divisionId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.DivisionId).ToString());
            var categoryId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.CategoryId).ToString());
            var seasonId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.SeasonId).ToString());

            var homeTeamId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.HomeTeamId).ToString());
            var homeTeamHandicap = int.Parse(TempData.Peek(Constants.CreateMatchStrings.HomeTeamHandicap).ToString());

            var awayTeamId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.AwayTeamId).ToString());
            var awayTeamHandicap = int.Parse(TempData.Peek(Constants.CreateMatchStrings.AwayTeamHandicap).ToString());
            
            var isCupMatch = bool.Parse(TempData.Peek(Constants.CreateMatchStrings.IsCupMatch).ToString());

            var startTime = DateTime.Parse(TempData.Peek(Constants.CreateMatchStrings.StartTime).ToString());
            var scheduledDate = DateTime.Parse(TempData.Peek(Constants.CreateMatchStrings.ScheduledDate).ToString());
            TempData.Keep();

            //get entities
            var division = await _context.Divisions.FindAsync(divisionId);
            var category = await _context.Categories.FindAsync(categoryId);
            var season = await _context.Seasons.Include(s => s.League).FirstOrDefaultAsync(s => s.Id == seasonId);
            var homeTeam = await _context.Teams.Include(t => t.LeagueClub).ThenInclude(lc => lc.Club).FirstOrDefaultAsync(t => t.Id == homeTeamId);
            var awayTeam = await _context.Teams.Include(t => t.LeagueClub).ThenInclude(lc => lc.Club).FirstOrDefaultAsync(t => t.Id == awayTeamId);

            var viewModel = new CreateMatchViewModel
            {
                SeasonName = String.Format("{0} > {1}", season.League.Name, season.Name),
                DivisionName = division.Name,
                CategoryName = category.Name,
                HomeTeamName = String.Format("{0} - {1}", homeTeam.LeagueClub.Club.Name, homeTeam.Name),
                AwayTeamName = String.Format("{0} - {1}", awayTeam.LeagueClub.Club.Name, awayTeam.Name),
                StartTime = startTime.ToString("hh:mm"),
                ScheduledDate = scheduledDate.ToString("dd/mm/yyyy"),
                IsCupMatch = isCupMatch

            };

            return View(viewModel);
        }

        // POST: Match/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: Match/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.SingleOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        // POST: Match/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            return View(match);
        }

        // GET: Match/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .SingleOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.SingleOrDefaultAsync(m => m.Id == id);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
    }
}
