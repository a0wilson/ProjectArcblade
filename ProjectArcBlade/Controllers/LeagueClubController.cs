using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;

namespace ProjectArcBlade.Controllers
{
    public class LeagueClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueClubController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: LeagueClub
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeagueClubs
                .Include(lc => lc.Club)
                .Include(lc => lc.League)
                .ToListAsync());
        }

        // GET: LeagueClub/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueClub = await _context.LeagueClubs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (leagueClub == null)
            {
                return NotFound();
            }

            return View(leagueClub);
        }

        // GET: LeagueClub/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeagueClub/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] LeagueClub leagueClub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leagueClub);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(leagueClub);
        }

        // GET: LeagueClub/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueClub = await _context.LeagueClubs.SingleOrDefaultAsync(m => m.Id == id);
            if (leagueClub == null)
            {
                return NotFound();
            }
            return View(leagueClub);
        }

        // POST: LeagueClub/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] LeagueClub leagueClub)
        {
            if (id != leagueClub.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leagueClub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueClubExists(leagueClub.Id))
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
            return View(leagueClub);
        }

        // GET: LeagueClub/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueClub = await _context.LeagueClubs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (leagueClub == null)
            {
                return NotFound();
            }

            return View(leagueClub);
        }

        // POST: LeagueClub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leagueClub = await _context.LeagueClubs.SingleOrDefaultAsync(m => m.Id == id);
            _context.LeagueClubs.Remove(leagueClub);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LeagueClubExists(int id)
        {
            return _context.LeagueClubs.Any(e => e.Id == id);
        }
    }
}
