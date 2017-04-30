using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectArcBlade.Models;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class MatchSchedulingService
    {
        private ApplicationDbContext _context;
        private List<DateTime> _availableDates;
        private List<ExclusionDate> _exclusionDates;
        private List<MatchSchedule> _matchSchedules;
        private List<Team> _teams;
        private List<ClubVenue> _clubVenues;
        private Season _season;

        public List<MatchSchedule> ScheduleMatches (ApplicationDbContext context, int seasonId)
        {
            //create a random number generator.
            var rng = new Random();

            _context = context;

            _matchSchedules = new List<MatchSchedule>();
            
            //get the season we are working with.
            _season = _context.Seasons
                .Include(s => s.League)
                .Where(s => s.Id == seasonId).Single();

            //get all exclusion dates entered for this season.
            _exclusionDates = _context.ExclusionDates
                .Include(ed => ed.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(ed => ed.Season)
                .Where(ed => ed.Season.Id == seasonId).ToList();
            
            //get all teams entered for this season - with complete teams.
            _teams = _context.Teams
                .Include(t => t.Season)
                .Include(t => t.Category)
                .Include(t => t.Division)
                .Include(t => t.TeamStatus)
                .Include(t => t.LeagueClub).ThenInclude(lc=>lc.League)
                .Include(t => t.LeagueClub).ThenInclude(lc=>lc.Club).ThenInclude(c=>c.ClubVenues)
                .Where(t=> t.Season.Id == seasonId)
                //.Where(t => t.TeamStatus.Id == Constants.TeamStatus.Complete
                //    && t.Season.Id == seasonId)
                .ToList();

            _clubVenues = _context.ClubVenues
                .Include(cv => cv.DayOfTheWeek)
                .Include(cv => cv.Club)
                .Include(cv => cv.Venue)
                .ToList();

            //get any dates which have been excluded globally this season. e.g. bank holidays.            
            var globalExclusionDates = _exclusionDates.Where(ed => ed.LeagueClub == null).Select(ed => ed.DateToExclude).ToList();
            _availableDates = new List<DateTime>();
            for (var date = _season.StartDate; date <= _season.EndDate; date = date.AddDays(1))
            {
                if(!globalExclusionDates.Contains(date)) _availableDates.Add(date);
            }

            //determine how many divisions there are.
            var divisionIds = _teams.GroupBy(t => t.Division.Id).Select(t => t.First()).Select(t => t.Division.Id).ToList();
            var categoryIds = _teams.GroupBy(t => t.Category.Id).Select(t => t.First()).Select(t => t.Category.Id).ToList();
            
            //loop through each division.
            for(var d=0; d<divisionIds.Count(); d++)
            {
                for( var c=0; c<categoryIds.Count(); c++)
                {
                    List<int> homeTeamIds, awayTeamIds;
                    homeTeamIds = awayTeamIds = _teams
                        .Where(t => t.Division.Id == divisionIds[d] && t.Category.Id == categoryIds[c])
                        .Select(t => t.Id).ToList();
                    
                    if (homeTeamIds.Count() == 0) break; // if no teams returned then break the loop.

                    //handle instances of 1 team being returned.
                    //if (homeTeamIds.Count() == 1)
                    //{
                    //    var matchSchedule = new MatchSchedule { HomeTeamId = -1, AwayTeamId = -1, ScheduledDate = null };
                    //    matchSchedule.Range = Constants.MatchScheduleRange.Initial; //initial match.
                    //    matchSchedule.HomeTeamId = homeTeamIds[0]; //store the single team id;
                    //    _matchSchedules.Add(matchSchedule);
                    //    break;
                    //}

                    //only  attempt to schedule matches when there is more than 1 team in the division.
                    if (homeTeamIds.Count() > 0)
                    {
                        //loop through each home team
                        for (var h = 0; h < homeTeamIds.Count(); h++)
                        {
                            //take a subset of awayTeams which excludes the current homeTeamId
                            var filteredAwayTeamsIds = awayTeamIds.Where(id => id != homeTeamIds[h]).ToList();
                            
                            if (filteredAwayTeamsIds.Count() > 0)
                            {
                                //select a random awayTeam to play against the current homeTeam.
                                var awayGamesAvailable = true;
                                while (awayGamesAvailable)
                                {
                                    var f = rng.Next(0, (filteredAwayTeamsIds.Count())); //get a random team id to play.
                                    var matchSchedule = new MatchSchedule
                                    {
                                        HomeTeamId = -1,
                                        AwayTeamId = -1,
                                        ScheduledDate = null,
                                        Range = Constants.MatchScheduleRange.Initial
                                    };

                                    matchSchedule.HomeTeamId = homeTeamIds[h]; //store homeTeamId
                                    matchSchedule.HomeClubId = _teams.Where(t => t.Id == matchSchedule.HomeTeamId).Select(t => t.LeagueClub.Club.Id).Single(); //set homeTeamClubId            
                                    matchSchedule.AwayTeamId = filteredAwayTeamsIds[f]; //store awayTeamId                            

                                    //check if these teams have played before - if they have set the match to be a return match.
                                    if(_matchSchedules.Where(ms=>ms.AwayTeamId == matchSchedule.HomeTeamId && ms.HomeTeamId == matchSchedule.AwayTeamId).Any())
                                    {
                                        matchSchedule.Range = Constants.MatchScheduleRange.Return; //return match.
                                    }
                                    
                                    filteredAwayTeamsIds.RemoveAt(f); //remove entry from awayTeamIds
                                    _matchSchedules.Add(matchSchedule); //add match to the list 

                                    if (filteredAwayTeamsIds.Count() == 0) awayGamesAvailable = false;
                                }
                            }
                        }
                    }
                }
            }
            
            //perform 2 passes when trying to schedule matches... 
            //the second pass is only performed if unable to find a match date on the first pass.
            //the second pass searches the whole season for a suitable date range.
            //the first pass will try to schedule initial matches in the first half of the season and return matches in the second half.
            for( var pass=1; pass<3; pass++)
            {
                if (pass == 2 && !(_matchSchedules.Any(ms => ms.ScheduledDate == null && ms.HomeTeamId > 0 && ms.AwayTeamId > 0))) break;
                    
                //get dates for each match - depending on range. if hometeamdid and awayteamid are populated.
                foreach (MatchSchedule ms in _matchSchedules)
                {
                    if (ms.HomeTeamId > 0 && ms.AwayTeamId > 0)
                    {
                        ms.ScheduledDate = pass==1 ? GetMatchDate(ms.HomeTeamId, ms.AwayTeamId, ms.Range) : GetMatchDate(ms.HomeTeamId, ms.AwayTeamId, Constants.MatchScheduleRange.Any);
                        if (ms.ScheduledDate != null)
                        {
                            ms.VenueId = GetClubVenueId(ms.HomeTeamId, Convert.ToDateTime(ms.ScheduledDate));
                            ms.StartTime = GetClubVenueStartTime(ms.HomeTeamId, Convert.ToDateTime(ms.ScheduledDate));
                        }
                    }
                }
            }

            //Finally... create/update the matches!
            foreach (MatchSchedule ms in _matchSchedules) CreateMatch(ms);
            
            return _matchSchedules;
        }
        
        private DateTime? GetMatchDate(int homeTeamId, int awayTeamId, Constants.MatchScheduleRange range )
        {
            //get the home match available days.
            var homeTeamClubId = _teams.Where(t => t.Id == homeTeamId).Select(t => t.LeagueClub.Club.Id).Single(); //get homeTeamClubId            
            var homeTeamClubVenues = _clubVenues.Where(cv => cv.Club.Id == homeTeamClubId).ToList(); //get home team club venues
            var homeTeamMaxMatches = _clubVenues.Where(cv => cv.Club.Id == homeTeamClubId).Max(cv => cv.MaxMatches); //get max matches for home team
            var maxMatches = _clubVenues.Max(cv => cv.MaxMatches); //get max matches across all teams.
            var homeTeamExclusionDates = _exclusionDates.Where(ed => ed.LeagueClub.Club.Id == homeTeamClubId).Select(ed => ed.DateToExclude).ToList();

            //1 pass loops through all available dates looking for a match.
            //the max passes is the most amount of matches that can be played at a single venue in one day.
            for (var pass = 0; pass < maxMatches; pass++)
            {
                //skip the iteration is there is no chance of finding a date.
                if (homeTeamMaxMatches < (pass + 1)) continue;

                //loop through available dates - dependent on range.
                int startIndex = 0;
                int endIndex = _availableDates.Count();
                if (range == Constants.MatchScheduleRange.Return) startIndex = _availableDates.Count() / 2;
                if (range == Constants.MatchScheduleRange.Initial) endIndex = _availableDates.Count() / 2;
                

                for (var a = startIndex; a < endIndex; a++)
                {
                    //if the current date is one of the days the club plays...
                    var availableDay = _availableDates[a].DayOfWeek.ToString().ToLower();
                    foreach (ClubVenue cv in homeTeamClubVenues)
                    {
                        if (cv.DayOfTheWeek.Name.ToLower().Equals(availableDay))
                        {
                            //check the club has not excluded this date.
                            if(!homeTeamExclusionDates.Contains(_availableDates[a]))
                            {
                                //check if there is currently a game being played there on this date.
                                var matchCount = _matchSchedules.Where(ms => ms.ScheduledDate == _availableDates[a] && ms.HomeClubId == homeTeamClubId).Count();
                                if (matchCount == pass)
                                {
                                    return _availableDates[a]; //return match date
                                }
                            }                            
                        }
                    }
                }
            }
            return null;
        }

        private int GetClubVenueId(int teamId, DateTime date)
        {
            var day = date.DayOfWeek.ToString().ToLower();
            var clubId = _teams.Where(t => t.Id == teamId).Select(t => t.LeagueClub.Club.Id).Single();
            return _clubVenues.Where(cv => cv.Club.Id == clubId && cv.DayOfTheWeek.Name.ToLower() == day).Select(cv => cv.Venue.Id).Single();
        }

        private DateTime GetClubVenueStartTime(int teamId, DateTime date)
        {
            var day = date.DayOfWeek.ToString().ToLower();
            var clubId = _teams.Where(t => t.Id == teamId).Select(t => t.LeagueClub.Club.Id).Single();
            return _clubVenues.Where(cv => cv.Club.Id == clubId && cv.DayOfTheWeek.Name.ToLower() == day).Select(cv => cv.StartTime).Single();
        }

        private bool CreateMatch(MatchSchedule match)
        {
            //only create/update match if home and away team are populated and a match date has been found.
            if (match.AwayTeamId > 0 && match.HomeTeamId > 0 )
            {
                //check if this match already exists.
                var matchExists = _context.Matches
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team)
                .Where(m => m.HomeMatchTeam.Team.Id == match.HomeTeamId
                    && m.AwayMatchTeam.Team.Id == match.AwayTeamId
                    && m.Season.Id == _season.Id).Any();

                if (matchExists)
                {
                    UpdateMatch(match);
                }
                else
                {

                    var matchType = _context.MatchTypes.Find(Constants.MatchType.League);
                    var season = _context.Seasons.Find(_season.Id);
                    var venue = _context.Venues.Find(match.VenueId);

                    var newLeagueMatch = new Match
                    {
                        MatchType = matchType,
                        Season = season,
                        Venue = venue,
                        StartDate = Convert.ToDateTime(match.ScheduledDate),
                        StartTime = match.StartTime
                    };
                    _context.Matches.Add(newLeagueMatch);
                    //_context.SaveChanges();

                    var homeMatchTeam = new HomeMatchTeam
                    {
                        Match = newLeagueMatch,
                        ResultType = _context.ResultTypes.Find(Constants.ResultType.NoEntry),
                        Team = _context.Teams.Find(match.HomeTeamId)
                    };
                    _context.HomeMatchTeams.Add(homeMatchTeam);
                    //_context.SaveChanges();

                    var awayMatchTeam = new AwayMatchTeam
                    {
                        Match = newLeagueMatch,
                        ResultType = _context.ResultTypes.Find(Constants.ResultType.NoEntry),
                        Team = _context.Teams.Find(match.AwayTeamId)
                    };
                    _context.AwayMatchTeams.Add(awayMatchTeam);
                    _context.SaveChanges();

                }
            }
            return true;
        }

        private bool UpdateMatch(MatchSchedule match)
        {
            var updateMatch = _context.Matches
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.Team)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.Team)
                .Include(m => m.HomeMatchTeam).ThenInclude(hmt => hmt.ResultType)
                .Include(m => m.AwayMatchTeam).ThenInclude(amt => amt.ResultType)
                .Where(m => m.HomeMatchTeam.Team.Id == match.HomeTeamId
                    && m.AwayMatchTeam.Team.Id == match.AwayTeamId
                    && m.Season.Id == _season.Id).Single();

            //only update match if it not yet started.
            if( updateMatch.AwayMatchTeam.ResultType.Id == Constants.ResultType.NoEntry 
                    && updateMatch.HomeMatchTeam.ResultType.Id == Constants.ResultType.NoEntry )
            {
                updateMatch.Venue = _context.Venues.Find(match.VenueId);
                updateMatch.StartDate = Convert.ToDateTime(match.ScheduledDate);
                updateMatch.StartTime = match.StartTime;

                _context.Matches.Update(updateMatch);
                _context.SaveChanges();
            }
            
            return true;
        }
    }
}
