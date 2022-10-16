using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASCARBackend.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MANUFACTURER",
                columns: table => new
                {
                    ManufacturerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "varchar(15)", nullable: false),
                    Model = table.Column<string>(type: "varchar(10)", nullable: false),
                    BrandsCountry = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANUFACTURER", x => x.ManufacturerID);
                    table.UniqueConstraint("AK_MANUFACTURER_Brand", x => x.Brand);
                    table.UniqueConstraint("AK_MANUFACTURER_Model", x => x.Model);
                    table.CheckConstraint("Brand", "Brand IN ('Chevrolet', 'Ford', 'Toyota')");
                });

            migrationBuilder.CreateTable(
                name: "TRACK",
                columns: table => new
                {
                    TrackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TracksName = table.Column<string>(type: "varchar(15)", nullable: false),
                    TracksType = table.Column<string>(type: "char(2)", nullable: false),
                    TracksState = table.Column<string>(type: "nvarchar(18)", nullable: false),
                    TracksCity = table.Column<string>(type: "nvarchar(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRACK", x => x.TrackID);
                    table.UniqueConstraint("AK_TRACK_TracksName", x => x.TracksName);
                    table.CheckConstraint("TracksType", "TracksType IN ('SS', 'ST', 'RC')");
                });

            migrationBuilder.CreateTable(
                name: "TEAM",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamsName = table.Column<string>(type: "varchar(60)", nullable: false),
                    FoundationYear = table.Column<int>(type: "int", nullable: false),
                    Founder = table.Column<string>(type: "varchar(25)", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEAM", x => x.TeamID);
                    table.UniqueConstraint("AK_TEAM_TeamsName", x => x.TeamsName);
                    table.ForeignKey(
                        name: "FK_TEAM_MANUFACTURER_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "MANUFACTURER",
                        principalColumn: "ManufacturerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAGE",
                columns: table => new
                {
                    StageNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StagesName = table.Column<string>(type: "varchar(30)", nullable: false),
                    EventsDate = table.Column<DateTime>(type: "date", nullable: false),
                    TrackID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAGE", x => x.StageNumber);
                    table.UniqueConstraint("AK_STAGE_EventsDate", x => x.EventsDate);
                    table.UniqueConstraint("AK_STAGE_StagesName", x => x.StagesName);
                    table.ForeignKey(
                        name: "FK_STAGE_TRACK_TrackID",
                        column: x => x.TrackID,
                        principalTable: "TRACK",
                        principalColumn: "TrackID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PILOT",
                columns: table => new
                {
                    PilotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(15)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(20)", nullable: false),
                    BirthCountry = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    BirthState = table.Column<string>(type: "nvarchar(18)", nullable: true),
                    BirthCity = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    PerformanceStatus = table.Column<string>(type: "varchar(3)", nullable: false),
                    PlayOffStatus = table.Column<bool>(type: "bit", nullable: false),
                    CarsNumber = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true),
                    TeamID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PILOT", x => x.PilotID);
                    table.UniqueConstraint("AK_PILOT_CarsNumber", x => x.CarsNumber);
                    table.CheckConstraint("PerformanceStatus", "PerformanceStatus IN ('OFF', 'ON', 'PT')");
                    table.ForeignKey(
                        name: "FK_PILOT_TEAM_TeamID",
                        column: x => x.TeamID,
                        principalTable: "TEAM",
                        principalColumn: "TeamID");
                });

            migrationBuilder.CreateTable(
                name: "CHANGE",
                columns: table => new
                {
                    ChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangesDate = table.Column<DateTime>(type: "date", nullable: false),
                    OldNumber = table.Column<int>(type: "int", nullable: true),
                    NewNumber = table.Column<int>(type: "int", nullable: true),
                    PilotID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHANGE", x => x.ChangeID);
                    table.ForeignKey(
                        name: "FK_CHANGE_PILOT_PilotID",
                        column: x => x.PilotID,
                        principalTable: "PILOT",
                        principalColumn: "PilotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESULT",
                columns: table => new
                {
                    PilotID = table.Column<int>(type: "int", nullable: false),
                    StageID = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    LeaderGap = table.Column<int>(type: "int", nullable: false),
                    NumberOfPitStops = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESULT", x => new { x.PilotID, x.StageID });
                    table.ForeignKey(
                        name: "FK_RESULT_PILOT_PilotID",
                        column: x => x.PilotID,
                        principalTable: "PILOT",
                        principalColumn: "PilotID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESULT_STAGE_StageID",
                        column: x => x.StageID,
                        principalTable: "STAGE",
                        principalColumn: "StageNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHANGE_PilotID",
                table: "CHANGE",
                column: "PilotID");

            migrationBuilder.CreateIndex(
                name: "IX_PILOT_TeamID",
                table: "PILOT",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_RESULT_StageID",
                table: "RESULT",
                column: "StageID");

            migrationBuilder.CreateIndex(
                name: "IX_STAGE_TrackID",
                table: "STAGE",
                column: "TrackID");

            migrationBuilder.CreateIndex(
                name: "IX_TEAM_ManufacturerId",
                table: "TEAM",
                column: "ManufacturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHANGE");

            migrationBuilder.DropTable(
                name: "RESULT");

            migrationBuilder.DropTable(
                name: "PILOT");

            migrationBuilder.DropTable(
                name: "STAGE");

            migrationBuilder.DropTable(
                name: "TEAM");

            migrationBuilder.DropTable(
                name: "TRACK");

            migrationBuilder.DropTable(
                name: "MANUFACTURER");
        }
    }
}
