using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASCARBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedChangesDateToNearestStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangesDate",
                table: "CHANGE");

            migrationBuilder.AddColumn<int>(
                name: "StageNumber",
                table: "CHANGE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "StageNumber",
                table: "CHANGE",
                sql: "StageNumber >= 0 AND StageNumber <= 36");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "StageNumber",
                table: "CHANGE");

            migrationBuilder.DropColumn(
                name: "StageNumber",
                table: "CHANGE");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangesDate",
                table: "CHANGE",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
