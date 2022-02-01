using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class addmaxlenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StaticNumbers",
                table: "Games",
                type: "nvarchar(81)",
                maxLength: 81,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SolvedNumbers",
                table: "Games",
                type: "nvarchar(81)",
                maxLength: 81,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StaticNumbers",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(81)",
                oldMaxLength: 81);

            migrationBuilder.AlterColumn<string>(
                name: "SolvedNumbers",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(81)",
                oldMaxLength: 81);
        }
    }
}
