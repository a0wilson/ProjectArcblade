﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectArcBlade.Data;

namespace ProjectArcBlade.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170420000534_01")]
    partial class _01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("AwardId");

                    b.Property<int>("ClubUserId");

                    b.Property<int?>("MatchId");

                    b.HasKey("Id");

                    b.HasIndex("AwardId");

                    b.HasIndex("ClubUserId");

                    b.HasIndex("MatchId");

                    b.ToTable("AwardNominee");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayMatchTeamGroupId");

                    b.Property<int?>("ResultTypeId");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamGroupId");

                    b.HasIndex("ResultTypeId");

                    b.ToTable("AwayGameResult");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGameResultScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AwayGameResultId");

                    b.Property<int?>("ClubUserId");

                    b.Property<DateTime>("DateSubmitted");

                    b.Property<int?>("Score");

                    b.Property<int?>("ScoreStatusId");

                    b.HasKey("Id");

                    b.HasIndex("AwayGameResultId");

                    b.HasIndex("ClubUserId");

                    b.HasIndex("ScoreStatusId");

                    b.ToTable("AwayGameResultScore");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchId");

                    b.Property<int?>("ResultTypeId");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .IsUnique();

                    b.HasIndex("ResultTypeId");

                    b.HasIndex("TeamId");

                    b.ToTable("AwayMatchTeam");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamCaptain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AwayMatchTeamGroupPlayerId");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamGroupPlayerId");

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

                    b.Property<int?>("ClubUserId");

                    b.HasKey("Id");

                    b.HasIndex("AwayMatchTeamGroupId");

                    b.HasIndex("ClubUserId");

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

            modelBuilder.Entity("ProjectArcBlade.Models.ClubSubscriber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubSubscriptionId");

                    b.Property<int?>("ClubUserId");

                    b.HasKey("Id");

                    b.HasIndex("ClubSubscriptionId");

                    b.HasIndex("ClubUserId");

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

            modelBuilder.Entity("ProjectArcBlade.Models.ClubUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubId");

                    b.Property<int?>("ClubUserStatusId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("MembershipNumber");

                    b.Property<int?>("MembershipTypeId");

                    b.Property<int>("UserDetailId");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("ClubUserStatusId");

                    b.HasIndex("MembershipTypeId");

                    b.HasIndex("UserDetailId");

                    b.ToTable("ClubUser");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.ClubUserStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ClubUserStatus");
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

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HomeMatchTeamGroupId");

                    b.Property<int?>("ResultTypeId");

                    b.HasKey("Id");

                    b.HasIndex("HomeMatchTeamGroupId");

                    b.HasIndex("ResultTypeId");

                    b.ToTable("HomeGameResult");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResultScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubUserId");

                    b.Property<DateTime>("DateSubmitted");

                    b.Property<int>("HomeGameResultId");

                    b.Property<int?>("Score");

                    b.Property<int?>("ScoreStatusId");

                    b.HasKey("Id");

                    b.HasIndex("ClubUserId");

                    b.HasIndex("HomeGameResultId");

                    b.HasIndex("ScoreStatusId");

                    b.ToTable("HomeGameResultScore");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchId");

                    b.Property<int?>("ResultTypeId");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .IsUnique();

                    b.HasIndex("ResultTypeId");

                    b.HasIndex("TeamId");

                    b.ToTable("HomeMatchTeam");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamCaptain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HomeMatchTeamGroupPlayerId");

                    b.HasKey("Id");

                    b.HasIndex("HomeMatchTeamGroupPlayerId");

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

                    b.Property<int?>("ClubUserId");

                    b.Property<int?>("HomeMatchTeamGroupId");

                    b.HasKey("Id");

                    b.HasIndex("ClubUserId");

                    b.HasIndex("HomeMatchTeamGroupId");

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

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StartTime");

                    b.Property<int?>("VenueId");

                    b.HasKey("Id");

                    b.HasIndex("MatchTypeId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("VenueId");

                    b.ToTable("Match");
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

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("LeagueClubId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamCaptain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TeamPlayerId");

                    b.HasKey("Id");

                    b.HasIndex("TeamPlayerId");

                    b.ToTable("TeamCaptain");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClubUserId");

                    b.Property<int?>("GroupId");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("ClubUserId");

                    b.HasIndex("GroupId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPlayer");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.UserDetail", b =>
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

                    b.ToTable("UserDetail");
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
                        .HasForeignKey("AwardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ClubUser", "ClubUser")
                        .WithMany("AwardNominees")
                        .HasForeignKey("ClubUserId")
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

                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("AwayGameResults")
                        .HasForeignKey("ResultTypeId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayGameResultScore", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayGameResult", "AwayGameResult")
                        .WithMany("AwayGameResultScores")
                        .HasForeignKey("AwayGameResultId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ClubUser", "ClubUser")
                        .WithMany("AwayGameResultScores")
                        .HasForeignKey("ClubUserId");

                    b.HasOne("ProjectArcBlade.Models.ScoreStatus", "ScoreStatus")
                        .WithMany("AwayGameResultScores")
                        .HasForeignKey("ScoreStatusId");
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
                });

            modelBuilder.Entity("ProjectArcBlade.Models.AwayMatchTeamCaptain", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.AwayMatchTeamGroupPlayer", "AwayMatchTeamGroupPlayer")
                        .WithMany("AwayMatchTeamCaptains")
                        .HasForeignKey("AwayMatchTeamGroupPlayerId");
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

                    b.HasOne("ProjectArcBlade.Models.ClubUser", "ClubUser")
                        .WithMany("AwayMatchTeamGroupPlayers")
                        .HasForeignKey("ClubUserId");
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

            modelBuilder.Entity("ProjectArcBlade.Models.ClubSubscriber", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubSubscription", "ClubSubscription")
                        .WithMany("ClubSubscribers")
                        .HasForeignKey("ClubSubscriptionId");

                    b.HasOne("ProjectArcBlade.Models.ClubUser", "ClubUser")
                        .WithMany("ClubSubscribers")
                        .HasForeignKey("ClubUserId");
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

            modelBuilder.Entity("ProjectArcBlade.Models.ClubUser", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Club", "Club")
                        .WithMany("ClubUsers")
                        .HasForeignKey("ClubId");

                    b.HasOne("ProjectArcBlade.Models.ClubUserStatus", "ClubUserStatus")
                        .WithMany("ClubUsers")
                        .HasForeignKey("ClubUserStatusId");

                    b.HasOne("ProjectArcBlade.Models.MembershipType", "MembershipType")
                        .WithMany("ClubUsers")
                        .HasForeignKey("MembershipTypeId");

                    b.HasOne("ProjectArcBlade.Models.UserDetail", "UserDetail")
                        .WithMany("ClubUsers")
                        .HasForeignKey("UserDetailId")
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

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResult", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeamGroup", "HomeMatchTeamGroup")
                        .WithMany("HomeGameResults")
                        .HasForeignKey("HomeMatchTeamGroupId");

                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("HomeGameResults")
                        .HasForeignKey("ResultTypeId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeGameResultScore", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubUser", "ClubUser")
                        .WithMany("HomeGameResultScores")
                        .HasForeignKey("ClubUserId");

                    b.HasOne("ProjectArcBlade.Models.HomeGameResult", "HomeGameResult")
                        .WithMany("HomeGameResultScores")
                        .HasForeignKey("HomeGameResultId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectArcBlade.Models.ScoreStatus", "ScoreStatus")
                        .WithMany("HomeGameResultScores")
                        .HasForeignKey("ScoreStatusId");
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
                });

            modelBuilder.Entity("ProjectArcBlade.Models.HomeMatchTeamCaptain", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeamGroupPlayer", "HomeMatchTeamGroupPlayer")
                        .WithMany("HomeMatchTeamCaptains")
                        .HasForeignKey("HomeMatchTeamGroupPlayerId");
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
                    b.HasOne("ProjectArcBlade.Models.ClubUser", "ClubUser")
                        .WithMany("HomeMatchTeamGroupPlayers")
                        .HasForeignKey("ClubUserId");

                    b.HasOne("ProjectArcBlade.Models.HomeMatchTeamGroup", "HomeMatchTeamGroup")
                        .WithMany("HomeMatchTeamGroupPlayers")
                        .HasForeignKey("HomeMatchTeamGroupId");
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

            modelBuilder.Entity("ProjectArcBlade.Models.PointScore", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ResultType", "ResultType")
                        .WithMany("PointScores")
                        .HasForeignKey("ResultTypeId");

                    b.HasOne("ProjectArcBlade.Models.Season", "Season")
                        .WithMany("PointScores")
                        .HasForeignKey("SeasonId");
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
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamCaptain", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.TeamPlayer", "TeamPlayer")
                        .WithMany("TeamCaptains")
                        .HasForeignKey("TeamPlayerId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.TeamPlayer", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.ClubUser", "ClubUser")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("ClubUserId");

                    b.HasOne("ProjectArcBlade.Models.Group", "Group")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("GroupId");

                    b.HasOne("ProjectArcBlade.Models.Team", "Team")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("ProjectArcBlade.Models.UserDetail", b =>
                {
                    b.HasOne("ProjectArcBlade.Models.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}