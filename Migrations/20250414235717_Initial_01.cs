using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBC_EmployeeAPI_POC.Migrations
{
    /// <inheritdoc />
    public partial class Initial_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPay",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPay",
                table: "Employees",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
