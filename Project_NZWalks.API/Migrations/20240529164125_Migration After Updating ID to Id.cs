using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAfterUpdatingIDtoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Walks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Regions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Difficulties",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Walks",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Regions",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Difficulties",
                newName: "ID");
        }
    }
}
