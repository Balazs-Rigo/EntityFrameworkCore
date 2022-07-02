using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class createDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "[FirstName] + ' ' + [LastName]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseHeaders_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ExpenseHeaders_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseLines",
                columns: table => new
                {
                    ExpenseLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(16,2)", nullable: false, computedColumnSql: "[Quantity] * [UnitCost]"),
                    ExpenseHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseLines", x => x.ExpenseLineId);
                    table.ForeignKey(
                        name: "FK_ExpenseLines_ExpenseHeaders_ExpenseHeaderId",
                        column: x => x.ExpenseHeaderId,
                        principalTable: "ExpenseHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseHeaders_ApproverId",
                table: "ExpenseHeaders",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseHeaders_RequesterId",
                table: "ExpenseHeaders",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseLines_ExpenseHeaderId",
                table: "ExpenseLines",
                column: "ExpenseHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseLines");

            migrationBuilder.DropTable(
                name: "ExpenseHeaders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
