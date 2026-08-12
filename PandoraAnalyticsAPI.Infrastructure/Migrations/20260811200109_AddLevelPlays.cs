using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PandoraAnalyticsAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelPlays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelPlays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<string>(type: "text", nullable: false),
                    PlayerId = table.Column<string>(type: "text", nullable: false),
                    Minigame = table.Column<string>(type: "text", nullable: false),
                    LevelNumber = table.Column<int>(type: "integer", nullable: false),
                    SuccessfulTrials = table.Column<int>(type: "integer", nullable: false),
                    RequiredTrials = table.Column<int>(type: "integer", nullable: false),
                    NormalPass = table.Column<bool>(type: "boolean", nullable: false),
                    AssistedPass = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveDurationMs = table.Column<int>(type: "integer", nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelPlays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelPlays_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelPlays_EventId",
                table: "LevelPlays",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LevelPlays_PlayerId",
                table: "LevelPlays",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelPlays");
        }
    }
}
