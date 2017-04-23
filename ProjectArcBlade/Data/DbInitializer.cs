using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectArcBlade.Models;

namespace ProjectArcBlade.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.MatchTypes.Any())
            {
                var matchTypes = new MatchType[]
                {
                    new MatchType{Name="League"},
                    new MatchType{Name="Cup"},
                    new MatchType{Name="Casual"}
                };
                foreach (MatchType mt in matchTypes) context.MatchTypes.Add(mt);
                context.SaveChanges();
            }

            if (!context.Sports.Any())
            {
                var sports = new Sport[]
                {
                    new Sport{Name="Badminton"},
                    new Sport{Name="Squash"},
                    new Sport{Name="Tennis"},
                    new Sport{Name="Badminton"},
                };
                foreach (Sport s in sports) context.Sports.Add(s);
                context.SaveChanges();                
            }
            
            if(!context.DaysOfTheWeek.Any())
            {
                var days = new DayOfTheWeek[]
                {
                    new DayOfTheWeek{Name="Monday"},
                    new DayOfTheWeek{Name="Tuesday"},
                    new DayOfTheWeek{Name="Wednesday"},
                    new DayOfTheWeek{Name="Thursday"},
                    new DayOfTheWeek{Name="Friday"},
                    new DayOfTheWeek{Name="Saturday"},
                    new DayOfTheWeek{Name="Sunday"},
                };
                foreach (DayOfTheWeek d in days) context.Add(d);
                context.SaveChanges();
            }
            
            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category{Name="Mens"},
                    new Category{Name="Womens"},
                    new Category{Name="Mixed"}
                };
                foreach (Category c in categories) context.Categories.Add(c);
                context.SaveChanges();
            }

            if (!context.Genders.Any())
            {
                var genders = new Gender[]
                {
                    new Gender{Name="Male"},
                    new Gender{Name="Female"},
                    new Gender{Name="Other"}
                };
                foreach (Gender g in genders) context.Genders.Add(g);
                context.SaveChanges();
            }

            if (!context.MembershipTypes.Any())
            {
                var membershipTypes = new MembershipType[]
                {
                    new MembershipType{Name="Full"},
                    new MembershipType{Name="Casual"}
                };
                foreach (MembershipType mt in membershipTypes) context.MembershipTypes.Add(mt);
                context.SaveChanges();
            }

            if (!context.TeamStatuses.Any())
            {
                var teamStatuses = new TeamStatus[]
                {
                    new TeamStatus{Name="New"},
                    new TeamStatus{Name="In Progress"},
                    new TeamStatus{Name="Complete"},
                    new TeamStatus{Name="Active"},
                    new TeamStatus{Name="Inactive"}
                };
                foreach (TeamStatus ts in teamStatuses) context.TeamStatuses.Add(ts);
                context.SaveChanges();
            }

            if (!context.Divisions.Any())
            {
                var divisions = new Division[]
                {
                    new Division{Name="Division One"},
                    new Division{Name="Division Two"},
                    new Division{Name="Division Three"},
                    new Division{Name="Division Four"},
                    new Division{Name="Division Five"},
                    new Division{Name="Division Six"},
                    new Division{Name="Division Seven"},
                    new Division{Name="Division Eight"},
                    new Division{Name="Division Nine"},
                    new Division{Name="Division Ten"},
                    new Division{Name="Division Eleven"},
                    new Division{Name="Division Twelve"}
                };
                foreach (Division d in divisions) context.Divisions.Add(d);
                context.SaveChanges();
            }

            if (!context.ClubUserStatuses.Any())
            {
                var clubUserStatuses = new ClubUserStatus[]
                {
                    new ClubUserStatus{Name="Available"},
                    new ClubUserStatus{Name="Limited availability"},
                    new ClubUserStatus{Name="Injured"},
                    new ClubUserStatus{Name="Sick"},
                    new ClubUserStatus{Name="Unavailable"},
                };
                foreach (ClubUserStatus cus in clubUserStatuses) context.ClubUserStatuses.Add(cus);
                context.SaveChanges();
            }

            if (!context.Groups.Any())
            {
                var groups = new Group[]
                {
                    new Group{Name="1"},
                    new Group{Name="2"},
                    new Group{Name="3"},
                    new Group{Name="4"},
                    new Group{Name="5"},
                    new Group{Name="6"},                
                };
                foreach (Group g in groups) context.Groups.Add(g);
                context.SaveChanges();
            }

            if (!context.ScoreStatuses.Any())
            {
                var scoreStatuses = new ScoreStatus[]
                {
                    new ScoreStatus{Name="No Entry"},
                    new ScoreStatus{Name="Accepted"},
                    new ScoreStatus{Name="Contested"}
                };
                foreach (ScoreStatus ss in scoreStatuses) context.ScoreStatuses.Add(ss);
                context.SaveChanges();
            }
            
            if (!context.ResultTypes.Any())
            {
                var resultTpyes = new ResultType[]
                {
                    new ResultType{Name="No Entry"},
                    new ResultType{Name="Win"},
                    new ResultType{Name="Loss"},
                    new ResultType{Name="Draw"},
                    new ResultType{Name="Forfeit"},
                    new ResultType{Name="Pending"},
                    new ResultType{Name="Conceded"}
                };
                foreach (ResultType rt in resultTpyes) context.ResultTypes.Add(rt);
                context.SaveChanges();
            }

            if (!context.Venues.Any())
            {
                var venues = new Venue[]
                {
                    new Venue{Name="Sport Wales National Centre", AddressLine1="Sophia Close",Postcode="CF11 9SW" },
                    new Venue{Name="Llantrisant Leisure Centre", AddressLine1="Llantrisant", Postcode="CF72 8DJ"},
                    new Venue{Name="Western Leisure Centre", AddressLine1="Caerau Lane", Postcode="CF5 5HJ"},
                    new Venue{Name="Holm View Leisure Centre", AddressLine1="Skomer Road", Postcode="CF62 9DA"}
                };
                foreach (Venue v in venues) context.Venues.Add(v);
                context.SaveChanges();
            }

            if (!context.Leagues.Any())
            {
                var leagues = new League[]
                {
                    new League{Name="Cardiff and District Badminton League", Sport=context.Sports.Find(1) },
                    new League{Name="Swansea Badminton League", Sport=context.Sports.Find(1) }
                };
                foreach (League l in leagues) context.Leagues.Add(l);
                context.SaveChanges();
            }

            if (!context.Clubs.Any())
            {
                var clubs = new Club[]
                {
                    new Club {Name="Cardiff Nomads",IsActive=true},
                    new Club{Name="Llantrisant", IsActive=true},
                    new Club{Name="Penarth", IsActive=true},
                    new Club{Name="Heath", IsActive=true},
                    new Club{Name="Bridgend Phoenix", IsActive=true},
                    new Club{Name="Holm View", IsActive=true}
                };
                foreach (Club c in clubs) context.Clubs.Add(c);
                context.SaveChanges();
            }

            if (!context.LeagueClubs.Any())
            {
                var leagueClubs = new LeagueClub[]
                {
                    new LeagueClub { League = context.Leagues.Find(1), Club = context.Clubs.Find (1) },
                    new LeagueClub { League = context.Leagues.Find(1), Club = context.Clubs.Find(2) },
                    new LeagueClub { League = context.Leagues.Find(1), Club = context.Clubs.Find(3) },
                    new LeagueClub { League = context.Leagues.Find(1), Club = context.Clubs.Find(4) },
                    new LeagueClub { League = context.Leagues.Find(1), Club = context.Clubs.Find(5) },
                    new LeagueClub { League = context.Leagues.Find(1), Club = context.Clubs.Find(6) }
                };
                
                foreach (LeagueClub lc in leagueClubs) context.LeagueClubs.Add(lc);
                context.SaveChanges();
            }

            if(!context.ClubVenues.Any())
            {
                var clubVenues = new ClubVenue[]
                {
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(1),
                        Venue = context.Venues.Find(1),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Monday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,22,30,0),
                        MaxMatches = 2
                    },
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(2),
                        Venue = context.Venues.Find(2),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Tuesday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,21,30,0),
                        MaxMatches = 2
                    },
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(2),
                        Venue = context.Venues.Find(2),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Friday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,21,30,0),
                        MaxMatches = 1
                    },
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(3),
                        Venue = context.Venues.Find(3),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Monday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,22,30,0),
                        MaxMatches = 2
                    },
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(6),
                        Venue = context.Venues.Find(4),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Tuesday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,22,30,0),
                        MaxMatches = 3
                    },
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(4),
                        Venue = context.Venues.Find(1),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Thursday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,22,30,0),
                        MaxMatches = 1
                    },
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(5),
                        Venue = context.Venues.Find(1),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Friday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,22,30,0),
                        MaxMatches = 2
                    },
                };
                foreach (ClubVenue cv in clubVenues) context.ClubVenues.Add(cv);
                context.SaveChanges();
            }

            if(!context.Seasons.Any())
            {
                var seasons = new Season[]
                {
                    new Season {
                        Name ="2015-2016",
                        StartDate = new DateTime(2015,9,1),
                        EndDate = new DateTime(2016,6,1),
                        League = context.Leagues.Find(1),
                        IsActive = false
                        
                    },
                    new Season {
                        Name ="2016-2017",
                        StartDate = new DateTime(2016,9,1),
                        EndDate = new DateTime(2017,6,1),
                        League = context.Leagues.Find(1),
                        IsActive = true

                    },
                    new Season {
                        Name ="2016-2017",
                        StartDate = new DateTime(2016,9,1),
                        EndDate = new DateTime(2017,6,1),
                        League = context.Leagues.Find(2),
                        IsActive = true
                    }
                };
                foreach (Season s in seasons) context.Seasons.Add(s);
                context.SaveChanges();
            }

            if(!context.UserDetails.Any())
            {
                var userDetails = new UserDetail[]
                {
                    new UserDetail
                    {
                        FirstName ="Alex",
                        LastName ="Wilson",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="a0wilson@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Andy",
                        LastName ="Li",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Chris",
                        LastName ="Short",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Chris",
                        LastName ="Jeynes",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Chris",
                        LastName ="Allen",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Peter",
                        LastName ="Bracegirdle",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Mike",
                        LastName ="Constantino",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Simon",
                        LastName ="Evans",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Huw",
                        LastName ="John",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Daniel",
                        LastName ="Thrift",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Peter",
                        LastName ="Yau",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Victor",
                        LastName ="Pang",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Oliver",
                        LastName ="Harerty",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Thomas",
                        LastName ="Spurrier",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Chris",
                        LastName ="Killick",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Karen",
                        LastName ="Sylvester",
                        Gender=context.Genders.Where(g => g.Name=="Female").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Daniel",
                        LastName ="Rhodes",
                        Gender=context.Genders.Where(g => g.Name=="Male").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    },
                    new UserDetail
                    {
                        FirstName ="Nicole",
                        LastName ="Walkley",
                        Gender=context.Genders.Where(g => g.Name=="Female").FirstOrDefault(),
                        EmailAddress="test@yahoo.com"
                    }

                };
                foreach (UserDetail ud in userDetails) context.UserDetails.Add(ud);
                context.SaveChanges();
            }

            if (!context.ClubUsers.Any())
            {
                var generalUsers = context.UserDetails.ToList();
                var clubUsers = new List<ClubUser>();
;               foreach( UserDetail gu in generalUsers)
                {
                    var clubUser = new ClubUser
                    {
                        Club = context.Clubs.Find(1),
                        UserDetail = gu,
                        IsActive = true,
                        ClubUserStatus = context.ClubUserStatuses.Find(1)
                    };
                    clubUsers.Add(clubUser);
                }
                foreach (ClubUser cu in clubUsers) context.ClubUsers.Add(cu);
                context.SaveChanges();
            }

            if (!context.Cups.Any())
            {
                var cups = new Cup[]
                {
                    new Cup()
                    {
                        Name = "Kerslake Cup",
                        League = context.Leagues.Find(1)
                    },

                    new Cup()
                    {
                        Name = "Grant Williams Cup",
                        League = context.Leagues.Find(1)
                    }
                };
                foreach (Cup c in cups) context.Cups.Add(c);
                context.SaveChanges();
            }
            
            if (!context.Teams.Any())
            {
                var teams = new Team[]
                {
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(1),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens B",
                        LeagueClub = context.LeagueClubs.Find(1),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(2),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens B",
                        LeagueClub = context.LeagueClubs.Find(2),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(3),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Womens A",
                        LeagueClub = context.LeagueClubs.Find(1),
                        Category = context.Categories.Find(Constants.Category.Womens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(4),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(2),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(5),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(6),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(3),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Womens A",
                        LeagueClub = context.LeagueClubs.Find(2),
                        Category = context.Categories.Find(Constants.Category.Womens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(2),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                };
                foreach (Team t in teams) context.Teams.Add(t);
                context.SaveChanges();
            }


            if (!context.Settings.Any())
            {
                var settings = new Setting[]
                {
                    new Setting{Name="MaxGroupsPerTeam" },
                    new Setting{Name="MaxPlayersPerGroup"}
                };
                foreach (Setting s in settings) context.Settings.Add(s);
                context.SaveChanges();
            }

            if(!context.Rules.Any())
            {
                var rules = new Rule[]
                {
                    new Rule{Setting=context.Settings.Find(1), Value=3 },
                    new Rule{Setting=context.Settings.Find(2), Value=2}
                };
                foreach (Rule r in rules) context.Rules.Add(r);
                context.SaveChanges();
            }
                       
            if (!context.LeagueRules.Any())
            {
                var leagueRules = new LeagueRule[]
                {
                    new LeagueRule{League=context.Leagues.Find(1), Rule=context.Rules.Find(1)},
                    new LeagueRule{League=context.Leagues.Find(1), Rule=context.Rules.Find(2)}
                };
                foreach (LeagueRule lr in leagueRules) context.LeagueRules.Add(lr);
                context.SaveChanges();
            }

            if (!context.CategoryRules.Any())
            {
                var categoryRules = new CategoryRule[]
                {
                    new CategoryRule{Category=context.Categories.Find(1), Rule=context.Rules.Find(1)},
                    new CategoryRule{Category=context.Categories.Find(1), Rule=context.Rules.Find(2)}
                };
                foreach (CategoryRule lr in categoryRules) context.CategoryRules.Add(lr);
                context.SaveChanges();
            }

        }
                
    }
}
