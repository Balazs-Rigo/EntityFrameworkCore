using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddOneToOneFluentBookAndBookDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Fluent_Books_Fluent_BookBook_Id",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookDetails_Books_Book_Id",
                table: "Fluent_BookDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_Books_BookDetails_BookDetail_Id1",
                table: "Fluent_Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_Books_Publisher_Publisher_Id1",
                table: "Fluent_Books");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_Books_BookDetail_Id1",
                table: "Fluent_Books");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_Books_Publisher_Id1",
                table: "Fluent_Books");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_BookDetails_Book_Id",
                table: "Fluent_BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_Fluent_BookBook_Id",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "BookDetail_Id1",
                table: "Fluent_Books");

            migrationBuilder.DropColumn(
                name: "Publisher_Id",
                table: "Fluent_Books");

            migrationBuilder.DropColumn(
                name: "Publisher_Id1",
                table: "Fluent_Books");

            migrationBuilder.DropColumn(
                name: "Book_Id",
                table: "Fluent_BookDetails");

            migrationBuilder.DropColumn(
                name: "Fluent_BookBook_Id",
                table: "BookAuthors");

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_BookDetail_Id",
                table: "Fluent_Books",
                column: "BookDetail_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_Books_Fluent_BookDetails_BookDetail_Id",
                table: "Fluent_Books",
                column: "BookDetail_Id",
                principalTable: "Fluent_BookDetails",
                principalColumn: "BookDetail_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_Books_Fluent_BookDetails_BookDetail_Id",
                table: "Fluent_Books");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_Books_BookDetail_Id",
                table: "Fluent_Books");

            migrationBuilder.AddColumn<int>(
                name: "BookDetail_Id1",
                table: "Fluent_Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Publisher_Id",
                table: "Fluent_Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Publisher_Id1",
                table: "Fluent_Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Book_Id",
                table: "Fluent_BookDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fluent_BookBook_Id",
                table: "BookAuthors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_BookDetail_Id1",
                table: "Fluent_Books",
                column: "BookDetail_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_Publisher_Id1",
                table: "Fluent_Books",
                column: "Publisher_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_BookDetails_Book_Id",
                table: "Fluent_BookDetails",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_Fluent_BookBook_Id",
                table: "BookAuthors",
                column: "Fluent_BookBook_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Fluent_Books_Fluent_BookBook_Id",
                table: "BookAuthors",
                column: "Fluent_BookBook_Id",
                principalTable: "Fluent_Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookDetails_Books_Book_Id",
                table: "Fluent_BookDetails",
                column: "Book_Id",
                principalTable: "Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_Books_BookDetails_BookDetail_Id1",
                table: "Fluent_Books",
                column: "BookDetail_Id1",
                principalTable: "BookDetails",
                principalColumn: "BookDetail_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_Books_Publisher_Publisher_Id1",
                table: "Fluent_Books",
                column: "Publisher_Id1",
                principalTable: "Publisher",
                principalColumn: "Publisher_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
