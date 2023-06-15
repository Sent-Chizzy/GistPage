using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GistPage.Migrations
{
    public partial class SettingAddedTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Settings");
        }
    }
}
