using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASCARBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRangeConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "Length",
                table: "TRACK",
                sql: "Length > 0");

            migrationBuilder.AddCheckConstraint(
                name: "FoundationYear",
                table: "TEAM",
                sql: "FoundationYear >= 1900 AND FoundationYear <= 2021");

            migrationBuilder.AddCheckConstraint(
                name: "LeaderGap",
                table: "RESULT",
                sql: "LeaderGap >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "NumberOfPitStops",
                table: "RESULT",
                sql: "NumberOfPitStops >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "Place",
                table: "RESULT",
                sql: "Place >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CarsNumber",
                table: "PILOT",
                sql: "CarsNumber >= 0 AND CarsNumber <= 99");

            migrationBuilder.AddCheckConstraint(
                name: "Points",
                table: "PILOT",
                sql: "Points >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "NewNumber",
                table: "CHANGE",
                sql: "NewNumber >= 0 AND NewNumber <= 99");

            migrationBuilder.AddCheckConstraint(
                name: "OldNumber",
                table: "CHANGE",
                sql: "OldNumber >= 0 AND OldNumber <= 99");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Length",
                table: "TRACK");

            migrationBuilder.DropCheckConstraint(
                name: "FoundationYear",
                table: "TEAM");

            migrationBuilder.DropCheckConstraint(
                name: "LeaderGap",
                table: "RESULT");

            migrationBuilder.DropCheckConstraint(
                name: "NumberOfPitStops",
                table: "RESULT");

            migrationBuilder.DropCheckConstraint(
                name: "Place",
                table: "RESULT");

            migrationBuilder.DropCheckConstraint(
                name: "CarsNumber",
                table: "PILOT");

            migrationBuilder.DropCheckConstraint(
                name: "Points",
                table: "PILOT");

            migrationBuilder.DropCheckConstraint(
                name: "NewNumber",
                table: "CHANGE");

            migrationBuilder.DropCheckConstraint(
                name: "OldNumber",
                table: "CHANGE");
        }
    }
}
