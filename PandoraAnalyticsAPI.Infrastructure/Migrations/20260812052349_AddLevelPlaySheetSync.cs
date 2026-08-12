using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PandoraAnalyticsAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelPlaySheetSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SheetSynced",
                table: "LevelPlays",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SheetSynced",
                table: "LevelPlays");
        }
    }
}
