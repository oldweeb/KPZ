using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab3.CodeFirst.Migrations
{
    public partial class AddGroupTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Groups",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Groups");
        }
    }
}
