using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectEntityFramework.Data.Migrations
{
    public partial class addedMonths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Chores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Chores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
