using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedAcademy_task_2.Migrations.AppIdentity
{
    public partial class addpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PaymentType = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseSalaryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DisplacementValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLiquid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyIRSValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SocialSecurityValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
