using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SportEventApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PlayersCount = table.Column<int>(type: "integer", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FootballEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    StadiumName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Team1Score = table.Column<int>(type: "integer", nullable: false),
                    Team2Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballEvents_SportEvents_Id",
                        column: x => x.Id,
                        principalTable: "SportEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TennisEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CourtSurface = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SetsCount = table.Column<int>(type: "integer", nullable: false),
                    Player1Score = table.Column<int>(type: "integer", nullable: false),
                    Player2Score = table.Column<int>(type: "integer", nullable: false),
                    CurrentGame = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TennisEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TennisEvents_SportEvents_Id",
                        column: x => x.Id,
                        principalTable: "SportEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballEvents");

            migrationBuilder.DropTable(
                name: "TennisEvents");

            migrationBuilder.DropTable(
                name: "SportEvents");
        }
    }
}
