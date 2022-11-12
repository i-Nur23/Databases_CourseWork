using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASCARBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedFKBetweenChangeAndStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CHANGE_StageNumber",
                table: "CHANGE",
                column: "StageNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CHANGE_STAGE_StageNumber",
                table: "CHANGE",
                column: "StageNumber",
                principalTable: "STAGE",
                principalColumn: "StageNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHANGE_STAGE_StageNumber",
                table: "CHANGE");

            migrationBuilder.DropIndex(
                name: "IX_CHANGE_StageNumber",
                table: "CHANGE");
        }
    }
}
