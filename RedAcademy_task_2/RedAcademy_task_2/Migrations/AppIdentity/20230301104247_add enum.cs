using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedAcademy_task_2.Migrations.AppIdentity
{
    public partial class addenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Marketings",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Columnist",
                table: "Marketings",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Marketings");

            migrationBuilder.DropColumn(
                name: "Columnist",
                table: "Marketings");
        }
    }
}
