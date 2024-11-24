using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_NZWalks.API.Migrations.NZWalksDb
{
    /// <inheritdoc />
    public partial class Modified_Walks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Walks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Walks");
        }
    }
}
