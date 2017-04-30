using System;
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
                name: "ClubPlayerStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPlayerStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayOfTheWeek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfTheWeek", x => x.Id);
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
                name: "MatchType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchType", x => x.Id);
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
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
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
                name: "TeamStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_PlayerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerDetail_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SettingId = table.Column<int>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rule_Setting_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Setting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    VenueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Club_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    RuleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryRule_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryRule_Rule_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    LeagueId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cup_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeagueRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<int>(nullable: true),
                    RuleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueRule_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeagueRule_Rule_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rule",
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
                name: "ClubPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubId = table.Column<int>(nullable: true),
                    ClubPlayerStatusId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MembershipNumber = table.Column<string>(nullable: true),
                    MembershipTypeId = table.Column<int>(nullable: true),
                    PlayerDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubPlayer_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubPlayer_ClubPlayerStatus_ClubPlayerStatusId",
                        column: x => x.ClubPlayerStatusId,
                        principalTable: "ClubPlayerStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubPlayer_MembershipType_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MembershipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubPlayer_PlayerDetail_PlayerDetailId",
                        column: x => x.PlayerDetailId,
                        principalTable: "PlayerDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClubVenue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubId = table.Column<int>(nullable: true),
                    DayOfTheWeekId = table.Column<int>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    MaxMatches = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    VenueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubVenue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubVenue_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubVenue_DayOfTheWeek_DayOfTheWeekId",
                        column: x => x.DayOfTheWeekId,
                        principalTable: "DayOfTheWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubVenue_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchTypeId = table.Column<int>(nullable: true),
                    SeasonId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    VenueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_MatchType_MatchTypeId",
                        column: x => x.MatchTypeId,
                        principalTable: "MatchType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PointValue = table.Column<int>(nullable: false),
                    ResultTypeId = table.Column<int>(nullable: true),
                    SeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointScore_ResultType_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PointScore_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExclusionDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateToExclude = table.Column<DateTime>(nullable: false),
                    LeagueClubId = table.Column<int>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    SeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExclusionDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExclusionDate_LeagueClub_LeagueClubId",
                        column: x => x.LeagueClubId,
                        principalTable: "LeagueClub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExclusionDate_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    SeasonId = table.Column<int>(nullable: true),
                    TeamStatusId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Team_TeamStatus_TeamStatusId",
                        column: x => x.TeamStatusId,
                        principalTable: "TeamStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClubSubscriber",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    ClubSubscriptionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubSubscriber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubSubscriber_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubSubscriber_ClubSubscription_ClubSubscriptionId",
                        column: x => x.ClubSubscriptionId,
                        principalTable: "ClubSubscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwardNominee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwardId = table.Column<int>(nullable: true),
                    ClubPlayerId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwardNominee_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
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
                name: "RescheduledStartDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RescheduledStartDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RescheduledStartDate_Match_MatchId",
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
                    MatchId = table.Column<int>(nullable: false),
                    ResultTypeId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    TeamStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayMatchTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeam_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeam_ResultType_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeam_TeamStatus_TeamStatusId",
                        column: x => x.TeamStatusId,
                        principalTable: "TeamStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeMatchTeam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: false),
                    ResultTypeId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    TeamStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeMatchTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeam_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeam_ResultType_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeam_TeamStatus_TeamStatusId",
                        column: x => x.TeamStatusId,
                        principalTable: "TeamStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
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
                name: "HomeMatchTeamGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: true),
                    HomeMatchTeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeMatchTeamGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamGroup_HomeMatchTeam_HomeMatchTeamId",
                        column: x => x.HomeMatchTeamId,
                        principalTable: "HomeMatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ClubPlayerId = table.Column<int>(nullable: true)
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
                        name: "FK_AwayMatchTeamGroupPlayer_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
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
                        name: "FK_HomeGameResult_HomeMatchTeamGroup_HomeMatchTeamGroupId",
                        column: x => x.HomeMatchTeamGroupId,
                        principalTable: "HomeMatchTeamGroup",
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
                name: "HomeMatchTeamGroupPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    HomeMatchTeamGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeMatchTeamGroupPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamGroupPlayer_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamGroupPlayer_HomeMatchTeamGroup_HomeMatchTeamGroupId",
                        column: x => x.HomeMatchTeamGroupId,
                        principalTable: "HomeMatchTeamGroup",
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
                    ClubPlayerId = table.Column<int>(nullable: true),
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
                        name: "FK_AwayGameResultScore_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
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
                    ClubPlayerId = table.Column<int>(nullable: true),
                    DateSubmitted = table.Column<DateTime>(nullable: false),
                    HomeGameResultId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: true),
                    ScoreStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeGameResultScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeGameResultScore_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
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
                name: "HomeMatchTeamCaptain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HomeMatchTeamGroupPlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeMatchTeamCaptain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamCaptain_HomeMatchTeamGroupPlayer_HomeMatchTeamGroupPlayerId",
                        column: x => x.HomeMatchTeamGroupPlayerId,
                        principalTable: "HomeMatchTeamGroupPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwardNominee_AwardId",
                table: "AwardNominee",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardNominee_ClubPlayerId",
                table: "AwardNominee",
                column: "ClubPlayerId");

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
                name: "IX_AwayGameResultScore_ClubPlayerId",
                table: "AwayGameResultScore",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayGameResultScore_ScoreStatusId",
                table: "AwayGameResultScore",
                column: "ScoreStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_MatchId",
                table: "AwayMatchTeam",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_ResultTypeId",
                table: "AwayMatchTeam",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_TeamId",
                table: "AwayMatchTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_TeamStatusId",
                table: "AwayMatchTeam",
                column: "TeamStatusId");

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
                name: "IX_AwayMatchTeamGroupPlayer_ClubPlayerId",
                table: "AwayMatchTeamGroupPlayer",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRule_CategoryId",
                table: "CategoryRule",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRule_RuleId",
                table: "CategoryRule",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Club_VenueId",
                table: "Club",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayer_ClubId",
                table: "ClubPlayer",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayer_ClubPlayerStatusId",
                table: "ClubPlayer",
                column: "ClubPlayerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayer_MembershipTypeId",
                table: "ClubPlayer",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayer_PlayerDetailId",
                table: "ClubPlayer",
                column: "PlayerDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscriber_ClubPlayerId",
                table: "ClubSubscriber",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscriber_ClubSubscriptionId",
                table: "ClubSubscriber",
                column: "ClubSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscription_ClubId",
                table: "ClubSubscription",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubSubscription_SeasonId",
                table: "ClubSubscription",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubVenue_ClubId",
                table: "ClubVenue",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubVenue_DayOfTheWeekId",
                table: "ClubVenue",
                column: "DayOfTheWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubVenue_VenueId",
                table: "ClubVenue",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Cup_LeagueId",
                table: "Cup",
                column: "LeagueId");

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
                name: "IX_ExclusionDate_LeagueClubId",
                table: "ExclusionDate",
                column: "LeagueClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ExclusionDate_SeasonId",
                table: "ExclusionDate",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResult_HomeMatchTeamGroupId",
                table: "HomeGameResult",
                column: "HomeMatchTeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResult_ResultTypeId",
                table: "HomeGameResult",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResultScore_ClubPlayerId",
                table: "HomeGameResultScore",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResultScore_HomeGameResultId",
                table: "HomeGameResultScore",
                column: "HomeGameResultId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGameResultScore_ScoreStatusId",
                table: "HomeGameResultScore",
                column: "ScoreStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeam_MatchId",
                table: "HomeMatchTeam",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeam_ResultTypeId",
                table: "HomeMatchTeam",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeam_TeamId",
                table: "HomeMatchTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeam_TeamStatusId",
                table: "HomeMatchTeam",
                column: "TeamStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeamCaptain_HomeMatchTeamGroupPlayerId",
                table: "HomeMatchTeamCaptain",
                column: "HomeMatchTeamGroupPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeamGroup_GroupId",
                table: "HomeMatchTeamGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeamGroup_HomeMatchTeamId",
                table: "HomeMatchTeamGroup",
                column: "HomeMatchTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeamGroupPlayer_ClubPlayerId",
                table: "HomeMatchTeamGroupPlayer",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeamGroupPlayer_HomeMatchTeamGroupId",
                table: "HomeMatchTeamGroupPlayer",
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
                name: "IX_LeagueRule_LeagueId",
                table: "LeagueRule",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueRule_RuleId",
                table: "LeagueRule",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatchTypeId",
                table: "Match",
                column: "MatchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_SeasonId",
                table: "Match",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_VenueId",
                table: "Match",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDetail_GenderId",
                table: "PlayerDetail",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PointScore_ResultTypeId",
                table: "PointScore",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PointScore_SeasonId",
                table: "PointScore",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RescheduledStartDate_MatchId",
                table: "RescheduledStartDate",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_SettingId",
                table: "Rule",
                column: "SettingId");

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
                name: "IX_Team_TeamStatusId",
                table: "Team",
                column: "TeamStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCaptain_TeamPlayerId",
                table: "TeamCaptain",
                column: "TeamPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_ClubPlayerId",
                table: "TeamPlayer",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_GroupId",
                table: "TeamPlayer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_TeamId",
                table: "TeamPlayer",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardNominee");

            migrationBuilder.DropTable(
                name: "AwayGameResultScore");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamCaptain");

            migrationBuilder.DropTable(
                name: "CategoryRule");

            migrationBuilder.DropTable(
                name: "ClubSubscriber");

            migrationBuilder.DropTable(
                name: "ClubVenue");

            migrationBuilder.DropTable(
                name: "CupMatchHandicap");

            migrationBuilder.DropTable(
                name: "ExclusionDate");

            migrationBuilder.DropTable(
                name: "HomeGameResultScore");

            migrationBuilder.DropTable(
                name: "HomeMatchTeamCaptain");

            migrationBuilder.DropTable(
                name: "LeagueRule");

            migrationBuilder.DropTable(
                name: "PointScore");

            migrationBuilder.DropTable(
                name: "RescheduledStartDate");

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
                name: "DayOfTheWeek");

            migrationBuilder.DropTable(
                name: "CupMatch");

            migrationBuilder.DropTable(
                name: "HomeGameResult");

            migrationBuilder.DropTable(
                name: "ScoreStatus");

            migrationBuilder.DropTable(
                name: "HomeMatchTeamGroupPlayer");

            migrationBuilder.DropTable(
                name: "Rule");

            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamGroup");

            migrationBuilder.DropTable(
                name: "Cup");

            migrationBuilder.DropTable(
                name: "HomeMatchTeamGroup");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ClubPlayer");

            migrationBuilder.DropTable(
                name: "AwayMatchTeam");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "HomeMatchTeam");

            migrationBuilder.DropTable(
                name: "ClubPlayerStatus");

            migrationBuilder.DropTable(
                name: "MembershipType");

            migrationBuilder.DropTable(
                name: "PlayerDetail");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "ResultType");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "MatchType");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropTable(
                name: "LeagueClub");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "TeamStatus");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropTable(
                name: "Venue");

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
