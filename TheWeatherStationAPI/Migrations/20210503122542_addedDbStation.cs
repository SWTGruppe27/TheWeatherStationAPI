using Microsoft.EntityFrameworkCore.Migrations;

namespace TheWeatherStationAPI.Migrations
{
    public partial class addedDbStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_WeatherObservations_WeatherObservationId",
                table: "Station");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Station",
                table: "Station");

            migrationBuilder.RenameTable(
                name: "Station",
                newName: "Stations");

            migrationBuilder.RenameIndex(
                name: "IX_Station_WeatherObservationId",
                table: "Stations",
                newName: "IX_Stations_WeatherObservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stations",
                table: "Stations",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_WeatherObservations_WeatherObservationId",
                table: "Stations",
                column: "WeatherObservationId",
                principalTable: "WeatherObservations",
                principalColumn: "WeatherObservationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_WeatherObservations_WeatherObservationId",
                table: "Stations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stations",
                table: "Stations");

            migrationBuilder.RenameTable(
                name: "Stations",
                newName: "Station");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_WeatherObservationId",
                table: "Station",
                newName: "IX_Station_WeatherObservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Station",
                table: "Station",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_WeatherObservations_WeatherObservationId",
                table: "Station",
                column: "WeatherObservationId",
                principalTable: "WeatherObservations",
                principalColumn: "WeatherObservationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
