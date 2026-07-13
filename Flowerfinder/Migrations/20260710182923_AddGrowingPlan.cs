using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowerfinder.Migrations
{
    /// <inheritdoc />
    public partial class AddGrowingPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GrowingPlan",
                table: "Flowers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrowingPlan",
                table: "Flowers");
        }
    }
}
