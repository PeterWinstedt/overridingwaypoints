using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WaypointsApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Latitude", "Longitude" },
                values: new object[] { 1, 58.100000000000001, 10.9 });

            migrationBuilder.InsertData(
                table: "WaypointSettings",
                columns: new[] { "Id", "DefaultEnterDistance", "DefaultExitDistance", "DefaultMaxEnteringDeviationAngle" },
                values: new object[] { 1, 30.0, 50.0, 45.0 });

            migrationBuilder.InsertData(
                table: "Waypoints",
                columns: new[] { "Id", "Direction", "EnterDistance", "ExitDistance", "Extra", "Gate", "Length", "LocationId", "Name", "Ref" },
                values: new object[] { 1, 90.0, 35.0, 10.0, "Extra", "B", 30.0, 1, "Nösnäs", "9025002000000111" });

            migrationBuilder.InsertData(
                table: "BorderPoints",
                columns: new[] { "Id", "Latitude", "Longitude", "WaypointId" },
                values: new object[,]
                {
                    { 1, 58.100000000000001, 10.9, 1 },
                    { 2, 58.200000000000003, 10.800000000000001, 1 },
                    { 3, 58.299999999999997, 10.699999999999999, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BorderPoints",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BorderPoints",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BorderPoints",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WaypointSettings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Waypoints",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
