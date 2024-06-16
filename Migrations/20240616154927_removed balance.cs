using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlifTestTask.Migrations
{
    public partial class removedbalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Users",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
