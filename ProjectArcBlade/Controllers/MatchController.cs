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
        public IActionResult CreateStep1(MatchAppData matchAppData, CreateStep1ViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateStep2");
            }
            return View();
        }

        // GET: Match/CreateStep2
        public async Task<IActionResult> CreateStep2(AppData appData, MatchAppData matchAppData)
        {
            var teams = await _context.Teams
                .Where(t => t.Division.Id == matchAppData.DivisionId 
                    && t.Category.Id == matchAppData.CategoryId
                    && t.Season.Id == matchAppData.SeasonId)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToListAsync();

            var cups = await _context.Cups.Where(c => c.League.Id == appData.LeagueId)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync();

            var viewlModel = new CreateStep2ViewModel()
            {
                HomeTeams = teams,
                Cups = cups
            };

            return View(viewlModel);
        }

        // POST: Match/CreateStep2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStep2(MatchAppData matchAppData, CreateStep2ViewModel viewModel, string btnNext, string btnPrevious)
        {
            if (ModelState.IsValid)
            {
                if(btnNext != null)
                {
                    return RedirectToAction("CreateStep3");
                }

                if( btnPrevious == null)
                {
                    return RedirectToAction("CreateStep1");
                }
                
            }
            return View();
        }

        // GET: Match/Create
        public IActionResult Create()
        {

            return View();
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
