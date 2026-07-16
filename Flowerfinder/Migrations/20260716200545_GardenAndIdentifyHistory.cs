using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowerfinder.Migrations
{
    /// <inheritdoc />
    public partial class GardenAndIdentifyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInGarden",
                table: "Flowers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "IdentifyRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTaken = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    TopScientificName = table.Column<string>(type: "TEXT", nullable: true),
                    TopCommonName = table.Column<string>(type: "TEXT", nullable: true),
                    TopScorePercent = table.Column<int>(type: "INTEGER", nullable: false),
                    MatchedFlowerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Demo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifyRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentifyRecords");

            migrationBuilder.DropColumn(
                name: "IsInGarden",
                table: "Flowers");
        }
    }
}
