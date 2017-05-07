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
        
        //Templates
        public DbSet<MatchTemplate> MatchTemplates { get; set; }
        public DbSet<MatchTemplateCategory> MatchTemplateCategories { get; set; }
        public DbSet<MatchTemplateSeason> MatchTemplateSeasons { get; set; }
        public DbSet<GroupTemplate> GroupTemplates { get; set; }
        public DbSet<RankTemplate> RankTemplates { get; set; }
        public DbSet<GameTemplate> GameTemplates { get; set; }
        public DbSet<HomeGroupTemplate> HomeGroupTemplates { get; set; }
        public DbSet<AwayGroupTemplate> AwayGroupTemplates { get; set; }
        
        public DbSet<MatchType> MatchTypes { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<DayOfTheWeek> DaysOfTheWeek { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AwardNominee> AwardNominees { get; set; }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubSubscriber> ClubSubscribers { get; set; }
        public DbSet<ClubSubscription> ClubSubscriptions { get; set; }
        public DbSet<ClubVenue> ClubVenues { get; set; }
        public DbSet<ClubPlayer> ClubPlayers { get; set; }
        public DbSet<ClubPlayerStatus> ClubPlayerStatuses { get; set; }

        public DbSet<Cup> Cups { get; set; }
        public DbSet<CupMatch> CupMatches { get; set; }
        public DbSet<CupMatchHandicap> CupMatchHandicaps { get; set; }
                
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueClub> LeagueClubs { get; set; }
        public DbSet<RescheduledStartDate> RescheduledStartDates { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Game> Games { get; set; }


        public DbSet<HomeMatchTeam> HomeMatchTeams { get; set; }
        public DbSet<HomeMatchTeamCaptain> HomeMatchTeamCaptains { get; set; }
        public DbSet<HomeMatchTeamGroup> HomeMatchTeamGroups { get; set; }
        public DbSet<HomeMatchTeamGroupPlayer> HomeMatchTeamGroupPlayers { get; set; }
        public DbSet<HomeGameResult> HomeGameResults { get; set; }
        public DbSet<HomeGameResultScore> HomeGameResultScores { get; set; }

        public DbSet<AwayMatchTeam> AwayMatchTeams { get; set; }
        public DbSet<AwayMatchTeamCaptain> AwayMatchTeamCaptains { get; set; }
        public DbSet<AwayMatchTeamGroup> AwayMatchTeamGroups { get; set; }
        public DbSet<AwayMatchTeamGroupPlayer> AwayMatchTeamGroupPlayers { get; set; }
        public DbSet<AwayGameResult> AwayGameResults { get; set; }
        public DbSet<AwayGameResultScore> AwayGameResultScores { get; set; }

        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<ResultType> ResultTypes { get; set; }
        public DbSet<ScoreStatus> ScoreStatuses { get; set; }
        public DbSet<Season> Seasons { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamCaptain> TeamCaptains { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<TeamStatus> TeamStatuses { get; set; }

        public DbSet<PlayerDetail> PlayerDetails { get; set; }
        public DbSet<Sport> Sports { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<LeagueRule> LeagueRules { get; set; }
        public DbSet<CategoryRule> CategoryRules { get; set; }

        public DbSet<ExclusionDate> ExclusionDates { get; set; }
        public DbSet<PointScore> PointScores { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);En

            builder.Entity<MatchTemplate>().ToTable("MatchTemplate");
            builder.Entity<MatchTemplateCategory>().ToTable("MatchTemplateCategory");
            builder.Entity<MatchTemplateSeason>().ToTable("MatchTemplateSeason");
            builder.Entity<GroupTemplate>().ToTable("GroupTemplate");
            builder.Entity<RankTemplate>().ToTable("RankTemplate");

            builder.Entity<GameTemplate>().ToTable("GameTemplate");
            builder.Entity<GameTemplate>()
                .HasOne(gt => gt.HomeGroupTemplate)
                .WithOne(hgt => hgt.GameTemplate)
                .HasForeignKey<HomeGroupTemplate>(hgt => hgt.GameTemplateId);
            builder.Entity<GameTemplate>()
                .HasOne(gt => gt.AwayGroupTemplate)
                .WithOne(agt => agt.GameTemplate)
                .HasForeignKey<AwayGroupTemplate>(agt => agt.GameTemplateId);

            builder.Entity<AwayGroupTemplate>().ToTable("AwayGroupTemplate");
            builder.Entity<HomeGroupTemplate>().ToTable("HomeGroupTemplate");
            
            builder.Entity<MatchType>().ToTable("MatchType");
            builder.Entity<Venue>().ToTable("Venue");
            builder.Entity<Award>().ToTable("Award");
            builder.Entity<AwardNominee>().ToTable("AwardNominee");
            builder.Entity<DayOfTheWeek>().ToTable("DayOfTheWeek");

            builder.Entity<Rank>().ToTable("Rank");
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Club>().ToTable("Club");
            builder.Entity<ClubSubscriber>().ToTable("ClubSubscriber");
            builder.Entity<ClubSubscription>().ToTable("ClubSubscription");
            builder.Entity<ClubPlayer>().ToTable("ClubPlayer");
            builder.Entity<ClubVenue>().ToTable("ClubVenue");
            builder.Entity<ClubPlayerStatus>().ToTable("ClubPlayerStatus");
            builder.Entity<Cup>().ToTable("Cup");
            builder.Entity<CupMatch>().ToTable("CupMatch");
            builder.Entity<CupMatchHandicap>().ToTable("CupMatchHandicap");
            builder.Entity<Division>().ToTable("Division");
            builder.Entity<Gender>().ToTable("Gender");
            builder.Entity<Group>().ToTable("Group");
            
            builder.Entity<League>().ToTable("League");
            builder.Entity<LeagueClub>().ToTable("LeagueClub");
            builder.Entity<RescheduledStartDate>().ToTable("RescheduledStartDate");

            //specify 1 to 1 relationships for match and teams
            builder.Entity<Match>().ToTable("Match");
            builder.Entity<Match>()
                .HasOne(m => m.AwayMatchTeam)
                .WithOne(amt => amt.Match)
                .HasForeignKey<AwayMatchTeam>(amt => amt.MatchId);
            builder.Entity<Match>()
                .HasOne(m => m.HomeMatchTeam)
                .WithOne(hmt => hmt.Match)
                .HasForeignKey<HomeMatchTeam>(amt => amt.MatchId);

            //specifiy 1 to 1 relationship for game and results
            builder.Entity<Game>().ToTable("Game");
            builder.Entity<Game>()
                .HasOne(g => g.HomeGameResult)
                .WithOne(hgr => hgr.Game)
                .HasForeignKey<HomeGameResult>(hgr => hgr.GameId);
            builder.Entity<Game>()
                .HasOne(g => g.AwayGameResult)
                .WithOne(agr => agr.Game)
                .HasForeignKey<AwayGameResult>(agr => agr.GameId);
            
            builder.Entity<HomeMatchTeam>().ToTable("HomeMatchTeam");
            builder.Entity<HomeMatchTeam>()
                .HasOne(hmt => hmt.HomeMatchTeamCaptain)
                .WithOne(hmtc => hmtc.HomeMatchTeam)
                .HasForeignKey<HomeMatchTeamCaptain>(hmtc => hmtc.HomeMatchTeamId);

            builder.Entity<HomeMatchTeamCaptain>().ToTable("HomeMatchTeamCaptain");
            builder.Entity<HomeMatchTeamGroup>().ToTable("HomeMatchTeamGroup");
            builder.Entity<HomeMatchTeamGroupPlayer>().ToTable("HomeMatchTeamGroupPlayer");
            builder.Entity<HomeGameResult>().ToTable("HomeGameResult");
            builder.Entity<HomeGameResultScore>().ToTable("HomeGameResultScore");

            builder.Entity<AwayMatchTeam>().ToTable("AwayMatchTeam");
            builder.Entity<AwayMatchTeam>()
                .HasOne(amt => amt.AwayMatchTeamCaptain)
                .WithOne(amtc => amtc.AwayMatchTeam)
                .HasForeignKey<AwayMatchTeamCaptain>(amtc => amtc.AwayMatchTeamId);

            builder.Entity<AwayMatchTeamCaptain>().ToTable("AwayMatchTeamCaptain");
            builder.Entity<AwayMatchTeamGroup>().ToTable("AwayMatchTeamGroup");
            builder.Entity<AwayMatchTeamGroupPlayer>().ToTable("AwayMatchTeamGroupPlayer");
            builder.Entity<AwayGameResult>().ToTable("AwayGameResult");
            builder.Entity<AwayGameResultScore>().ToTable("AwayGameResultScore");

            builder.Entity<MembershipType>().ToTable("MembershipType");
            builder.Entity<ResultType>().ToTable("ResultType");
            builder.Entity<ScoreStatus>().ToTable("ScoreStatus");
            builder.Entity<Season>().ToTable("Season");

            builder.Entity<Team>().ToTable("Team");
            builder.Entity<Team>()
                .HasOne(t => t.TeamCaptain)
                .WithOne(tc => tc.Team)
                .HasForeignKey<TeamCaptain>(tc => tc.TeamId);

            builder.Entity<TeamCaptain>().ToTable("TeamCaptain");
            builder.Entity<TeamPlayer>().ToTable("TeamPlayer");
            builder.Entity<TeamStatus>().ToTable("TeamStatus");

            builder.Entity<PlayerDetail>().ToTable("PlayerDetail");
            builder.Entity<Sport>().ToTable("Sport");

            builder.Entity<Setting>().ToTable("Setting");
            builder.Entity<Rule>().ToTable("Rule");
            builder.Entity<LeagueRule>().ToTable("LeagueRule");
            builder.Entity<CategoryRule>().ToTable("CategoryRule");

            builder.Entity<ExclusionDate>().ToTable("ExclusionDate");
            builder.Entity<PointScore>().ToTable("PointScore");
        }
    }
}
