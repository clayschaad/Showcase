using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Showcase.Infrastructure.Persistence.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Coordinates_Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Coordinates_Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pressure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LocationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pressure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pressure_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LocationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperature_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pressure_LocationId",
                table: "Pressure",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Temperature_LocationId",
                table: "Temperature",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pressure");

            migrationBuilder.DropTable(
                name: "Temperature");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
