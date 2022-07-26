using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showcase.Infrastructure.Persistence.Database.Migrations
{
    public partial class AddCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Coordinates_Latitude",
                table: "Temperatures",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Coordinates_Longitude",
                table: "Temperatures",
                type: "REAL",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates_Latitude",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "Coordinates_Longitude",
                table: "Temperatures");
        }
    }
}
