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
                    new Gender{Name="Female"}
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

            if (!context.ClubPlayerStatuses.Any())
            {
                var clubPlayerStatuses = new ClubPlayerStatus[]
                {
                    new ClubPlayerStatus{Name="Available"},
                    new ClubPlayerStatus{Name="Limited availability"},
                    new ClubPlayerStatus{Name="Injured"},
                    new ClubPlayerStatus{Name="Sick"},
                    new ClubPlayerStatus{Name="Unavailable"},
                };
                foreach (ClubPlayerStatus cus in clubPlayerStatuses) context.ClubPlayerStatuses.Add(cus);
                context.SaveChanges();
            }

            if(!context.Ranks.Any())
            {
                for(var i=1; i<26; i++)
                {
                    context.Ranks.Add(new Rank { Number = i });
                    context.SaveChanges();
                }
                
            }

            if (!context.Groups.Any())
            {
                var groups = new Group[]
                {
                    new Group{Name="First"},
                    new Group{Name="Second"},
                    new Group{Name="Third"},
                    new Group{Name="Fourth"},
                    new Group{Name="Fifth"},
                    new Group{Name="Sixth"},
                    new Group{Name="Seventh"},
                    new Group{Name="Eighth"},
                    new Group{Name="Ninth"},
                    new Group{Name="Tenth"},
                    new Group{Name="Eleventh"},
                    new Group{Name="Twelfth"},
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
                    new Venue{Name="Holm View Leisure Centre", AddressLine1="Skomer Road", Postcode="CF62 9DA"},
                    new Venue{Name="Cardiff Medical Centre Sports and Social Site", AddressLine1="Heath Hospital", Postcode="CF14 4XW"}
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
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Thursday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,22,30,0),
                        MaxMatches = 3
                    },
                    new ClubVenue
                    {
                        Club = context.Clubs.Find(4),
                        Venue = context.Venues.Find(5),
                        DayOfTheWeek = context.DaysOfTheWeek.Find(Constants.DayOfTheWeek.Tuesday),
                        StartTime = new DateTime(1,1,1,19,30,0),
                        EndTime = new DateTime(1,1,1,22,30,0),
                        MaxMatches = 2
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
                        IsActive = false

                    },
                    new Season {
                        Name ="2016-2017",
                        StartDate = new DateTime(2016,9,1),
                        EndDate = new DateTime(2017,6,1),
                        League = context.Leagues.Find(2),
                        IsActive = false
                    },
                    new Season
                    {
                        Name ="2017-2018",
                        StartDate = new DateTime(2017,9,1),
                        EndDate = new DateTime(2018,6,1),
                        League = context.Leagues.Find(1),
                        IsActive = true
                    }
                };
                foreach (Season s in seasons) context.Seasons.Add(s);
                context.SaveChanges();
            }

            if(!context.PlayerDetails.Any())
            {
                var playerDetails = new PlayerDetail[]
                {
                    new PlayerDetail
                    {
                        FirstName ="Alex",
                        LastName ="Wilson",
                        Gender=context.Genders.Find(1),
                        EmailAddress="a0wilson@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Andy",
                        LastName ="Li",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Chris",
                        LastName ="Short",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Chris",
                        LastName ="Jeynes",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Chris",
                        LastName ="Allen",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Peter",
                        LastName ="Bracegirdle",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Mike",
                        LastName ="Constantino",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Simon",
                        LastName ="Evans",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Huw",
                        LastName ="John",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Daniel",
                        LastName ="Thrift",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Peter",
                        LastName ="Yau",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Victor",
                        LastName ="Pang",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Oliver",
                        LastName ="Harerty",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Thomas",
                        LastName ="Spurrier",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Chris",
                        LastName ="Killick",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Karen",
                        LastName ="Sylvester",
                        Gender=context.Genders.Find(2),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Daniel",
                        LastName ="Rhodes",
                        Gender=context.Genders.Find(1),
                        EmailAddress="test@yahoo.com"
                    },
                    new PlayerDetail
                    {
                        FirstName ="Nicole",
                        LastName ="Walkley",
                        Gender=context.Genders.Find(2),
                        EmailAddress="test@yahoo.com"
                    }

                };
                foreach (PlayerDetail ud in playerDetails) context.PlayerDetails.Add(ud);
                context.SaveChanges();
            }

            if (!context.ClubPlayers.Any())
            {
                var playerDetails = context.PlayerDetails.ToList();
                var clubPlayers = new List<ClubPlayer>();
;               foreach( PlayerDetail playerDetail in playerDetails)
                {
                    var clubPlayer = new ClubPlayer
                    {
                        Club = context.Clubs.Find(1),
                        PlayerDetail = playerDetail,
                        IsActive = true,
                        ClubPlayerStatus = context.ClubPlayerStatuses.Find(1)
                    };
                    clubPlayers.Add(clubPlayer);
                }
                foreach (ClubPlayer cp in clubPlayers) context.ClubPlayers.Add(cp);
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
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens B",
                        LeagueClub = context.LeagueClubs.Find(1),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(2),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens B",
                        LeagueClub = context.LeagueClubs.Find(2),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(3),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Womens A",
                        LeagueClub = context.LeagueClubs.Find(1),
                        Category = context.Categories.Find(Constants.Category.Womens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(4),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens B",
                        LeagueClub = context.LeagueClubs.Find(4),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(2),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(5),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Mens A",
                        LeagueClub = context.LeagueClubs.Find(6),
                        Category = context.Categories.Find(Constants.Category.Mens),
                        Division = context.Divisions.Find(2),
                        Season = context.Seasons.Find(4),
                        TeamStatus = context.TeamStatuses.Find(1)
                    },
                    new Team
                    {
                        Name = "Womens A",
                        LeagueClub = context.LeagueClubs.Find(2),
                        Category = context.Categories.Find(Constants.Category.Womens),
                        Division = context.Divisions.Find(1),
                        Season = context.Seasons.Find(4),
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

            //Setup Templates.
            if(!context.MatchTemplates.Any())
            {
                var matchTemplates = new MatchTemplate[]
                {
                    new MatchTemplate { Name="Mens Match (2010)" },
                    new MatchTemplate { Name="Womens Match (2014)" }
                };
                foreach (MatchTemplate mt in matchTemplates) context.MatchTemplates.Add(mt);
                context.SaveChanges();                
            }

            if(!context.GroupTemplates.Any())
            {
                var groupTemplates = new GroupTemplate[]
                {
                    //mens
                    new GroupTemplate {Group = context.Groups.Find(1), MatchTemplate = context.MatchTemplates.Find(1) }, //1
                    new GroupTemplate {Group = context.Groups.Find(2), MatchTemplate = context.MatchTemplates.Find(1) }, //2
                    new GroupTemplate {Group = context.Groups.Find(3), MatchTemplate = context.MatchTemplates.Find(1) }, //3
                    //womens
                    new GroupTemplate {Group = context.Groups.Find(1), MatchTemplate = context.MatchTemplates.Find(2) }, //4
                    new GroupTemplate {Group = context.Groups.Find(2), MatchTemplate = context.MatchTemplates.Find(2) }, //5
                    new GroupTemplate {Group = context.Groups.Find(3), MatchTemplate = context.MatchTemplates.Find(2) }, //6
                    new GroupTemplate {Group = context.Groups.Find(4), MatchTemplate = context.MatchTemplates.Find(2) } //7
                };
                foreach (GroupTemplate gt in groupTemplates) context.GroupTemplates.Add(gt);
                context.SaveChanges();
            }

            if(!context.RankTemplates.Any())
            {
                var rankTemplates = new RankTemplate[]
                {
                    new RankTemplate { Rank = context.Ranks.Find(1), GroupTemplate = context.GroupTemplates.Find(1) },
                    new RankTemplate { Rank = context.Ranks.Find(2), GroupTemplate = context.GroupTemplates.Find(1) },
                    new RankTemplate { Rank = context.Ranks.Find(3), GroupTemplate = context.GroupTemplates.Find(2) },
                    new RankTemplate { Rank = context.Ranks.Find(4), GroupTemplate = context.GroupTemplates.Find(2) },
                    new RankTemplate { Rank = context.Ranks.Find(5), GroupTemplate = context.GroupTemplates.Find(3) },
                    new RankTemplate { Rank = context.Ranks.Find(6), GroupTemplate = context.GroupTemplates.Find(3) },
                    new RankTemplate { Rank = context.Ranks.Find(1), GroupTemplate = context.GroupTemplates.Find(4) },
                    new RankTemplate { Rank = context.Ranks.Find(2), GroupTemplate = context.GroupTemplates.Find(4) },
                    new RankTemplate { Rank = context.Ranks.Find(3), GroupTemplate = context.GroupTemplates.Find(5) },
                    new RankTemplate { Rank = context.Ranks.Find(4), GroupTemplate = context.GroupTemplates.Find(5) },
                    new RankTemplate { Rank = context.Ranks.Find(1), GroupTemplate = context.GroupTemplates.Find(6) },
                    new RankTemplate { Rank = context.Ranks.Find(3), GroupTemplate = context.GroupTemplates.Find(6) },
                    new RankTemplate { Rank = context.Ranks.Find(2), GroupTemplate = context.GroupTemplates.Find(7) },
                    new RankTemplate { Rank = context.Ranks.Find(4), GroupTemplate = context.GroupTemplates.Find(7) }                    
                };
                foreach (RankTemplate rt in rankTemplates) context.RankTemplates.Add(rt);
                context.SaveChanges();
            }

            if(!context.GameTemplates.Any())
            {
                var gameTemplates = new GameTemplate[]
                {
                    //mens
                    new GameTemplate { Order=1, MatchTemplate=context.MatchTemplates.Find(1) },//1
                    new GameTemplate { Order=2, MatchTemplate=context.MatchTemplates.Find(1) },//2
                    new GameTemplate { Order=3, MatchTemplate=context.MatchTemplates.Find(1) },//3
                    new GameTemplate { Order=4, MatchTemplate=context.MatchTemplates.Find(1) },//4
                    new GameTemplate { Order=5, MatchTemplate=context.MatchTemplates.Find(1) },//5
                    new GameTemplate { Order=6, MatchTemplate=context.MatchTemplates.Find(1) },//6
                    new GameTemplate { Order=7, MatchTemplate=context.MatchTemplates.Find(1) },//7
                    new GameTemplate { Order=8, MatchTemplate=context.MatchTemplates.Find(1) },//8
                    new GameTemplate { Order=9, MatchTemplate=context.MatchTemplates.Find(1) },//9
                    //womens
                    new GameTemplate { Order=1, MatchTemplate=context.MatchTemplates.Find(2) },//10
                    new GameTemplate { Order=2, MatchTemplate=context.MatchTemplates.Find(2) },//11
                    new GameTemplate { Order=3, MatchTemplate=context.MatchTemplates.Find(2) },//12
                    new GameTemplate { Order=4, MatchTemplate=context.MatchTemplates.Find(2) },//13
                    new GameTemplate { Order=5, MatchTemplate=context.MatchTemplates.Find(2) },//14
                    new GameTemplate { Order=6, MatchTemplate=context.MatchTemplates.Find(2) },//15
                };
                foreach (GameTemplate gt in gameTemplates) context.GameTemplates.Add(gt);
                context.SaveChanges();
            }

            if(!context.HomeGroupTemplates.Any())
            {
                var homeGroupTemplates = new HomeGroupTemplate[]
                {
                    //mens
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(1), GroupTemplate = context.GroupTemplates.Find(1) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(2), GroupTemplate = context.GroupTemplates.Find(2) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(3), GroupTemplate = context.GroupTemplates.Find(3) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(4), GroupTemplate = context.GroupTemplates.Find(2) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(5), GroupTemplate = context.GroupTemplates.Find(3) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(6), GroupTemplate = context.GroupTemplates.Find(1) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(7), GroupTemplate = context.GroupTemplates.Find(3) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(8), GroupTemplate = context.GroupTemplates.Find(1) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(9), GroupTemplate = context.GroupTemplates.Find(2) },
                    //womens
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(10), GroupTemplate = context.GroupTemplates.Find(4) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(11), GroupTemplate = context.GroupTemplates.Find(5) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(12), GroupTemplate = context.GroupTemplates.Find(5) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(13), GroupTemplate = context.GroupTemplates.Find(4) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(14), GroupTemplate = context.GroupTemplates.Find(6) },
                    new HomeGroupTemplate{ GameTemplate = context.GameTemplates.Find(15), GroupTemplate = context.GroupTemplates.Find(7) }
                };
                foreach (HomeGroupTemplate hgt in homeGroupTemplates) context.HomeGroupTemplates.Add(hgt);
                context.SaveChanges();
            }

            if(!context.AwayGroupTemplates.Any())
            {
                var awayGroupTemplates = new AwayGroupTemplate[]
                {
                    //mens
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(1), GroupTemplate = context.GroupTemplates.Find(1) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(2), GroupTemplate = context.GroupTemplates.Find(2) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(3), GroupTemplate = context.GroupTemplates.Find(3) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(4), GroupTemplate = context.GroupTemplates.Find(1) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(5), GroupTemplate = context.GroupTemplates.Find(2) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(6), GroupTemplate = context.GroupTemplates.Find(3) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(7), GroupTemplate = context.GroupTemplates.Find(1) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(8), GroupTemplate = context.GroupTemplates.Find(2) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(9), GroupTemplate = context.GroupTemplates.Find(3) },
                    //womens
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(10), GroupTemplate = context.GroupTemplates.Find(4) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(11), GroupTemplate = context.GroupTemplates.Find(5) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(12), GroupTemplate = context.GroupTemplates.Find(4) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(13), GroupTemplate = context.GroupTemplates.Find(5) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(14), GroupTemplate = context.GroupTemplates.Find(6) },
                    new AwayGroupTemplate{ GameTemplate = context.GameTemplates.Find(15), GroupTemplate = context.GroupTemplates.Find(7) }
                };
                foreach (AwayGroupTemplate agt in awayGroupTemplates) context.AwayGroupTemplates.Add(agt);
                context.SaveChanges();
            }

            if(!context.MatchTemplateCategories.Any())
            {
                var matchTemplateCategories = new MatchTemplateCategory[]
                {
                    new MatchTemplateCategory{ Category = context.Categories.Find(1), MatchTemplate = context.MatchTemplates.Find(1) },
                    new MatchTemplateCategory{ Category = context.Categories.Find(3), MatchTemplate = context.MatchTemplates.Find(1) },
                    new MatchTemplateCategory{ Category = context.Categories.Find(2), MatchTemplate = context.MatchTemplates.Find(2) }
                };
                foreach (MatchTemplateCategory mtc in matchTemplateCategories) context.MatchTemplateCategories.Add(mtc);
                context.SaveChanges();
            }

            if(!context.MatchTemplateSeasons.Any())
            {
                var matchTemplateSeaons = new MatchTemplateSeason[]
                {
                    new MatchTemplateSeason { Season = context.Seasons.Find(4), MatchTemplate = context.MatchTemplates.Find(1) },
                    new MatchTemplateSeason { Season = context.Seasons.Find(4), MatchTemplate = context.MatchTemplates.Find(2) }
                };
                foreach (MatchTemplateSeason mts in matchTemplateSeaons) context.MatchTemplateSeasons.Add(mts);
                context.SaveChanges();
            }
        }
                
    }
}
