using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Models;

namespace ProjectArcBlade.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AvailableDay> AvailableDays { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AwardNominee> AwardNominees { get; set; }
        public DbSet<AwayGameResult> AwayGameResults { get; set; }
        public DbSet<AwayGameResultScore> AwayGameResultScores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubSubscriber> ClubSubscribers { get; set; }
        public DbSet<ClubSubscription> ClubSubscriptions { get; set; }
        public DbSet<ClubUser> ClubUsers { get; set; }
        public DbSet<ClubUserStatus> ClubUserStatuses { get; set; }
        public DbSet<Cup> Cups { get; set; }
        public DbSet<CupMatch> CupMatches { get; set; }
        public DbSet<CupMatchHandicap> CupMatchHandicaps { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<HomeGameResult> HomeGameResults { get; set; }
        public DbSet<HomeGameResultScore> HomeGameResultScores { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueClub> LeagueClubs { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchScheduledDate> MatchScheduledDates { get; set; }
        public DbSet<HomeMatchTeam> MatchTeams { get; set; }
        public DbSet<HomeMatchTeamCaptain> MatchTeamCaptains { get; set; }
        public DbSet<HomeMatchTeamGroup> MatchTeamGroups { get; set; }
        public DbSet<HomeMatchTeamGroupPlayer> MatchTeamGroupPlayers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<ResultType> ResultTypes { get; set; }
        public DbSet<ScoreStatus> ScoreStatuses { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamCaptain> TeamCaptains { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);En

            builder.Entity<Address>().ToTable("Address");
            builder.Entity<AvailableDay>().ToTable("AvailableDay");
            builder.Entity<Award>().ToTable("Award");
            builder.Entity<AwardNominee>().ToTable("AwardNominee");
            builder.Entity<AwayGameResult>().ToTable("AwayGameResult");
            builder.Entity<AwayGameResultScore>().ToTable("AwayGameResultScore");
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Club>().ToTable("Club");
            builder.Entity<ClubSubscriber>().ToTable("ClubSubscriber");
            builder.Entity<ClubSubscription>().ToTable("ClubSubscription");
            builder.Entity<ClubUser>().ToTable("ClubUser");
            builder.Entity<ClubUserStatus>().ToTable("ClubUserStatus");
            builder.Entity<Cup>().ToTable("Cup");
            builder.Entity<CupMatch>().ToTable("CupMatch");
            builder.Entity<CupMatchHandicap>().ToTable("CupMatchHandicap");
            builder.Entity<Division>().ToTable("Division");
            builder.Entity<Gender>().ToTable("Gender");
            builder.Entity<Group>().ToTable("Group");
            builder.Entity<HomeGameResult>().ToTable("HomeGameResult");
            builder.Entity<HomeGameResultScore>().ToTable("HomeGameResultScore");
            builder.Entity<League>().ToTable("League");
            builder.Entity<LeagueClub>().ToTable("LeagueClub");
            builder.Entity<Match>().ToTable("Match");
            builder.Entity<MatchScheduledDate>().ToTable("MatchScheduledDate");
            builder.Entity<HomeMatchTeam>().ToTable("MatchTeam");
            builder.Entity<HomeMatchTeamCaptain>().ToTable("MatchTeamCaptain");
            builder.Entity<HomeMatchTeamGroup>().ToTable("MatchTeamGroup");
            builder.Entity<HomeMatchTeamGroupPlayer>().ToTable("MatchTeamGroupPlayer");
            builder.Entity<MembershipType>().ToTable("MembershipType");
            builder.Entity<ResultType>().ToTable("ResultType");
            builder.Entity<ScoreStatus>().ToTable("ScoreStatus");
            builder.Entity<Season>().ToTable("Season");
            builder.Entity<Team>().ToTable("Team");
            builder.Entity<TeamCaptain>().ToTable("TeamCaptain");
            builder.Entity<TeamPlayer>().ToTable("TeamPlayer");
            builder.Entity<UserDetail>().ToTable("UserDetail");
            builder.Entity<Sport>().ToTable("Sport");
        }
    }
}
