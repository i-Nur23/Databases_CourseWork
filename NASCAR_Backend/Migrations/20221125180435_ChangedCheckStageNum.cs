using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASCARBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCheckStageNum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropCheckConstraint(
                name: "StageNumber",
                table: "CHANGE");

            migrationBuilder.AddCheckConstraint(
                name: "StageNumber",
                table: "CHANGE",
                sql: "StageNumber >= 1 AND StageNumber <= 36");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "StageNumber",
                table: "CHANGE");

            migrationBuilder.AddCheckConstraint(
                name: "StageNumber",
                table: "CHANGE",
                sql: "StageNumber >= 0 AND StageNumber <= 36");
        }
    }
}
