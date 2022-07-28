using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showcase.Infrastructure.Persistence.Database.Migrations
{
    public partial class AddPressure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pressures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Coordinates_Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Coordinates_Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pressures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pressures");
        }
    }
}
