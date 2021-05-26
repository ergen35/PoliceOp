using Microsoft.EntityFrameworkCore.Migrations;

namespace PoliceOp.API.Migrations
{
    public partial class AddPasswordToAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Personnes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Personnes");
        }
    }
}
