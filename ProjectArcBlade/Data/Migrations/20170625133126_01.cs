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

            migrationBuilder.AddColumn<int>(
                name: "PlayerDetailId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audit_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DefaultGameLossScore = table.Column<int>(nullable: false),
                    DefaultGameWinScore = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.Id);
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
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
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
                name: "GroupTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: true),
                    MatchTemplateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTemplate_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupTemplate_MatchTemplate_MatchTemplateId",
                        column: x => x.MatchTemplateId,
                        principalTable: "MatchTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SetTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchTemplateId = table.Column<int>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetTemplate_MatchTemplate_MatchTemplateId",
                        column: x => x.MatchTemplateId,
                        principalTable: "MatchTemplate",
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
                name: "ResultRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConditionId = table.Column<int>(nullable: false),
                    JoinConditionId = table.Column<int>(nullable: false),
                    OperatorId = table.Column<int>(nullable: true),
                    ResultTypeId = table.Column<int>(nullable: false),
                    RuleId = table.Column<int>(nullable: false),
                    ScoreOne = table.Column<bool>(nullable: false),
                    ScoreTwo = table.Column<bool>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultRule_Lookup_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultRule_Lookup_JoinConditionId",
                        column: x => x.JoinConditionId,
                        principalTable: "Lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResultRule_Lookup_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResultRule_Type_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultRule_Rule_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "RankTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupTemplateId = table.Column<int>(nullable: true),
                    RankId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RankTemplate_GroupTemplate_GroupTemplateId",
                        column: x => x.GroupTemplateId,
                        principalTable: "GroupTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RankTemplate_Rank_RankId",
                        column: x => x.RankId,
                        principalTable: "Rank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayGroupTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupTemplateId = table.Column<int>(nullable: true),
                    SetTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayGroupTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayGroupTemplate_GroupTemplate_GroupTemplateId",
                        column: x => x.GroupTemplateId,
                        principalTable: "GroupTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayGroupTemplate_SetTemplate_SetTemplateId",
                        column: x => x.SetTemplateId,
                        principalTable: "SetTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    SetTemplateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTemplate_SetTemplate_SetTemplateId",
                        column: x => x.SetTemplateId,
                        principalTable: "SetTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeGroupTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupTemplateId = table.Column<int>(nullable: true),
                    SetTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeGroupTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeGroupTemplate_GroupTemplate_GroupTemplateId",
                        column: x => x.GroupTemplateId,
                        principalTable: "GroupTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeGroupTemplate_SetTemplate_SetTemplateId",
                        column: x => x.SetTemplateId,
                        principalTable: "SetTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_ClubPlayer_Status_ClubPlayerStatusId",
                        column: x => x.ClubPlayerStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubPlayer_Type_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "Type",
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
                    CategoryId = table.Column<int>(nullable: true),
                    DivisionId = table.Column<int>(nullable: true),
                    MatchStatusId = table.Column<int>(nullable: true),
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
                        name: "FK_Match_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Status_MatchStatusId",
                        column: x => x.MatchStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Type_MatchTypeId",
                        column: x => x.MatchTypeId,
                        principalTable: "Type",
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
                name: "MatchTemplateLink",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    MatchTemplateId = table.Column<int>(nullable: true),
                    RuleId = table.Column<int>(nullable: true),
                    SeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTemplateLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTemplateLink_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTemplateLink_MatchTemplate_MatchTemplateId",
                        column: x => x.MatchTemplateId,
                        principalTable: "MatchTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTemplateLink_Rule_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchTemplateLink_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
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
                        name: "FK_PointScore_Type_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "Type",
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
                name: "AwayTeamScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditId = table.Column<int>(nullable: true),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Score = table.Column<int>(nullable: true),
                    ScoreStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayTeamScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayTeamScore_Audit_AuditId",
                        column: x => x.AuditId,
                        principalTable: "Audit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayTeamScore_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayTeamScore_Status_ScoreStatusId",
                        column: x => x.ScoreStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeTeamScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditId = table.Column<int>(nullable: true),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Score = table.Column<int>(nullable: true),
                    ScoreStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeTeamScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeTeamScore_Audit_AuditId",
                        column: x => x.AuditId,
                        principalTable: "Audit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeTeamScore_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeTeamScore_Status_ScoreStatusId",
                        column: x => x.ScoreStatusId,
                        principalTable: "Status",
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
                        name: "FK_Team_Status_TeamStatusId",
                        column: x => x.TeamStatusId,
                        principalTable: "Status",
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
                        name: "FK_AwayMatchTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeam_Status_TeamStatusId",
                        column: x => x.TeamStatusId,
                        principalTable: "Status",
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
                        name: "FK_HomeMatchTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeam_Status_TeamStatusId",
                        column: x => x.TeamStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamCaptain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCaptain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamCaptain_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamCaptain_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    RankId = table.Column<int>(nullable: true),
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
                        name: "FK_TeamPlayer_Rank_RankId",
                        column: x => x.RankId,
                        principalTable: "Rank",
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
                name: "AwayMatchTeamCaptain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayMatchTeamId = table.Column<int>(nullable: false),
                    ClubPlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayMatchTeamCaptain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamCaptain_AwayMatchTeam_AwayMatchTeamId",
                        column: x => x.AwayMatchTeamId,
                        principalTable: "AwayMatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamCaptain_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
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
                name: "HomeMatchTeamCaptain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubPlayerId = table.Column<int>(nullable: true),
                    HomeMatchTeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeMatchTeamCaptain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamCaptain_ClubPlayer_ClubPlayerId",
                        column: x => x.ClubPlayerId,
                        principalTable: "ClubPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamCaptain_HomeMatchTeam_HomeMatchTeamId",
                        column: x => x.HomeMatchTeamId,
                        principalTable: "HomeMatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ScoreSheet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    SignedOff = table.Column<bool>(nullable: false),
                    AwayMatchTeamId = table.Column<int>(nullable: true),
                    MatchId = table.Column<int>(nullable: true),
                    HomeMatchTeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreSheet_Audit_AuditId",
                        column: x => x.AuditId,
                        principalTable: "Audit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScoreSheet_AwayMatchTeam_AwayMatchTeamId",
                        column: x => x.AwayMatchTeamId,
                        principalTable: "AwayMatchTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScoreSheet_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScoreSheet_HomeMatchTeam_HomeMatchTeamId",
                        column: x => x.HomeMatchTeamId,
                        principalTable: "HomeMatchTeam",
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
                    ClubPlayerId = table.Column<int>(nullable: true),
                    RankId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_AwayMatchTeamGroupPlayer_Rank_RankId",
                        column: x => x.RankId,
                        principalTable: "Rank",
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
                    HomeMatchTeamGroupId = table.Column<int>(nullable: true),
                    RankId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_HomeMatchTeamGroupPlayer_Rank_RankId",
                        column: x => x.RankId,
                        principalTable: "Rank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayMatchTeamGroupId = table.Column<int>(nullable: true),
                    HomeMatchTeamGroupId = table.Column<int>(nullable: true),
                    MatchId = table.Column<int>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    SetStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Set_AwayMatchTeamGroup_AwayMatchTeamGroupId",
                        column: x => x.AwayMatchTeamGroupId,
                        principalTable: "AwayMatchTeamGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Set_HomeMatchTeamGroup_HomeMatchTeamGroupId",
                        column: x => x.HomeMatchTeamGroupId,
                        principalTable: "HomeMatchTeamGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Set_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Set_Status_SetStatusId",
                        column: x => x.SetStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoreSheetGame",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwaySocre = table.Column<int>(nullable: false),
                    HomeScore = table.Column<int>(nullable: false),
                    ScoreSheetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSheetGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreSheetGame_ScoreSheet_ScoreSheetId",
                        column: x => x.ScoreSheetId,
                        principalTable: "ScoreSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoreSheetPoint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwaySocre = table.Column<int>(nullable: false),
                    HomeScore = table.Column<int>(nullable: false),
                    ScoreSheetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSheetPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreSheetPoint_ScoreSheet_ScoreSheetId",
                        column: x => x.ScoreSheetId,
                        principalTable: "ScoreSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoreSheetSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwaySocre = table.Column<int>(nullable: false),
                    HomeScore = table.Column<int>(nullable: false),
                    ScoreSheetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSheetSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreSheetSet_ScoreSheet_ScoreSheetId",
                        column: x => x.ScoreSheetId,
                        principalTable: "ScoreSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayTeamAwayTeamScoreId = table.Column<int>(nullable: false),
                    AwayTeamHomeTeamScoreId = table.Column<int>(nullable: false),
                    HomeTeamAwayTeamScoreId = table.Column<int>(nullable: false),
                    HomeTeamHomeTeamScoreId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    SetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_AwayTeamScore_AwayTeamAwayTeamScoreId",
                        column: x => x.AwayTeamAwayTeamScoreId,
                        principalTable: "AwayTeamScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_HomeTeamScore_AwayTeamHomeTeamScoreId",
                        column: x => x.AwayTeamHomeTeamScoreId,
                        principalTable: "HomeTeamScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_AwayTeamScore_HomeTeamAwayTeamScoreId",
                        column: x => x.HomeTeamAwayTeamScoreId,
                        principalTable: "AwayTeamScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_HomeTeamScore_HomeTeamHomeTeamScoreId",
                        column: x => x.HomeTeamHomeTeamScoreId,
                        principalTable: "HomeTeamScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AwayResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    ResultTypeId = table.Column<int>(nullable: true),
                    GameId = table.Column<int>(nullable: true),
                    MatchId = table.Column<int>(nullable: true),
                    SetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwayResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwayResult_Type_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AwayResult_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwayResult_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwayResult_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    ResultTypeId = table.Column<int>(nullable: true),
                    GameId = table.Column<int>(nullable: true),
                    MatchId = table.Column<int>(nullable: true),
                    SetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeResult_Type_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeResult_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeResult_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeResult_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlayerDetailId",
                table: "AspNetUsers",
                column: "PlayerDetailId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audit_ApplicationUserId",
                table: "Audit",
                column: "ApplicationUserId");

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
                name: "IX_AwayGroupTemplate_GroupTemplateId",
                table: "AwayGroupTemplate",
                column: "GroupTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayGroupTemplate_SetTemplateId",
                table: "AwayGroupTemplate",
                column: "SetTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_MatchId",
                table: "AwayMatchTeam",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_TeamId",
                table: "AwayMatchTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeam_TeamStatusId",
                table: "AwayMatchTeam",
                column: "TeamStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeamCaptain_AwayMatchTeamId",
                table: "AwayMatchTeamCaptain",
                column: "AwayMatchTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwayMatchTeamCaptain_ClubPlayerId",
                table: "AwayMatchTeamCaptain",
                column: "ClubPlayerId");

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
                name: "IX_AwayMatchTeamGroupPlayer_RankId",
                table: "AwayMatchTeamGroupPlayer",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayResult_ResultTypeId",
                table: "AwayResult",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayResult_GameId",
                table: "AwayResult",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwayResult_MatchId",
                table: "AwayResult",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwayResult_SetId",
                table: "AwayResult",
                column: "SetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwayTeamScore_AuditId",
                table: "AwayTeamScore",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayTeamScore_ClubPlayerId",
                table: "AwayTeamScore",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AwayTeamScore_ScoreStatusId",
                table: "AwayTeamScore",
                column: "ScoreStatusId");

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
                name: "IX_Game_AwayTeamAwayTeamScoreId",
                table: "Game",
                column: "AwayTeamAwayTeamScoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_AwayTeamHomeTeamScoreId",
                table: "Game",
                column: "AwayTeamHomeTeamScoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_HomeTeamAwayTeamScoreId",
                table: "Game",
                column: "HomeTeamAwayTeamScoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_HomeTeamHomeTeamScoreId",
                table: "Game",
                column: "HomeTeamHomeTeamScoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_SetId",
                table: "Game",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTemplate_SetTemplateId",
                table: "GameTemplate",
                column: "SetTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTemplate_GroupId",
                table: "GroupTemplate",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTemplate_MatchTemplateId",
                table: "GroupTemplate",
                column: "MatchTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGroupTemplate_GroupTemplateId",
                table: "HomeGroupTemplate",
                column: "GroupTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeGroupTemplate_SetTemplateId",
                table: "HomeGroupTemplate",
                column: "SetTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeam_MatchId",
                table: "HomeMatchTeam",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeam_TeamId",
                table: "HomeMatchTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeam_TeamStatusId",
                table: "HomeMatchTeam",
                column: "TeamStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeamCaptain_ClubPlayerId",
                table: "HomeMatchTeamCaptain",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeMatchTeamCaptain_HomeMatchTeamId",
                table: "HomeMatchTeamCaptain",
                column: "HomeMatchTeamId",
                unique: true);

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
                name: "IX_HomeMatchTeamGroupPlayer_RankId",
                table: "HomeMatchTeamGroupPlayer",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeResult_ResultTypeId",
                table: "HomeResult",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeResult_GameId",
                table: "HomeResult",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeResult_MatchId",
                table: "HomeResult",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeResult_SetId",
                table: "HomeResult",
                column: "SetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeTeamScore_AuditId",
                table: "HomeTeamScore",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeTeamScore_ClubPlayerId",
                table: "HomeTeamScore",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeTeamScore_ScoreStatusId",
                table: "HomeTeamScore",
                column: "ScoreStatusId");

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
                name: "IX_Match_CategoryId",
                table: "Match",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_DivisionId",
                table: "Match",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatchStatusId",
                table: "Match",
                column: "MatchStatusId");

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
                name: "IX_MatchTemplateLink_CategoryId",
                table: "MatchTemplateLink",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTemplateLink_MatchTemplateId",
                table: "MatchTemplateLink",
                column: "MatchTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTemplateLink_RuleId",
                table: "MatchTemplateLink",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTemplateLink_SeasonId",
                table: "MatchTemplateLink",
                column: "SeasonId");

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
                name: "IX_RankTemplate_GroupTemplateId",
                table: "RankTemplate",
                column: "GroupTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RankTemplate_RankId",
                table: "RankTemplate",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_RescheduledStartDate_MatchId",
                table: "RescheduledStartDate",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRule_ConditionId",
                table: "ResultRule",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRule_JoinConditionId",
                table: "ResultRule",
                column: "JoinConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRule_OperatorId",
                table: "ResultRule",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRule_ResultTypeId",
                table: "ResultRule",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRule_RuleId",
                table: "ResultRule",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_AuditId",
                table: "ScoreSheet",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_AwayMatchTeamId",
                table: "ScoreSheet",
                column: "AwayMatchTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_MatchId",
                table: "ScoreSheet",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_HomeMatchTeamId",
                table: "ScoreSheet",
                column: "HomeMatchTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheetGame_ScoreSheetId",
                table: "ScoreSheetGame",
                column: "ScoreSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheetPoint_ScoreSheetId",
                table: "ScoreSheetPoint",
                column: "ScoreSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheetSet_ScoreSheetId",
                table: "ScoreSheetSet",
                column: "ScoreSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Season_LeagueId",
                table: "Season",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_AwayMatchTeamGroupId",
                table: "Set",
                column: "AwayMatchTeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_HomeMatchTeamGroupId",
                table: "Set",
                column: "HomeMatchTeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_MatchId",
                table: "Set",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_SetStatusId",
                table: "Set",
                column: "SetStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SetTemplate_MatchTemplateId",
                table: "SetTemplate",
                column: "MatchTemplateId");

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
                name: "IX_TeamCaptain_ClubPlayerId",
                table: "TeamCaptain",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCaptain_TeamId",
                table: "TeamCaptain",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_ClubPlayerId",
                table: "TeamPlayer",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_GroupId",
                table: "TeamPlayer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_RankId",
                table: "TeamPlayer",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_TeamId",
                table: "TeamPlayer",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PlayerDetail_PlayerDetailId",
                table: "AspNetUsers",
                column: "PlayerDetailId",
                principalTable: "PlayerDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PlayerDetail_PlayerDetailId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AwardNominee");

            migrationBuilder.DropTable(
                name: "AwayGroupTemplate");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamCaptain");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamGroupPlayer");

            migrationBuilder.DropTable(
                name: "AwayResult");

            migrationBuilder.DropTable(
                name: "ClubSubscriber");

            migrationBuilder.DropTable(
                name: "ClubVenue");

            migrationBuilder.DropTable(
                name: "CupMatchHandicap");

            migrationBuilder.DropTable(
                name: "ExclusionDate");

            migrationBuilder.DropTable(
                name: "GameTemplate");

            migrationBuilder.DropTable(
                name: "HomeGroupTemplate");

            migrationBuilder.DropTable(
                name: "HomeMatchTeamCaptain");

            migrationBuilder.DropTable(
                name: "HomeMatchTeamGroupPlayer");

            migrationBuilder.DropTable(
                name: "HomeResult");

            migrationBuilder.DropTable(
                name: "MatchTemplateLink");

            migrationBuilder.DropTable(
                name: "PointScore");

            migrationBuilder.DropTable(
                name: "RankTemplate");

            migrationBuilder.DropTable(
                name: "RescheduledStartDate");

            migrationBuilder.DropTable(
                name: "ResultRule");

            migrationBuilder.DropTable(
                name: "ScoreSheetGame");

            migrationBuilder.DropTable(
                name: "ScoreSheetPoint");

            migrationBuilder.DropTable(
                name: "ScoreSheetSet");

            migrationBuilder.DropTable(
                name: "TeamCaptain");

            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.DropTable(
                name: "Award");

            migrationBuilder.DropTable(
                name: "ClubSubscription");

            migrationBuilder.DropTable(
                name: "DayOfTheWeek");

            migrationBuilder.DropTable(
                name: "CupMatch");

            migrationBuilder.DropTable(
                name: "SetTemplate");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GroupTemplate");

            migrationBuilder.DropTable(
                name: "Lookup");

            migrationBuilder.DropTable(
                name: "Rule");

            migrationBuilder.DropTable(
                name: "ScoreSheet");

            migrationBuilder.DropTable(
                name: "Rank");

            migrationBuilder.DropTable(
                name: "Cup");

            migrationBuilder.DropTable(
                name: "AwayTeamScore");

            migrationBuilder.DropTable(
                name: "HomeTeamScore");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "MatchTemplate");

            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.DropTable(
                name: "ClubPlayer");

            migrationBuilder.DropTable(
                name: "AwayMatchTeamGroup");

            migrationBuilder.DropTable(
                name: "HomeMatchTeamGroup");

            migrationBuilder.DropTable(
                name: "PlayerDetail");

            migrationBuilder.DropTable(
                name: "AwayMatchTeam");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "HomeMatchTeam");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropTable(
                name: "LeagueClub");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropTable(
                name: "Venue");

            migrationBuilder.DropTable(
                name: "Sport");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PlayerDetailId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "PlayerDetailId",
                table: "AspNetUsers");

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
