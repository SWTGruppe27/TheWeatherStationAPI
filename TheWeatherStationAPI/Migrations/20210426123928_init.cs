using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheWeatherStationAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherObservations",
                columns: table => new
                {
                    TemperatureReadingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    AirPressure = table.Column<double>(type: "float", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherObservations", x => x.TemperatureReadingId);
                    table.ForeignKey(
                        name: "FK_WeatherObservations_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherObservations_StationId",
                table: "WeatherObservations",
                column: "StationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherObservations");

            migrationBuilder.DropTable(
                name: "Station");
        }
    }
}
