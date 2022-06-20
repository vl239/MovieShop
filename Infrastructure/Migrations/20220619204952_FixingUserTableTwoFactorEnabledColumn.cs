using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FixingUserTableTwoFactorEnabledColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwoFactorEnables",
                table: "Users",
                newName: "TwoFactorEnabled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "Users",
                newName: "TwoFactorEnables");
        }
    }
}
