using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class addstaticnumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numbers",
                table: "Games",
                newName: "StaticNumbers");

            migrationBuilder.AddColumn<string>(
                name: "SolvedNumbers",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolvedNumbers",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "StaticNumbers",
                table: "Games",
                newName: "Numbers");
        }
    }
}
