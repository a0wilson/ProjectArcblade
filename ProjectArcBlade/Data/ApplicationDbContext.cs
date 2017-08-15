using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
        public DbSet<MatchTemplateLink> MatchTemplateLinks { get; set; }
        public DbSet<GroupTemplate> GroupTemplates { get; set; }
        public DbSet<RankTemplate> RankTemplates { get; set; }
        public DbSet<GameTemplate> GameTemplates { get; set; }
        public DbSet<SetTemplate> SetTemplates { get; set; }
        public DbSet<HomeGroupTemplate> HomeGroupTemplates { get; set; }
        public DbSet<AwayGroupTemplate> AwayGroupTemplates { get; set; }

        //Statuses
        public DbSet<Status> Statuses { get; set; }
        public DbSet<MatchStatus> MatchStatuses { get; set; }
        public DbSet<SetStatus> SetStatuses { get; set; }
        public DbSet<TeamStatus> TeamStatuses { get; set; }
        public DbSet<ClubPlayerStatus> ClubPlayerStatuses { get; set; }
        public DbSet<ScoreStatus> ScoreStatuses { get; set; }

        //Types
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<ResultType> ResultTypes { get; set; }
        public DbSet<MatchType> MatchTypes { get; set; }

        //lookup
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<JoinCondition> JoinConditions { get; set; }

        //rules
        public DbSet<Rule> Rules { get; set; }
        public DbSet<ResultRule> ResultRules { get; set; }
        public DbSet<ResultRuleException> ResultRuleExceptions { get; set; }

        //scoresheets
        public DbSet<HomeScoreSheet> HomeScoreSheets { get; set; }
        public DbSet<AwayScoreSheet> AwayScoreSheets { get; set; }
        public DbSet<ScoreSheetLine> ScoreSheetLines { get; set; }
        public DbSet<HomeScoreSheetLine> HomeScoreSheetLines { get; set; }
        public DbSet<AwayScoreSheetLine> AwayScoreSheetLines { get; set; }

        public DbSet<Audit> Audits { get; set; }
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

        public DbSet<Cup> Cups { get; set; }
        public DbSet<CupMatch> CupMatches { get; set; }
        public DbSet<CupMatchHandicap> CupMatchHandicaps { get; set; }

        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueClub> LeagueClubs { get; set; }
        public DbSet<RescheduledStartDate> RescheduledStartDates { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PromotionMatch> PromotionMatches { get; set; }

        public DbSet<HomeMatchTeam> HomeMatchTeams { get; set; }
        public DbSet<HomeMatchTeamCaptain> HomeMatchTeamCaptains { get; set; }
        public DbSet<HomeMatchTeamGroup> HomeMatchTeamGroups { get; set; }
        public DbSet<HomeMatchTeamGroupPlayer> HomeMatchTeamGroupPlayers { get; set; }
        public DbSet<HomeResult> HomeResults { get; set; }
        public DbSet<MatchHomeResult> MatchHomeResults { get; set; }
        public DbSet<SetHomeResult> SetHomeResults { get; set; }
        public DbSet<GameHomeResult> GameHomeResults { get; set; }
        public DbSet<HomeTeamScore> HomeTeamScores { get; set; }
        public DbSet<HomeTeamHomeTeamScore> HomeTeamHomeTeamScores { get; set; }
        public DbSet<AwayTeamHomeTeamScore> AwayTeamHomeTeamScores { get; set; }

        public DbSet<AwayMatchTeam> AwayMatchTeams { get; set; }
        public DbSet<AwayMatchTeamCaptain> AwayMatchTeamCaptains { get; set; }
        public DbSet<AwayMatchTeamGroup> AwayMatchTeamGroups { get; set; }
        public DbSet<AwayMatchTeamGroupPlayer> AwayMatchTeamGroupPlayers { get; set; }
        public DbSet<AwayResult> AwayResults { get; set; }
        public DbSet<MatchAwayResult> MatchAwayResults { get; set; }
        public DbSet<SetAwayResult> SetAwayResults { get; set; }
        public DbSet<GameAwayResult> GameAwayResults { get; set; }
        public DbSet<AwayTeamScore> AwayTeamScores { get; set; }
        public DbSet<HomeTeamAwayTeamScore> HomeTeamAwayTeamScores { get; set; }
        public DbSet<AwayTeamAwayTeamScore> AwayTeamAwayTeamScores { get; set; }
        
        public DbSet<Season> Seasons { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamCaptain> TeamCaptains { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<PlayerDetail> PlayerDetails { get; set; }
        public DbSet<Sport> Sports { get; set; }

        public DbSet<ExclusionDate> ExclusionDates { get; set; }
        public DbSet<PointScore> PointScores { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);En

            builder.Entity<MatchTemplate>().ToTable("MatchTemplate");
            builder.Entity<MatchTemplateLink>().ToTable("MatchTemplateLink");
            builder.Entity<GroupTemplate>().ToTable("GroupTemplate");
            builder.Entity<RankTemplate>().ToTable("RankTemplate");
            builder.Entity<GameTemplate>().ToTable("GameTemplate");

            builder.Entity<SetTemplate>().ToTable("SetTemplate");
            builder.Entity<SetTemplate>()
                .HasOne(gt => gt.HomeGroupTemplate)
                .WithOne(hgt => hgt.SetTemplate)
                .HasForeignKey<HomeGroupTemplate>(hgt => hgt.SetTemplateId);
            builder.Entity<SetTemplate>()
                .HasOne(gt => gt.AwayGroupTemplate)
                .WithOne(agt => agt.SetTemplate)
                .HasForeignKey<AwayGroupTemplate>(agt => agt.SetTemplateId);

            builder.Entity<AwayGroupTemplate>().ToTable("AwayGroupTemplate");
            builder.Entity<HomeGroupTemplate>().ToTable("HomeGroupTemplate");

            //Status tables
            builder.Entity<Status>().ToTable("Status");
            builder.Entity<MatchStatus>().ToTable("MatchStatus");
            builder.Entity<SetStatus>().ToTable("SetStatus");
            builder.Entity<TeamStatus>().ToTable("TeamStatus");
            builder.Entity<ScoreStatus>().ToTable("ScoreStatus");

            //Type tables
            builder.Entity<Models.Type>().ToTable("Type");
            builder.Entity<MatchType>().ToTable("MatchType");
            builder.Entity<MembershipType>().ToTable("MembershipType");
            builder.Entity<ResultType>().ToTable("ResultType");

            //lookup
            builder.Entity<Lookup>().ToTable("Lookup");
            builder.Entity<Condition>().ToTable("Condition");
            builder.Entity<Operator>().ToTable("Operator");
            builder.Entity<JoinCondition>().ToTable("JoinCondition");

            //result rule
            builder.Entity<Rule>().ToTable("Rule");
            builder.Entity<ResultRule>().ToTable("ResultRule");
            builder.Entity<ResultRule>()
                .HasOne(rr => rr.JoinCondition)
                .WithMany(jc => jc.ResultRules)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ResultRuleException>().ToTable("ResultRuleException");
            builder.Entity<ResultRuleException>()
                .HasOne(rre => rre.TargetRule)
                .WithMany(rr => rr.TargetRules);
            builder.Entity<ResultRuleException>()
                .HasOne(rre => rre.ExceptionRule)
                .WithMany(rr => rr.ExceptionRules);

            //scoresheets
            builder.Entity<HomeScoreSheet>().ToTable("HomeScoreSheet");
            builder.Entity<AwayScoreSheet>().ToTable("AwayScoreSheet");
            builder.Entity<ScoreSheetLine>().ToTable("ScoreSheetLine");
            builder.Entity<HomeScoreSheetLine>().ToTable("HomeScoreSheetLine");
            builder.Entity<AwayScoreSheetLine>().ToTable("AwayScoreSheetLine");            
            
            builder.Entity<Audit>().ToTable("Audit");
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
            builder.Entity<PromotionMatch>().ToTable("PromotionMatch");
                        
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
            builder.Entity<Match>()
                .HasOne(m => m.MatchAwayResult)
                .WithOne(mar => mar.Match)
                .HasForeignKey<MatchAwayResult>(mar => mar.MatchId);
            builder.Entity<Match>()
                .HasOne(m => m.MatchHomeResult)
                .WithOne(mar => mar.Match)
                .HasForeignKey<MatchHomeResult>(mar => mar.MatchId);
            builder.Entity<Match>()
                .HasOne(m => m.HomeScoreSheet)
                .WithOne(hss => hss.Match)
                .HasForeignKey<HomeScoreSheet>(hss => hss.MatchId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Match>()
                .HasOne(m => m.AwayScoreSheet)
                .WithOne(ass => ass.Match)
                .HasForeignKey<AwayScoreSheet>(ass => ass.MatchId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Set>().ToTable("Set");
            builder.Entity<Set>()
                .HasOne(s => s.SetAwayResult)
                .WithOne(sar => sar.Set)
                .HasForeignKey<SetAwayResult>(sar => sar.SetId);
            builder.Entity<Set>()
               .HasOne(s => s.SetHomeResult)
               .WithOne(sar => sar.Set)
               .HasForeignKey<SetHomeResult>(sar => sar.SetId);
            
            builder.Entity<Game>().ToTable("Game");
            builder.Entity<Game>()
                .HasOne(g => g.GameAwayResult)
                .WithOne(gar => gar.Game)
                .HasForeignKey<GameAwayResult>(gar => gar.GameId);
            builder.Entity<Game>()
                .HasOne(g => g.GameHomeResult)
                .WithOne(gar => gar.Game)
                .HasForeignKey<GameHomeResult>(gar => gar.GameId);
            


            builder.Entity<HomeMatchTeam>().ToTable("HomeMatchTeam");
            builder.Entity<HomeMatchTeam>()
                .HasOne(hmt => hmt.HomeMatchTeamCaptain)
                .WithOne(hmtc => hmtc.HomeMatchTeam)
                .HasForeignKey<HomeMatchTeamCaptain>(hmtc => hmtc.HomeMatchTeamId);
            builder.Entity<HomeMatchTeam>()
                .HasOne(hmt => hmt.HomeScoreSheet)
                .WithOne(hss => hss.HomeMatchTeam)
                .HasForeignKey<HomeScoreSheet>(hss => hss.HomeMatchTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<HomeMatchTeamCaptain>().ToTable("HomeMatchTeamCaptain");
            builder.Entity<HomeMatchTeamGroup>().ToTable("HomeMatchTeamGroup");
            builder.Entity<HomeMatchTeamGroupPlayer>().ToTable("HomeMatchTeamGroupPlayer");            
            builder.Entity<HomeTeamScore>().ToTable("HomeTeamScore");            
            builder.Entity<HomeResult>().ToTable("HomeResult");
            builder.Entity<MatchHomeResult>().ToTable("MatchHomeResult");
            builder.Entity<SetHomeResult>().ToTable("SetHomeResult");
            builder.Entity<GameHomeResult>().ToTable("GameHomeResult");

            builder.Entity<AwayTeamHomeTeamScore>().ToTable("AwayTeamHomeTeamScore");
            builder.Entity<AwayTeamHomeTeamScore>()
                .HasOne(athts => athts.Game)
                .WithOne(g => g.AwayTeamHomeTeamScore)
                .HasForeignKey<Game>(g => g.AwayTeamHomeTeamScoreId)
                .OnDelete(DeleteBehavior.Restrict);                

            builder.Entity<HomeTeamHomeTeamScore>().ToTable("HomeTeamHomeTeamScore");
            builder.Entity<HomeTeamHomeTeamScore>()
                .HasOne(hthts => hthts.Game)
                .WithOne(g => g.HomeTeamHomeTeamScore)
                .HasForeignKey<Game>(g => g.HomeTeamHomeTeamScoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AwayMatchTeam>().ToTable("AwayMatchTeam");
            builder.Entity<AwayMatchTeam>()
                .HasOne(amt => amt.AwayMatchTeamCaptain)
                .WithOne(amtc => amtc.AwayMatchTeam)
                .HasForeignKey<AwayMatchTeamCaptain>(amtc => amtc.AwayMatchTeamId);
            builder.Entity<AwayMatchTeam>()
                .HasOne(amt => amt.AwayScoreSheet)
                .WithOne(ass => ass.AwayMatchTeam)
                .HasForeignKey<AwayScoreSheet>(ass => ass.AwayMatchTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AwayMatchTeamCaptain>().ToTable("AwayMatchTeamCaptain");
            builder.Entity<AwayMatchTeamGroup>().ToTable("AwayMatchTeamGroup");
            builder.Entity<AwayMatchTeamGroupPlayer>().ToTable("AwayMatchTeamGroupPlayer");            
            builder.Entity<AwayTeamScore>().ToTable("AwayTeamScore");
            builder.Entity<AwayResult>().ToTable("AwayResult");
            builder.Entity<MatchAwayResult>().ToTable("MatchAwayResult");
            builder.Entity<SetAwayResult>().ToTable("SetAwayResult");
            builder.Entity<GameAwayResult>().ToTable("GameAwayResult");

            builder.Entity<AwayTeamAwayTeamScore>().ToTable("AwayTeamAwayTeamScore");
            builder.Entity<AwayTeamAwayTeamScore>()
                .HasOne(atats => atats.Game)
                .WithOne(g => g.AwayTeamAwayTeamScore)
                .HasForeignKey<Game>(g => g.AwayTeamAwayTeamScoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<HomeTeamAwayTeamScore>().ToTable("HomeTeamAwayTeamScore");
            builder.Entity<HomeTeamAwayTeamScore>()
                .HasOne(htats => htats.Game)
                .WithOne(g => g.HomeTeamAwayTeamScore)
                .HasForeignKey<Game>(g => g.HomeTeamAwayTeamScoreId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Season>().ToTable("Season");

            builder.Entity<Team>().ToTable("Team");
            builder.Entity<Team>()
                .HasOne(t => t.TeamCaptain)
                .WithOne(tc => tc.Team)
                .HasForeignKey<TeamCaptain>(tc => tc.TeamId);

            builder.Entity<TeamCaptain>().ToTable("TeamCaptain");
            builder.Entity<TeamPlayer>().ToTable("TeamPlayer");
            
            builder.Entity<PlayerDetail>().ToTable("PlayerDetail");
            builder.Entity<Sport>().ToTable("Sport");
            
            builder.Entity<ExclusionDate>().ToTable("ExclusionDate");
            builder.Entity<PointScore>().ToTable("PointScore");
        }
    }
}
