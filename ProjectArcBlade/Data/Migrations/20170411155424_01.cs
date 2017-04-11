﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectArcBlade.Data.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Line1 = table.Column<string>(nullable: false),
                    Line2 = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Award",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Award", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClubUserStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubUserStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoreStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Club_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDetail_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDetail_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    SportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                    table.ForeignKey(
                        name: "FK_League_Sport_SportId",
                        column: x => x.SportId,
                        principalTable: "Sport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvailableDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubId = table.Column<int>(nullable: false),
                    Day = table.Column<string>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableDay_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubId = table.Column<int>(nullable: true),
                    ClubUserStatusId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MembershipNumber = table.Column<string>(nullable: true),
                    MembershipTypeId = table.Column<int>(nullable: true),
                    UserDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubUser_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubUser_ClubUserStatus_ClubUserStatusId",
                        column: x => x.ClubUserStatusId,
                        principalTable: "ClubUserStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubUser_MembershipType_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MembershipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubUser_UserDetail_UserDetailId",
                        column: x => x.UserDetailId,
                        principalTable: "UserDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueClub",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubId = table.Column<int>(nullable: true),
                    LeagueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueClub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueClub_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeagueClub_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LeagueId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Season_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClubSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    ClubId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubSubscription_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubSubscription_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    DivisionId = table.Column<int>(nullable: true),
                    LeagueClubId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    SeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_LeagueClub_LeagueClubId",
                        column: x => x.LeagueClubId,
                        principalTable: "LeagueClub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClubSubscriber",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubSubscriptionId = table.Column<int>(nullable: true),
                    ClubUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubSubscriber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubSubscriber_ClubSubscription_ClubSubscriptionId",
                        column: x => x.ClubSubscriptionId,
                        principalTable: "ClubSubscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubSubscriber_ClubUser_ClubUserId",
                        column: x => x.ClubUserId,
                        principalTable: "ClubUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: true),
                    ResultTypeId = table.Column<int>(nullable: true),
                    SeasonId = table.Column<int>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    WinningTeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_ResultType_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_WinningTeamId",
                        column: x => x.WinningTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubUserId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_ClubUser_ClubUserId",
                        column: x => x.ClubUserId,
                        principalTable: "ClubUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwardNominee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwardId = table.Column<int>(nullable: false),
                    ClubUserId = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardNominee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardNominee_Award_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Award",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwardNominee_ClubUser_ClubUserId",
                        column: x => x.ClubUserId,
                        principalTable: "ClubUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwardNominee_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayMatchTeam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayMatchTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeam_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CupMatch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CupId = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CupMatch_Cup_CupId",
                        column: x => x.CupId,
                        principalTable: "Cup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CupMatch_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeam_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchScheduledDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchScheduledDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchScheduledDate_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamCaptain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamPlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCaptain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamCaptain_TeamPlayer_TeamPlayerId",
                        column: x => x.TeamPlayerId,
                        principalTable: "TeamPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayMatchTeamGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayMatchTeamId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayMatchTeamGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamGroup_AwayMatchTeam_AwayMatchTeamId",
                        column: x => x.AwayMatchTeamId,
                        principalTable: "AwayMatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CupMatchHandicap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CupMatchId = table.Column<int>(nullable: false),
                    Handicap = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupMatchHandicap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CupMatchHandicap_CupMatch_CupMatchId",
                        column: x => x.CupMatchId,
                        principalTable: "CupMatch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CupMatchHandicap_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: true),
                    HomeMatchTeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTeamGroup_MatchTeam_HomeMatchTeamId",
                        column: x => x.HomeMatchTeamId,
                        principalTable: "MatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayGameResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayMatchTeamGroupId = table.Column<int>(nullable: true),
                    ResultTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayGameResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayGameResult_AwayMatchTeamGroup_AwayMatchTeamGroupId",
                        column: x => x.AwayMatchTeamGroupId,
                        principalTable: "AwayMatchTeamGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayGameResult_ResultType_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayMatchTeamGroupPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayMatchTeamGroupId = table.Column<int>(nullable: true),
                    ClubUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayMatchTeamGroupPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamGroupPlayer_AwayMatchTeamGroup_AwayMatchTeamGroupId",
                        column: x => x.AwayMatchTeamGroupId,
                        principalTable: "AwayMatchTeamGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamGroupPlayer_ClubUser_ClubUserId",
                        column: x => x.ClubUserId,
                        principalTable: "ClubUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeGameResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HomeMatchTeamGroupId = table.Column<int>(nullable: true),
                    ResultTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeGameResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeGameResult_MatchTeamGroup_HomeMatchTeamGroupId",
                        column: x => x.HomeMatchTeamGroupId,
                        principalTable: "MatchTeamGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeGameResult_ResultType_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamGroupPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubUserId = table.Column<int>(nullable: true),
                    HomeMatchTeamGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamGroupPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamGroupPlayer_ClubUser_ClubUserId",
                        column: x => x.ClubUserId,
                        principalTable: "ClubUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTeamGroupPlayer_MatchTeamGroup_HomeMatchTeamGroupId",
                        column: x => x.HomeMatchTeamGroupId,
                        principalTable: "MatchTeamGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayGameResultScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayGameResultId = table.Column<int>(nullable: false),
                    ClubUserId = table.Column<int>(nullable: true),
                    DateSubmitted = table.Column<DateTime>(nullable: false),
                    Score = table.Column<int>(nullable: true),
                    ScoreStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayGameResultScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayGameResultScore_AwayGameResult_AwayGameResultId",
                        column: x => x.AwayGameResultId,
                        principalTable: "AwayGameResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwayGameResultScore_ClubUser_ClubUserId",
                        column: x => x.ClubUserId,
                        principalTable: "ClubUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayGameResultScore_ScoreStatus_ScoreStatusId",
                        column: x => x.ScoreStatusId,
                        principalTable: "ScoreStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayMatchTeamCaptain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayMatchTeamGroupPlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayMatchTeamCaptain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamCaptain_AwayMatchTeamGroupPlayer_AwayMatchTeamGroupPlayerId",
                        column: x => x.AwayMatchTeamGroupPlayerId,
                        principalTable: "AwayMatchTeamGroupPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeGameResultScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubUserId = table.Column<int>(nullable: true),
                    DateSubmitted = table.Column<DateTime>(nullable: false),
                    HomeGameResultId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: true),
                    ScoreStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeGameResultScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeGameResultScore_ClubUser_ClubUserId",
                        column: x => x.ClubUserId,
                        principalTable: "ClubUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeGameResultScore_HomeGameResult_HomeGameResultId",
                        column: x => x.HomeGameResultId,
                        principalTable: "HomeGameResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeGameResultScore_ScoreStatus_ScoreStatusId",
                        column: x => x.ScoreStatusId,
                        principalTable: "ScoreStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamCaptain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HomeMatchTeamGroupPlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamCaptain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamCaptain_MatchTeamGroupPlayer_HomeMatchTeamGroupPlayerId",
                        column: x => x.HomeMatchTeamGroupPlayerId,
                        principalTable: "MatchTeamGroupPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvailableDay_ClubId",
                table: "AvailableDay",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardNominee_AwardId",
                table: "AwardNominee",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardNominee_ClubUserId",
                table: "AwardNominee",
                column: "ClubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardNominee_MatchId",
                table: "AwardNominee",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayGameResult_AwayMatchTeamGroupId",
                table: "AwayGameResult",
                column: "AwayMatchTeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayGameResult_ResultTypeId",
                table: "AwayGameResult",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayGameResultScore_AwayGameResultId",
                table: "AwayGameResultScore",
                column: "AwayGameResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayGameResultScore_ClubUserId",
                table: "AwayGameResultScore",
                column: "ClubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayGameResultScore_ScoreStatusId",
                table: "AwayGameResultScore",
                column: "ScoreStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_MatchId",
                table: "AwayMatchTeam",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_TeamId",
                table: "AwayMatchTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeamCaptain_AwayMatchTeamGroupPlayerId",
                table: "AwayMatchTeamCaptain",
                column: "AwayMatchTeamGroupPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeamGroup_AwayMatchTeamId",
                table: "AwayMatchTeamGroup",
                column: "AwayMatchTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeamGroup_GroupId",
                table: "AwayMatchTeamGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeamGroupPlayer_AwayMatchTeamGroupId",
                table: "AwayMatchTeamGroupPlayer",
                column: "AwayMatchTeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeamGroupPlayer_ClubUserId",
                table: "AwayMatchTeamGroupPlayer",
                column: "ClubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Club_AddressId",
                table: "Club",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscriber_ClubSubscriptionId",
                table: "ClubSubscriber",
                column: "ClubSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscriber_ClubUserId",
                table: "ClubSubscriber",
                column: "ClubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscription_ClubId",
                table: "ClubSubscription",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscription_SeasonId",
                table: "ClubSubscription",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_ClubId",
                table: "ClubUser",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_ClubUserStatusId",
                table: "ClubUser",
                column: "ClubUserStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_MembershipTypeId",
                table: "ClubUser",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_UserDetailId",
                table: "ClubUser",
                column: "UserDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CupMatch_CupId",
                table: "CupMatch",
                column: "CupId");

            migrationBuilder.CreateIndex(
                name: "IX_CupMatch_MatchId",
                table: "CupMatch",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_CupMatchHandicap_CupMatchId",
                table: "CupMatchHandicap",
                column: "CupMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_CupMatchHandicap_TeamId",
                table: "CupMatchHandicap",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResult_HomeMatchTeamGroupId",
                table: "HomeGameResult",
                column: "HomeMatchTeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResult_ResultTypeId",
                table: "HomeGameResult",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResultScore_ClubUserId",
                table: "HomeGameResultScore",
                column: "ClubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResultScore_HomeGameResultId",
                table: "HomeGameResultScore",
                column: "HomeGameResultId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResultScore_ScoreStatusId",
                table: "HomeGameResultScore",
                column: "ScoreStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_MatchId",
                table: "MatchTeam",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_TeamId",
                table: "MatchTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamCaptain_HomeMatchTeamGroupPlayerId",
                table: "MatchTeamCaptain",
                column: "HomeMatchTeamGroupPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamGroup_GroupId",
                table: "MatchTeamGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamGroup_HomeMatchTeamId",
                table: "MatchTeamGroup",
                column: "HomeMatchTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamGroupPlayer_ClubUserId",
                table: "MatchTeamGroupPlayer",
                column: "ClubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamGroupPlayer_HomeMatchTeamGroupId",
                table: "MatchTeamGroupPlayer",
                column: "HomeMatchTeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_League_SportId",
                table: "League",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueClub_ClubId",
                table: "LeagueClub",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueClub_LeagueId",
                table: "LeagueClub",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_AddressId",
                table: "Match",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_ResultTypeId",
                table: "Match",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_SeasonId",
                table: "Match",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_WinningTeamId",
                table: "Match",
                column: "WinningTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchScheduledDate_MatchId",
                table: "MatchScheduledDate",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Season_LeagueId",
                table: "Season",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_CategoryId",
                table: "Team",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_DivisionId",
                table: "Team",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LeagueClubId",
                table: "Team",
                column: "LeagueClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_SeasonId",
                table: "Team",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCaptain_TeamPlayerId",
                table: "TeamCaptain",
                column: "TeamPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_ClubUserId",
                table: "TeamPlayer",
                column: "ClubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_GroupId",
                table: "TeamPlayer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_TeamId",
                table: "TeamPlayer",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_AddressId",
                table: "UserDetail",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_GenderId",
                table: "UserDetail",
                column: "GenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableDay");

            migrationBuilder.DropTable(
                name: "AwardNominee");

            migrationBuilder.DropTable(
                name: "AwayGameResultScore");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamCaptain");

            migrationBuilder.DropTable(
                name: "ClubSubscriber");

            migrationBuilder.DropTable(
                name: "CupMatchHandicap");

            migrationBuilder.DropTable(
                name: "HomeGameResultScore");

            migrationBuilder.DropTable(
                name: "MatchTeamCaptain");

            migrationBuilder.DropTable(
                name: "MatchScheduledDate");

            migrationBuilder.DropTable(
                name: "TeamCaptain");

            migrationBuilder.DropTable(
                name: "Award");

            migrationBuilder.DropTable(
                name: "AwayGameResult");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamGroupPlayer");

            migrationBuilder.DropTable(
                name: "ClubSubscription");

            migrationBuilder.DropTable(
                name: "CupMatch");

            migrationBuilder.DropTable(
                name: "HomeGameResult");

            migrationBuilder.DropTable(
                name: "ScoreStatus");

            migrationBuilder.DropTable(
                name: "MatchTeamGroupPlayer");

            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamGroup");

            migrationBuilder.DropTable(
                name: "Cup");

            migrationBuilder.DropTable(
                name: "MatchTeamGroup");

            migrationBuilder.DropTable(
                name: "ClubUser");

            migrationBuilder.DropTable(
                name: "AwayMatchTeam");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "MatchTeam");

            migrationBuilder.DropTable(
                name: "ClubUserStatus");

            migrationBuilder.DropTable(
                name: "MembershipType");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "ResultType");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropTable(
                name: "LeagueClub");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Sport");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
