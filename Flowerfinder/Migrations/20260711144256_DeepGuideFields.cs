using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowerfinder.Migrations
{
    /// <inheritdoc />
    public partial class DeepGuideFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CareCalendar",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Faqs",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GearList",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuideSections",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Problems",
                table: "Flowers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CareCalendar",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Faqs",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "GearList",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "GuideSections",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Problems",
                table: "Flowers");
        }
    }
}
