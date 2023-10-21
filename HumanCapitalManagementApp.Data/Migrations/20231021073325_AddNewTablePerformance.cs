using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanCapitalManagementApp.Migrations
{
    public partial class AddNewTablePerformance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidOrUnpaidLeave",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LeaveRequests",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VacationOrSickLeave",
                table: "LeaveRequests",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PerformanceManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompletedТraining = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ParticipationInProjects = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ParticipationInTeamBuilding = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceManagement_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceManagement_EmployeeId",
                table: "PerformanceManagement",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceManagement");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "VacationOrSickLeave",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<string>(
                name: "PaidOrUnpaidLeave",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
