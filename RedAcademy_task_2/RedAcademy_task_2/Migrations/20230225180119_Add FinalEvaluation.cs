using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedAcademy_task_2.Migrations
{
    public partial class AddFinalEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinalEvaluation",
                table: "Assessaments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalEvaluation",
                table: "Assessaments");
        }
    }
}
