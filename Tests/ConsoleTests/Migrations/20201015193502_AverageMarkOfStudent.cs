using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleTests.Migrations
{
    public partial class AverageMarkOfStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageMark",
                table: "Students",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageMark",
                table: "Students");
        }
    }
}
