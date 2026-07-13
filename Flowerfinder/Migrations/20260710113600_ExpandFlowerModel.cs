using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowerfinder.Migrations
{
    /// <inheritdoc />
    public partial class ExpandFlowerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CommonName",
                table: "Flowers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloomSeason",
                table: "Flowers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Colors",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPerennial",
                table: "Flowers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NativeRegion",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoilType",
                table: "Flowers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sunlight",
                table: "Flowers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Watering",
                table: "Flowers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloomSeason",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Colors",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "IsPerennial",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "NativeRegion",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "SoilType",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Sunlight",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Watering",
                table: "Flowers");

            migrationBuilder.AlterColumn<string>(
                name: "CommonName",
                table: "Flowers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
