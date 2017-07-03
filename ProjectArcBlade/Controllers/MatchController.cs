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
           
            //    .ToListAsync();
            //foreach(Match m in matches)
            //{
            //    var homeTeam = await _context.HomeMatchTeams.Include(hmt=>hmt.Team).Where(hmt => hmt.Match.Id == m.Id).SingleAsync();
            //}

            return View(await _context.Matches
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t=>t.LeagueClub).ThenInclude(lc=>lc.Club)
                .Include(m => m.AwayMatchTeam).ThenInclude(hmt => hmt.Team).ThenInclude(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Select(m => new DisplayMatchViewModel
                {
                    Id = m.Id,
                    StartDate = Convert.ToDateTime(m.StartDate).ToString(Constants.DateFormat.Long),
                    StartTime = Convert.ToDateTime(m.StartTime).ToString(Constants.TimeFormat.Short),
                    HomeTeam = String.Format("{0} - {1}", m.HomeMatchTeam.Team.LeagueClub.Club.Name, m.HomeMatchTeam.Team.Name),
                    AwayTeam = String.Format("{0} - {1}", m.AwayMatchTeam.Team.LeagueClub.Club.Name, m.AwayMatchTeam.Team.Name)
                        
                })
                .ToListAsync());
            //.Include(m=>m.HomeMatchTeams)

            //.Include(m=>m.AwayMatchTeams).ToListAsync());
        }

        // GET: Match/Details/5
        public async Task<IActionResult> Details(MatchService matchService, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .SingleOrDefaultAsync(m => m.Id == id);

            //await matchService.CreateMatchGamesByGroupAsync(_context, match.Id);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        //GET: Match/Preview/5
        public async Task<IActionResult> Preview(MatchService matchService, int matchId, int teamId)
        {
            var viewModel = await matchService.GetPreviewMatchViewModelAsync(_context, matchId, teamId);
            return View(viewModel);
        }

        //POST: Match/Preview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Preview(MatchService matchService, PreviewMatchViewModel viewModel, string btnBack, string btnConcede, string btnTeam, string btnPlay)
        {
            if(btnBack != null)
            {
                return RedirectToAction("Dashboard", "Team", new { id = viewModel.TeamId });
            }

            if(btnTeam != null)
            {
                return RedirectToAction("ManageMatchTeam", "Team", new { matchTeamId = viewModel.MatchTeamId, teamType = viewModel.TeamType });
            }

            if(btnPlay != null)
            {
                await matchService.StartMatch(_context, viewModel.MatchId);
                return RedirectToAction("MatchProgress", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId });
            }

            if(btnConcede != null)
            {
                var serviceResult = await matchService.ConcedeMatchAsync(_context, viewModel.MatchId, viewModel.TeamId);
                if (serviceResult.Success)
                {
                    return RedirectToAction("Dashboard", "Team", new { id = viewModel.TeamId });
                }
            }

            return View(viewModel);
        }
        
        //GET: Match/GameProgress
        public async Task<IActionResult> GameProgress(MatchService matchService, int setId, int teamId)
        {
            var viewModel = await matchService.GetGameProgressViewModelAsync(_context, setId, teamId);

            if (viewModel.Set.AwayResult == Constants.ResultType.Conceded || viewModel.Set.HomeResult == Constants.ResultType.Conceded)
            {
                return View("ViewGameProgress", viewModel);
            }

            return View(viewModel);
        }

        //POST: Match/GameProgress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GameProgress(MatchService matchService, GameProgressViewModel viewModel, string btnSave, string btnBack, string btnConcede)
        {
            if (btnSave != null)
            {
                var serviceResult = await matchService.UpdateSetAsync(_context, viewModel);
                if (serviceResult.Success)
                {
                    TempData["successMessage"] = "Game Progress updated successfully!";
                    //if the score update means a pair has won the set then redirect to the match progress page.
                    if( serviceResult.ReturnValue.Set.HomeResult == Constants.ResultType.Win || serviceResult.ReturnValue.Set.AwayResult == Constants.ResultType.Win)
                    {
                        return RedirectToAction("MatchProgress", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId });
                    }
                }
            }
                
            if( btnBack != null) return RedirectToAction("MatchProgress", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId });
            
            if(btnConcede != null)
            {
                var serviceResult = await matchService.ConcedeGameAsync(_context, viewModel);

                if (serviceResult.Success)
                {
                    if (serviceResult.HasSuccessMessage) TempData["successMessage"] = serviceResult.SuccessMessage;
                    return RedirectToAction("MatchProgress", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId });
                }
                
            }

            return RedirectToAction("GameProgress", new { setId = viewModel.Set.Id, teamId = viewModel.TeamId });
        }

        // GET: Match/MatchProgress
        public async Task<IActionResult> MatchProgress(MatchService matchService, int matchId, int teamId)
        {
            var viewModel = await matchService.GetMatchProgressViewModelAsync(_context, matchId, teamId);
            return View(viewModel);
        }

        //POST: Match/MatchProgress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MatchProgress(MatchService matchService, MatchProgressViewModel viewModel, string btnBack, string btnConcede, string btnSummary)
        {
            if(btnBack != null) return RedirectToAction("Dashboard", "Team", new { id = viewModel.TeamId });

            if (btnSummary != null) return RedirectToAction("MatchSummary", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId, matchTeamId = viewModel.MatchTeamId });

            if (btnConcede != null)
            {
                var serviceResult = await matchService.ConcedeMatchAsync(_context, viewModel.MatchId, viewModel.TeamId);
                return RedirectToAction("Dashboard", "Team", new { id = viewModel.TeamId });
            }

            return RedirectToAction("MatchProgress", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId });
        }

        // GET: Match/MatchSummary
        public async Task<IActionResult> MatchSummary(MatchService matchService, int matchId, int teamId, int matchTeamId)
        {
            var serviceResult = await matchService.GetMatchSummaryViewModelAsync(_context, matchId, teamId);
            var viewModel = serviceResult.ReturnValue;
            return View(viewModel);
        }

        //POST: Match/MatchSummary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MatchSummary(MatchService matchService, MatchSummaryViewModel viewModel, string btnBack, string btnComplete)
        {
            if (btnBack != null) return RedirectToAction("MatchProgress", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId });

            if (btnComplete != null)
            {
                var serviceResult = await matchService.CompleteMatchAsync(_context, viewModel);
                return RedirectToAction("Dashboard", "Team", new { id = viewModel.TeamId });
            }

            return RedirectToAction("MatchSummary", new { matchId = viewModel.MatchId, teamId = viewModel.TeamId, matchTeamId = viewModel.MatchTeamId });

        }

        // GET: Match/CreateStep1
        public async Task<IActionResult> CreateStep1(int leagueId)
        {
            var matchTypes = await _context.MatchTypes
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToListAsync();

            var seasons = await _context.Seasons
                .Where(s => s.IsActive == true && s.League.Id == leagueId)
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
                Categories = categories,
                MatchTypes = matchTypes,
                LeagueId = leagueId
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
                TempData[Constants.CreateMatchStrings.LeagueId] = createStep1ViewModel.LeagueId;
                TempData[Constants.CreateMatchStrings.CategoryId] = createStep1ViewModel.CategoryId;
                TempData[Constants.CreateMatchStrings.DivisionId] = createStep1ViewModel.DivisionId;
                TempData[Constants.CreateMatchStrings.SeasonId] = createStep1ViewModel.SeasonId;
                TempData[Constants.CreateMatchStrings.MatchTypeId] = createStep1ViewModel.MatchTypeId;
                //TempData[Constants.CreateMatchStrings.IsCupMatch] = createStep1ViewModel.MatchTypeId == Constants.MatchType.Cup;
                TempData.Keep();
                
                return RedirectToAction("CreateStep2");
            }
            return View();
        }

        // GET: Match/CreateStep2
        public async Task<IActionResult> CreateStep2()
        {
            var leagueId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.LeagueId).ToString());
            var divisionId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.DivisionId).ToString());
            var categoryId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.CategoryId).ToString());
            var seasonId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.SeasonId).ToString());
            var isCupMatch = bool.Parse(TempData.Peek(Constants.CreateMatchStrings.IsCupMatch).ToString());

            var teams = await _context.Teams
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(t => t.Division.Id == divisionId
                    && t.Category.Id == categoryId
                    && t.Season.Id == seasonId)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = String.Format("{0} - {1}", t.LeagueClub.Club.Name, t.Name) })
                .ToListAsync();

            var cups = await _context.Cups.Where(c => c.League.Id == leagueId)
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
                .Include(t => t.LeagueClub).ThenInclude(lc => lc.Club)
                .Where(t => t.Division.Id == divisionId
                    && t.Category.Id == categoryId
                    && t.Season.Id == seasonId
                    && t.Id != homeTeamId)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = String.Format("{0} - {1}", t.LeagueClub.Club.Name, t.Name) })
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
            var matchTypeId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.MatchTypeId).ToString());
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
            var matchType = await _context.MatchTypes.FindAsync(matchTypeId);
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
                StartTime = startTime.ToString(Constants.TimeFormat.Short),
                ScheduledDate = scheduledDate.ToString(Constants.DateFormat.Long),
                MatchType = matchType.Name,
                IsCupMatch = matchType.Name == Constants.MatchType.Cup
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string btnPrevious)
        {
            if (btnPrevious == null)
            {
                var matchTypeId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.MatchTypeId).ToString());
                var seasonId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.SeasonId).ToString());

                var homeTeamId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.HomeTeamId).ToString());
                var homeTeamHandicap = int.Parse(TempData.Peek(Constants.CreateMatchStrings.HomeTeamHandicap).ToString());

                var awayTeamId = int.Parse(TempData.Peek(Constants.CreateMatchStrings.AwayTeamId).ToString());
                var awayTeamHandicap = int.Parse(TempData.Peek(Constants.CreateMatchStrings.AwayTeamHandicap).ToString());

                var startTime = DateTime.Parse(TempData.Peek(Constants.CreateMatchStrings.StartTime).ToString());
                var scheduledDate = DateTime.Parse(TempData.Peek(Constants.CreateMatchStrings.ScheduledDate).ToString());

                var isCupMatch = bool.Parse(TempData.Peek(Constants.CreateMatchStrings.IsCupMatch).ToString());

                //get entities
                var matchType = await _context.MatchTypes.FindAsync(matchTypeId);
                var season = await _context.Seasons.FindAsync(seasonId);
                var homeTeam = await _context.Teams.FindAsync(homeTeamId);
                var awayTeam = await _context.Teams.FindAsync(awayTeamId);

                //create match
                var newMatch = new Match
                {
                    Season = season,
                    StartTime = startTime,
                    StartDate = scheduledDate,
                    MatchType = matchType
                };
                _context.Matches.Add(newMatch);
                await _context.SaveChangesAsync();

                //create home match team entry
                var newHomeMatchTeam = new HomeMatchTeam
                {
                    Match = newMatch,
                    Team = homeTeam,
                };
                _context.HomeMatchTeams.Add(newHomeMatchTeam);
                await _context.SaveChangesAsync();

                //create away match team entry
                var newAwayMatchTeam = new AwayMatchTeam
                {
                    Match = newMatch,
                    Team = awayTeam,
                };
                _context.AwayMatchTeams.Add(newAwayMatchTeam);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("CreateStep3");
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
