using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangesMadeToImageAndRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Regions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Images",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("14ceba71-4b51-4777-9b17-46602cf66153"),
                column: "UserId",
                value: "3c781f81-f753-45b6-9676-7a98629fb86f");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                column: "UserId",
                value: "ae5899b0-6f9e-4e18-a804-a6045edc37d2");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                column: "UserId",
                value: "ae5899b0-6f9e-4e18-a804-a6045edc37d2");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                column: "UserId",
                value: "ae5899b0-6f9e-4e18-a804-a6045edc37d2");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                column: "UserId",
                value: "3c781f81-f753-45b6-9676-7a98629fb86f");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                column: "UserId",
                value: "3c781f81-f753-45b6-9676-7a98629fb86f");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Images");
        }
    }
}
