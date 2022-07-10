using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buzzword.Application.Domain.Migrations
{
    public partial class AddUserWordTranslate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Translate",
                table: "UserWord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Translate",
                table: "UserWord");
        }
    }
}
