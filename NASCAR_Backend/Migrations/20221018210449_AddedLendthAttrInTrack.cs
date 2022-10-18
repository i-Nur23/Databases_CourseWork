using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASCARBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedLendthAttrInTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TracksName",
                table: "TRACK",
                type: "varchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "TRACK",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "TRACK");

            migrationBuilder.AlterColumn<string>(
                name: "TracksName",
                table: "TRACK",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)");
        }
    }
}
