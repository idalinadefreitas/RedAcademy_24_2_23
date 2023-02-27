using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedAcademy_task_2.Migrations
{
    public partial class floatFinalEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "FinalEvaluation",
                table: "Assessments",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FinalEvaluation",
                table: "Assessments",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
