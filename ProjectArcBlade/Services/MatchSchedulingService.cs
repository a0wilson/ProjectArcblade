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

            _matchSchedules = new List<MatchSchedule>();
            
            //get the season we are working with.
            _season = context.Seasons
                .Include(s => s.League)
                .Where(s => s.Id == seasonId).Single();

            //get all exclusion dates entered for this season.
            _exclusionDates = context.ExclusionDates
                .Include(ed => ed.LeagueClub).ThenInclude(lc => lc.Club)
                .Include(ed => ed.Season)
                .Where(ed => ed.Season.Id == seasonId).ToList();
            
            //get all teams entered for this season - with complete teams.
            _teams = context.Teams
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

            _clubVenues = context.ClubVenues
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
                    if (homeTeamIds.Count() == 1)
                    {
                        var matchSchedule = new MatchSchedule { HomeTeamId = -1, AwayTeamId = -1, ScheduledDate = null };
                        matchSchedule.Range = Constants.MatchScheduleRange.Initial; //initial match.
                        matchSchedule.HomeTeamId = homeTeamIds[0]; //store the single team id;
                        matchSchedule.DivisionName = _teams.Where(t => t.Id == matchSchedule.HomeTeamId).Select(t => t.Division.Name).Single();
                        _matchSchedules.Add(matchSchedule);
                        break;
                    }

                    //only  attempt to schedule matches when there is more than 1 team in the division.
                    if (homeTeamIds.Count() > 0)
                    {
                        //loop through each home team
                        for (var h = 0; h < homeTeamIds.Count(); h++)
                        {
                            var matchSchedule = new MatchSchedule { HomeTeamId = -1, AwayTeamId = -1, ScheduledDate = null };
                            matchSchedule.Range = Constants.MatchScheduleRange.Initial; //initial match.
                            matchSchedule.HomeTeamId = homeTeamIds[h]; //store homeTeamId
                            matchSchedule.HomeClubId = _teams.Where(t => t.Id == matchSchedule.HomeTeamId).Select(t => t.LeagueClub.Club.Id).Single(); //set homeTeamClubId            
                            matchSchedule.DivisionName = _teams.Where(t => t.Id == matchSchedule.HomeTeamId).Select(t => t.Division.Name).Single();

                            //select a random awayTeam to play against the current homeTeam.
                            var rngFail = true;
                            while (rngFail)
                            {
                                var a = rng.Next(0, (awayTeamIds.Count()));
                                if (homeTeamIds[h] != awayTeamIds[a])
                                {
                                    matchSchedule.AwayTeamId = awayTeamIds[a]; //store awayTeamId                            
                                    awayTeamIds.RemoveAt(a); //remove entry from awayTeamIds
                                    rngFail = false;
                                }
                            }

                            _matchSchedules.Add(matchSchedule);
                        }
                    }
                }
            }
            
            //setup all return matches.
            var returnMatches = new List<MatchSchedule>();
            foreach(MatchSchedule ms in _matchSchedules)
            {
                //only arrange return if home and away are > 1
                if (ms.HomeTeamId > 0 && ms.AwayTeamId > 0)
                {
                    var returnMatch = new MatchSchedule();
                    returnMatch.HomeTeamId = ms.AwayTeamId;
                    returnMatch.HomeClubId = _teams.Where(t => t.Id == returnMatch.HomeTeamId).Select(t => t.LeagueClub.Club.Id).Single(); //set homeTeamClubId
                    returnMatch.AwayTeamId = ms.HomeTeamId;
                    returnMatch.DivisionName = ms.DivisionName;
                    returnMatch.Range = Constants.MatchScheduleRange.Return;
                    returnMatches.Add(returnMatch);
                }                
            }
                        
            //include the return matches in the matchSchedules
            foreach (MatchSchedule rm in returnMatches) _matchSchedules.Add(rm);

            //update the teamname display.
            foreach(MatchSchedule ms in _matchSchedules)
            {
                if( ms.AwayTeamId > 0)
                {
                    ms.AwayTeamName = _teams
                        .Where(t => t.Id == ms.AwayTeamId)
                        .Select(t => t.LeagueClub.Club.Name + " - " + t.Name + " (" + t.TeamStatus.Name + ")").Single();
                }

                if( ms.HomeTeamId > 0)
                {
                    ms.HomeTeamName = _teams
                        .Where(t => t.Id == ms.HomeTeamId)
                        .Select(t => t.LeagueClub.Club.Name + " - " + t.Name + " (" + t.TeamStatus.Name + ")").Single();
                }
            }

            //get dates for each match - depending on range. if hometeamdid and awayteamid are populated.
            foreach (MatchSchedule ms in _matchSchedules)
            {
                if( ms.HomeTeamId > 0 && ms.AwayTeamId > 0)
                {
                    ms.ScheduledDate = GetMatchDate(ms.HomeTeamId, ms.AwayTeamId, ms.Range);
                    ms.DisplayDate = Convert.ToDateTime(ms.ScheduledDate).ToString(Constants.DateFormat.Long);
                    ms.VenueName = GetClubVenueName(ms.HomeTeamId, Convert.ToDateTime(ms.ScheduledDate));
                }
            }

            //if there are still null dates then try once more but this time use the entire season as a date range.
            if(_matchSchedules.Any(ms => ms.ScheduledDate == null && ms.HomeTeamId > 0 && ms.AwayTeamId > 0))
            {
                foreach (MatchSchedule ms in _matchSchedules)
                {
                    ms.ScheduledDate = GetMatchDate(ms.HomeTeamId, ms.AwayTeamId, Constants.MatchScheduleRange.Any);
                    ms.DisplayDate = Convert.ToDateTime(ms.ScheduledDate).ToString(Constants.DateFormat.Long);
                    ms.VenueName = GetClubVenueName(ms.HomeTeamId, Convert.ToDateTime(ms.ScheduledDate));
                }
            }

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

        private string GetClubVenueName(int teamId, DateTime date)
        {
            var day = date.DayOfWeek.ToString().ToLower();
            var clubId = _teams.Where(t => t.Id == teamId).Select(t => t.LeagueClub.Club.Id).Single();
            return _clubVenues.Where(cv => cv.Club.Id == clubId && cv.DayOfTheWeek.Name.ToLower() == day).Select(cv => cv.Venue.Name).Single();
        }
    }
}
