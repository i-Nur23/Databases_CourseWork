using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASCARBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedWinsToPilots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "PILOT",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "PILOT",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "Wins",
                table: "PILOT",
                sql: "Wins >= 0 AND Wins <= 36");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Wins",
                table: "PILOT");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "PILOT");

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "PILOT",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
