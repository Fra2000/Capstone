using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneBack.Migrations
{
    /// <inheritdoc />
    public partial class CorrezioneStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$o.kYf3pOYDuVvqF3Usze7OKoxOUaW5BhtwYqmkgAe1p2ZrHQfzWIq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPurchasedBooks",
                columns: table => new
                {
                    UserPurchasedBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPurchasedBooks", x => x.UserPurchasedBookId);
                    table.ForeignKey(
                        name: "FK_UserPurchasedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPurchasedBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$6xS.BexcZuveJl1rPcaSs.Yn04gvFez24cjmFNplXsqHHw4MsVrNS");

            migrationBuilder.CreateIndex(
                name: "IX_UserPurchasedBooks_BookId",
                table: "UserPurchasedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPurchasedBooks_UserId",
                table: "UserPurchasedBooks",
                column: "UserId");
        }
    }
}
