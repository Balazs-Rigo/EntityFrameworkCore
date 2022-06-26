using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class expenselines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_expenseHeaders",
                table: "expenseHeaders");

            migrationBuilder.RenameTable(
                name: "expenseHeaders",
                newName: "ExpenseHeaders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseHeaders",
                table: "ExpenseHeaders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExpenseDetails",
                columns: table => new
                {
                    ExpenseLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(16,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDetails", x => x.ExpenseLineId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseHeaders",
                table: "ExpenseHeaders");

            migrationBuilder.RenameTable(
                name: "ExpenseHeaders",
                newName: "expenseHeaders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_expenseHeaders",
                table: "expenseHeaders",
                column: "Id");
        }
    }
}
