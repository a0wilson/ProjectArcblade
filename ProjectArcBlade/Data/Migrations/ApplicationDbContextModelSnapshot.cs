using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectArcBlade.Data;

namespace ProjectArcBlade.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Award", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Award");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwardNominee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwardId");

                    b.Property<int>("ClubPlayerId");

                    b.Property<int?>("MatchId");

                    b.HasKey("Id");

                    b.HasIndex("AwardId");

                    b.HasIndex("ClubPlayerId");

                    b.HasIndex("MatchId");

                    b.ToTable("AwardNominee");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayMatchTeamGroupId");

                    b.Property<int>("GameId");

                    b.Property<int?>("ResultTypeId");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamGroupId");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.HasIndex("ResultTypeId");

                    b.ToTable("AwayGameResult");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGameResultScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayGameResultId");

                    b.Property<DateTime>("DateSubmitted");

                    b.Property<int>("Score");

                    b.Property<int?>("ScoreStatusId");

                    b.Property<int?>("SubmittedByClubPlayerId");

                    b.Property<int?>("SubmittedByLeagueClubId");

                    b.HasKey("Id");

                    b.HasIndex("AwayGameResultId");

                    b.HasIndex("ScoreStatusId");

                    b.HasIndex("SubmittedByClubPlayerId");

                    b.HasIndex("SubmittedByLeagueClubId");

                    b.ToTable("AwayGameResultScore");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGroupTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameTemplateId");

                    b.Property<int?>("GroupTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("GameTemplateId")
                        .IsUnique();

                    b.HasIndex("GroupTemplateId");

                    b.ToTable("AwayGroupTemplate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchId");

                    b.Property<int?>("ResultTypeId");

                    b.Property<int?>("TeamId");

                    b.Property<int?>("TeamStatusId");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .IsUnique();

                    b.HasIndex("ResultTypeId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamStatusId");

                    b.ToTable("AwayMatchTeam");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamCaptain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AwayMatchTeamId");

                    b.Property<int?>("ClubPlayerId");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamId")
                        .IsUnique();

                    b.HasIndex("ClubPlayerId");

                    b.ToTable("AwayMatchTeamCaptain");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayMatchTeamId");

                    b.Property<int?>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamId");

                    b.HasIndex("GroupId");

                    b.ToTable("AwayMatchTeamGroup");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamGroupPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayMatchTeamGroupId");

                    b.Property<int?>("ClubPlayerId");

                    b.Property<int?>("RankId");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamGroupId");

                    b.HasIndex("ClubPlayerId");

                    b.HasIndex("RankId");

                    b.ToTable("AwayMatchTeamGroupPlayer");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.CategoryRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("RuleId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RuleId");

                    b.ToTable("CategoryRule");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("VenueId");

                    b.HasKey("Id");

                    b.HasIndex("VenueId");

                    b.ToTable("Club");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubId");

                    b.Property<int?>("ClubPlayerStatusId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("MembershipNumber");

                    b.Property<int?>("MembershipTypeId");

                    b.Property<int?>("PlayerDetailId");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("ClubPlayerStatusId");

                    b.HasIndex("MembershipTypeId");

                    b.HasIndex("PlayerDetailId");

                    b.ToTable("ClubPlayer");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubPlayerStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ClubPlayerStatus");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubSubscriber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubPlayerId");

                    b.Property<int?>("ClubSubscriptionId");

                    b.HasKey("Id");

                    b.HasIndex("ClubPlayerId");

                    b.HasIndex("ClubSubscriptionId");

                    b.ToTable("ClubSubscriber");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int>("ClubId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SeasonId");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("SeasonId");

                    b.ToTable("ClubSubscription");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubVenue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubId");

                    b.Property<int?>("DayOfTheWeekId");

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("MaxMatches");

                    b.Property<DateTime>("StartTime");

                    b.Property<int?>("VenueId");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("DayOfTheWeekId");

                    b.HasIndex("VenueId");

                    b.ToTable("ClubVenue");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Cup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("LeagueId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Cup");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.CupMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CupId");

                    b.Property<int>("MatchId");

                    b.HasKey("Id");

                    b.HasIndex("CupId");

                    b.HasIndex("MatchId");

                    b.ToTable("CupMatch");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.CupMatchHandicap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CupMatchId");

                    b.Property<int>("Handicap");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("CupMatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("CupMatchHandicap");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.DayOfTheWeek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DayOfTheWeek");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Division");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ExclusionDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateToExclude");

                    b.Property<int?>("LeagueClubId");

                    b.Property<string>("Reason");

                    b.Property<int?>("SeasonId");

                    b.HasKey("Id");

                    b.HasIndex("LeagueClubId");

                    b.HasIndex("SeasonId");

                    b.ToTable("ExclusionDate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayMatchTeamGroupId");

                    b.Property<int?>("HomeMatchTeamGroupId");

                    b.Property<int?>("MatchId");

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamGroupId");

                    b.HasIndex("HomeMatchTeamGroupId");

                    b.HasIndex("MatchId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.GameTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MatchTemplateId");

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.HasIndex("MatchTemplateId");

                    b.ToTable("GameTemplate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.GroupTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GroupId");

                    b.Property<int?>("MatchTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("MatchTemplateId");

                    b.ToTable("GroupTemplate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameId");

                    b.Property<int?>("HomeMatchTeamGroupId");

                    b.Property<int?>("ResultTypeId");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.HasIndex("HomeMatchTeamGroupId");

                    b.HasIndex("ResultTypeId");

                    b.ToTable("HomeGameResult");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResultScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateSubmitted");

                    b.Property<int?>("HomeGameResultId");

                    b.Property<int>("Score");

                    b.Property<int?>("ScoreStatusId");

                    b.Property<int?>("SubmittedByClubPlayerId");

                    b.Property<int?>("SubmittedByLeagueClubId");

                    b.HasKey("Id");

                    b.HasIndex("HomeGameResultId");

                    b.HasIndex("ScoreStatusId");

                    b.HasIndex("SubmittedByClubPlayerId");

                    b.HasIndex("SubmittedByLeagueClubId");

                    b.ToTable("HomeGameResultScore");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGroupTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameTemplateId");

                    b.Property<int?>("GroupTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("GameTemplateId")
                        .IsUnique();

                    b.HasIndex("GroupTemplateId");

                    b.ToTable("HomeGroupTemplate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchId");

                    b.Property<int?>("ResultTypeId");

                    b.Property<int?>("TeamId");

                    b.Property<int?>("TeamStatusId");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .IsUnique();

                    b.HasIndex("ResultTypeId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamStatusId");

                    b.ToTable("HomeMatchTeam");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamCaptain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubPlayerId");

                    b.Property<int>("HomeMatchTeamId");

                    b.HasKey("Id");

                    b.HasIndex("ClubPlayerId");

                    b.HasIndex("HomeMatchTeamId")
                        .IsUnique();

                    b.ToTable("HomeMatchTeamCaptain");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GroupId");

                    b.Property<int?>("HomeMatchTeamId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("HomeMatchTeamId");

                    b.ToTable("HomeMatchTeamGroup");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamGroupPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubPlayerId");

                    b.Property<int?>("HomeMatchTeamGroupId");

                    b.Property<int?>("RankId");

                    b.HasKey("Id");

                    b.HasIndex("ClubPlayerId");

                    b.HasIndex("HomeMatchTeamGroupId");

                    b.HasIndex("RankId");

                    b.ToTable("HomeMatchTeamGroupPlayer");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("League");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.LeagueClub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubId");

                    b.Property<int?>("LeagueId");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("LeagueId");

                    b.ToTable("LeagueClub");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.LeagueRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LeagueId");

                    b.Property<int?>("RuleId");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("RuleId");

                    b.ToTable("LeagueRule");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MatchTypeId");

                    b.Property<int?>("SeasonId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<DateTime?>("StartTime");

                    b.Property<int?>("VenueId");

                    b.HasKey("Id");

                    b.HasIndex("MatchTypeId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("VenueId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.MatchTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MatchTemplate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.MatchTemplateCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("MatchTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MatchTemplateId");

                    b.ToTable("MatchTemplateCategory");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.MatchTemplateSeason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MatchTemplateId");

                    b.Property<int?>("SeasonId");

                    b.HasKey("Id");

                    b.HasIndex("MatchTemplateId");

                    b.HasIndex("SeasonId");

                    b.ToTable("MatchTemplateSeason");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.MatchType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MatchType");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.MembershipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("MembershipType");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.PlayerDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("GenderId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MobileNumber");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.ToTable("PlayerDetail");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.PointScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PointValue");

                    b.Property<int?>("ResultTypeId");

                    b.Property<int?>("SeasonId");

                    b.HasKey("Id");

                    b.HasIndex("ResultTypeId");

                    b.HasIndex("SeasonId");

                    b.ToTable("PointScore");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Rank");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.RankTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GroupTemplateId");

                    b.Property<int?>("RankId");

                    b.HasKey("Id");

                    b.HasIndex("GroupTemplateId");

                    b.HasIndex("RankId");

                    b.ToTable("RankTemplate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.RescheduledStartDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MatchId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("RescheduledStartDate");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ResultType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ResultType");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SettingId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.ToTable("Rule");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ScoreStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ScoreStatus");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("LeagueId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Sport");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("DivisionId");

                    b.Property<int?>("LeagueClubId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("SeasonId");

                    b.Property<int?>("TeamStatusId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("LeagueClubId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("TeamStatusId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamCaptain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubPlayerId");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("ClubPlayerId");

                    b.HasIndex("TeamId")
                        .IsUnique();

                    b.ToTable("TeamCaptain");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubPlayerId");

                    b.Property<int?>("GroupId");

                    b.Property<int?>("RankId");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("ClubPlayerId");

                    b.HasIndex("GroupId");

                    b.HasIndex("RankId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPlayer");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TeamStatus");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("Country");

                    b.Property<string>("County");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Postcode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Venue");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwardNominee", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Award", "Award")
                        .WithMany("AwardNominees")
                        .HasForeignKey("AwardId");

                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("AwardNominees")
                        .HasForeignKey("ClubPlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.Match", "Match")
                        .WithMany("AwardNominees")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGameResult", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayMatchTeamGroup", "AwayMatchTeamGroup")
                        .WithMany("AwayGameResults")
                        .HasForeignKey("AwayMatchTeamGroupId");

                    b.HasOne("ProjectArcBlade.Models.Game", "Game")
                        .WithOne("AwayGameResult")
                        .HasForeignKey("ProjectArcBlade.Models.AwayGameResult", "GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("AwayGameResults")
                        .HasForeignKey("ResultTypeId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGameResultScore", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayGameResult", "AwayGameResult")
                        .WithMany("AwayGameResultScores")
                        .HasForeignKey("AwayGameResultId");

                    b.HasOne("ProjectArcBlade.Models.ScoreStatus", "ScoreStatus")
                        .WithMany("AwayGameResultScores")
                        .HasForeignKey("ScoreStatusId");

                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "SubmittedByClubPlayer")
                        .WithMany("AwayGameResultScores")
                        .HasForeignKey("SubmittedByClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.LeagueClub", "SubmittedByLeagueClub")
                        .WithMany("AwayGameResultScores")
                        .HasForeignKey("SubmittedByLeagueClubId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGroupTemplate", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.GameTemplate", "GameTemplate")
                        .WithOne("AwayGroupTemplate")
                        .HasForeignKey("ProjectArcBlade.Models.AwayGroupTemplate", "GameTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.GroupTemplate", "GroupTemplate")
                        .WithMany("AwayGroupTemplates")
                        .HasForeignKey("GroupTemplateId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeam", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Match", "Match")
                        .WithOne("AwayMatchTeam")
                        .HasForeignKey("ProjectArcBlade.Models.AwayMatchTeam", "MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("AwayMatchTeams")
                        .HasForeignKey("ResultTypeId");

                    b.HasOne("ProjectArcBlade.Models.Team", "Team")
                        .WithMany("AwayMatchTeams")
                        .HasForeignKey("TeamId");

                    b.HasOne("ProjectArcBlade.Models.TeamStatus", "TeamStatus")
                        .WithMany("AwayMatchTeams")
                        .HasForeignKey("TeamStatusId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamCaptain", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayMatchTeam", "AwayMatchTeam")
                        .WithOne("AwayMatchTeamCaptain")
                        .HasForeignKey("ProjectArcBlade.Models.AwayMatchTeamCaptain", "AwayMatchTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("AwayMatchTeamCaptaincies")
                        .HasForeignKey("ClubPlayerId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamGroup", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayMatchTeam", "AwayMatchTeam")
                        .WithMany("AwayMatchTeamGroups")
                        .HasForeignKey("AwayMatchTeamId");

                    b.HasOne("ProjectArcBlade.Models.Group", "Group")
                        .WithMany("AwayMatchTeamGroups")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamGroupPlayer", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayMatchTeamGroup", "AwayMatchTeamGroup")
                        .WithMany("AwayMatchTeamGroupPlayers")
                        .HasForeignKey("AwayMatchTeamGroupId");

                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("AwayMatchTeamGroupPlayers")
                        .HasForeignKey("ClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.Rank", "Rank")
                        .WithMany("AwayMatchTeamGroupPlayers")
                        .HasForeignKey("RankId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.CategoryRule", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("ProjectArcBlade.Models.Rule", "Rule")
                        .WithMany("CategoryRules")
                        .HasForeignKey("RuleId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Club", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Venue")
                        .WithMany("Clubs")
                        .HasForeignKey("VenueId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubPlayer", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Club", "Club")
                        .WithMany("ClubPlayers")
                        .HasForeignKey("ClubId");

                    b.HasOne("ProjectArcBlade.Models.ClubPlayerStatus", "ClubPlayerStatus")
                        .WithMany("ClubPlayers")
                        .HasForeignKey("ClubPlayerStatusId");

                    b.HasOne("ProjectArcBlade.Models.MembershipType", "MembershipType")
                        .WithMany("ClubPlayers")
                        .HasForeignKey("MembershipTypeId");

                    b.HasOne("ProjectArcBlade.Models.PlayerDetail", "PlayerDetail")
                        .WithMany("ClubPlayers")
                        .HasForeignKey("PlayerDetailId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubSubscriber", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("ClubSubscribers")
                        .HasForeignKey("ClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.ClubSubscription", "ClubSubscription")
                        .WithMany("ClubSubscribers")
                        .HasForeignKey("ClubSubscriptionId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubSubscription", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Club", "Club")
                        .WithMany("ClubSubscriptions")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.Season", "Season")
                        .WithMany("ClubSubscriptions")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubVenue", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Club", "Club")
                        .WithMany("ClubVenues")
                        .HasForeignKey("ClubId");

                    b.HasOne("ProjectArcBlade.Models.DayOfTheWeek", "DayOfTheWeek")
                        .WithMany("ClubVenues")
                        .HasForeignKey("DayOfTheWeekId");

                    b.HasOne("ProjectArcBlade.Models.Venue", "Venue")
                        .WithMany("ClubVenues")
                        .HasForeignKey("VenueId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Cup", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.League", "League")
                        .WithMany("Cups")
                        .HasForeignKey("LeagueId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.CupMatch", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Cup", "Cup")
                        .WithMany("CupMatches")
                        .HasForeignKey("CupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.Match", "Match")
                        .WithMany("CupMatches")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectArcBlade.Models.CupMatchHandicap", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.CupMatch", "CupMatch")
                        .WithMany("CupMatchHandicaps")
                        .HasForeignKey("CupMatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ExclusionDate", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.LeagueClub", "LeagueClub")
                        .WithMany("ExclusionDates")
                        .HasForeignKey("LeagueClubId");

                    b.HasOne("ProjectArcBlade.Models.Season", "Season")
                        .WithMany("ExclusionDates")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Game", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayMatchTeamGroup", "AwayMatchTeamGroup")
                        .WithMany("Games")
                        .HasForeignKey("AwayMatchTeamGroupId");

                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeamGroup", "HomeMatchTeamGroup")
                        .WithMany("Games")
                        .HasForeignKey("HomeMatchTeamGroupId");

                    b.HasOne("ProjectArcBlade.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.GameTemplate", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.MatchTemplate", "MatchTemplate")
                        .WithMany("MatchGameTemplates")
                        .HasForeignKey("MatchTemplateId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.GroupTemplate", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Group", "Group")
                        .WithMany("GroupTemplates")
                        .HasForeignKey("GroupId");

                    b.HasOne("ProjectArcBlade.Models.MatchTemplate", "MatchTemplate")
                        .WithMany("GroupTemplates")
                        .HasForeignKey("MatchTemplateId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResult", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Game", "Game")
                        .WithOne("HomeGameResult")
                        .HasForeignKey("ProjectArcBlade.Models.HomeGameResult", "GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeamGroup", "HomeMatchTeamGroup")
                        .WithMany("HomeGameResults")
                        .HasForeignKey("HomeMatchTeamGroupId");

                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("HomeGameResults")
                        .HasForeignKey("ResultTypeId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResultScore", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.HomeGameResult", "HomeGameResult")
                        .WithMany("HomeGameResultScores")
                        .HasForeignKey("HomeGameResultId");

                    b.HasOne("ProjectArcBlade.Models.ScoreStatus", "ScoreStatus")
                        .WithMany("HomeGameResultScores")
                        .HasForeignKey("ScoreStatusId");

                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "SubmittedByClubPlayer")
                        .WithMany("HomeGameResultScores")
                        .HasForeignKey("SubmittedByClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.LeagueClub", "SubmittedByLeagueClub")
                        .WithMany("HomeGameResultScores")
                        .HasForeignKey("SubmittedByLeagueClubId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGroupTemplate", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.GameTemplate", "GameTemplate")
                        .WithOne("HomeGroupTemplate")
                        .HasForeignKey("ProjectArcBlade.Models.HomeGroupTemplate", "GameTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.GroupTemplate", "GroupTemplate")
                        .WithMany("HomeGroupTemplates")
                        .HasForeignKey("GroupTemplateId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeam", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Match", "Match")
                        .WithOne("HomeMatchTeam")
                        .HasForeignKey("ProjectArcBlade.Models.HomeMatchTeam", "MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("HomeMatchTeams")
                        .HasForeignKey("ResultTypeId");

                    b.HasOne("ProjectArcBlade.Models.Team", "Team")
                        .WithMany("HomeMatchTeams")
                        .HasForeignKey("TeamId");

                    b.HasOne("ProjectArcBlade.Models.TeamStatus", "TeamStatus")
                        .WithMany("HomeMatchTeams")
                        .HasForeignKey("TeamStatusId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamCaptain", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("HomeMatchTeamCaptaincies")
                        .HasForeignKey("ClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeam", "HomeMatchTeam")
                        .WithOne("HomeMatchTeamCaptain")
                        .HasForeignKey("ProjectArcBlade.Models.HomeMatchTeamCaptain", "HomeMatchTeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamGroup", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Group", "Group")
                        .WithMany("HomeMatchTeamGroups")
                        .HasForeignKey("GroupId");

                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeam", "HomeMatchTeam")
                        .WithMany("HomeMatchTeamGroups")
                        .HasForeignKey("HomeMatchTeamId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamGroupPlayer", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("HomeMatchTeamGroupPlayers")
                        .HasForeignKey("ClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeamGroup", "HomeMatchTeamGroup")
                        .WithMany("HomeMatchTeamGroupPlayers")
                        .HasForeignKey("HomeMatchTeamGroupId");

                    b.HasOne("ProjectArcBlade.Models.Rank", "Rank")
                        .WithMany("HomeMatchTeamGroupPlayers")
                        .HasForeignKey("RankId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.League", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Sport", "Sport")
                        .WithMany("Leagues")
                        .HasForeignKey("SportId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.LeagueClub", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Club", "Club")
                        .WithMany()
                        .HasForeignKey("ClubId");

                    b.HasOne("ProjectArcBlade.Models.League", "League")
                        .WithMany("LeagueClubs")
                        .HasForeignKey("LeagueId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.LeagueRule", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId");

                    b.HasOne("ProjectArcBlade.Models.Rule", "Rule")
                        .WithMany("LeagueRules")
                        .HasForeignKey("RuleId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Match", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.MatchType", "MatchType")
                        .WithMany("Matches")
                        .HasForeignKey("MatchTypeId");

                    b.HasOne("ProjectArcBlade.Models.Season", "Season")
                        .WithMany("Matches")
                        .HasForeignKey("SeasonId");

                    b.HasOne("ProjectArcBlade.Models.Venue", "Venue")
                        .WithMany("Matches")
                        .HasForeignKey("VenueId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.MatchTemplateCategory", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Category", "Category")
                        .WithMany("MatchTemplateCategories")
                        .HasForeignKey("CategoryId");

                    b.HasOne("ProjectArcBlade.Models.MatchTemplate", "MatchTemplate")
                        .WithMany("MatchTemplateCategories")
                        .HasForeignKey("MatchTemplateId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.MatchTemplateSeason", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.MatchTemplate", "MatchTemplate")
                        .WithMany("MatchTemplateSeasons")
                        .HasForeignKey("MatchTemplateId");

                    b.HasOne("ProjectArcBlade.Models.Season", "Season")
                        .WithMany("MatchTemplateSeasons")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.PlayerDetail", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectArcBlade.Models.PointScore", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("PointScores")
                        .HasForeignKey("ResultTypeId");

                    b.HasOne("ProjectArcBlade.Models.Season", "Season")
                        .WithMany("PointScores")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.RankTemplate", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.GroupTemplate", "GroupTemplate")
                        .WithMany("RankTemplates")
                        .HasForeignKey("GroupTemplateId");

                    b.HasOne("ProjectArcBlade.Models.Rank", "Rank")
                        .WithMany("RankTemplates")
                        .HasForeignKey("RankId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.RescheduledStartDate", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Match", "Match")
                        .WithMany("RescheduledStartDates")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Rule", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Setting", "Setting")
                        .WithMany("Rules")
                        .HasForeignKey("SettingId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Season", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.League", "League")
                        .WithMany("Seasons")
                        .HasForeignKey("LeagueId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.Team", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Category", "Category")
                        .WithMany("Teams")
                        .HasForeignKey("CategoryId");

                    b.HasOne("ProjectArcBlade.Models.Division", "Division")
                        .WithMany("Teams")
                        .HasForeignKey("DivisionId");

                    b.HasOne("ProjectArcBlade.Models.LeagueClub", "LeagueClub")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueClubId");

                    b.HasOne("ProjectArcBlade.Models.Season", "Season")
                        .WithMany("Teams")
                        .HasForeignKey("SeasonId");

                    b.HasOne("ProjectArcBlade.Models.TeamStatus", "TeamStatus")
                        .WithMany("Teams")
                        .HasForeignKey("TeamStatusId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamCaptain", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("TeamCaptaincies")
                        .HasForeignKey("ClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.Team", "Team")
                        .WithOne("TeamCaptain")
                        .HasForeignKey("ProjectArcBlade.Models.TeamCaptain", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamPlayer", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubPlayer", "ClubPlayer")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("ClubPlayerId");

                    b.HasOne("ProjectArcBlade.Models.Group", "Group")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("GroupId");

                    b.HasOne("ProjectArcBlade.Models.Rank", "Rank")
                        .WithMany()
                        .HasForeignKey("RankId");

                    b.HasOne("ProjectArcBlade.Models.Team", "Team")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId");
                });
        }
    }
}
