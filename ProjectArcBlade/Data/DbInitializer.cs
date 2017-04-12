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

            if(!context.Divisions.Any())
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

            if (!context.Leagues.Any())
            {
                var leagues = new League[]
                {
                    new League{Name="Cardiff League", Sport=context.Sports.Where(s => s.Name=="Badminton").FirstOrDefault() },
                    new League{Name="Swansea League", Sport=context.Sports.Where(s => s.Name=="Badminton").FirstOrDefault() }
                };
                foreach (League l in leagues) context.Leagues.Add(l);
                context.SaveChanges();
            }

            if (!context.Clubs.Any())
            {
                var clubs = new Club[]
                {
                    new Club {Name="Cardiff Nomads",IsActive=true},
                    new Club{Name="Llantrisant", IsActive=true}
                };
                foreach (Club c in clubs) context.Clubs.Add(c);
                context.SaveChanges();
            }

            if (!context.LeagueClubs.Any())
            {
                var leagueClubs = new LeagueClub[]
                {
                    new LeagueClub { League = context.Leagues.Where(l => l.Name == "Cardiff League").FirstOrDefault(), Club = context.Clubs.Where(c => c.Name == "Cardiff Nomads").FirstOrDefault() },
                    new LeagueClub { League = context.Leagues.Where(l => l.Name == "Cardiff League").FirstOrDefault(), Club = context.Clubs.Where(c => c.Name == "Llantrisant").FirstOrDefault() }
                };
                foreach (LeagueClub lc in leagueClubs) context.LeagueClubs.Add(lc);
                context.SaveChanges();
            }

            if(!context.Seasons.Any())
            {
                var seasons = new Season[]
                {
                    new Season {
                        Name ="2015 -2016",
                        StartDate = new DateTime(2015,9,1),
                        EndDate = new DateTime(2016,6,1),
                        League = context.Leagues.Where(l=>l.Name=="Cardiff League").FirstOrDefault(),
                        IsActive = false
                        
                    },
                    new Season {
                        Name ="2016 -2017",
                        StartDate = new DateTime(2016,9,1),
                        EndDate = new DateTime(2017,6,1),
                        League = context.Leagues.Where(l=>l.Name=="Cardiff League").FirstOrDefault(),
                        IsActive = true

                    },
                    new Season {
                        Name ="2016 -2017",
                        StartDate = new DateTime(2016,9,1),
                        EndDate = new DateTime(2017,6,1),
                        League = context.Leagues.Where(l=>l.Name=="Swansea League").FirstOrDefault(),
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
                        Club = context.Clubs.Where(c => c.Name == "Cardiff Nomads").FirstOrDefault(),
                        UserDetail = gu,
                        IsActive = true,
                        ClubUserStatus = context.ClubUserStatuses.Where(cus => cus.Name== "Available").FirstOrDefault()
                    };
                    clubUsers.Add(clubUser);
                }
                foreach (ClubUser cu in clubUsers) context.ClubUsers.Add(cu);
                context.SaveChanges();

            }

        }

        
    }
}
