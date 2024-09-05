using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "UserBookStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "UserPurchasedBooks",
                columns: table => new
                {
                    UserPurchasedBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPurchasedBooks");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "UserBookStatuses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$4RLqrzL5DHa.SuB5siFEGeT4sANvoK0AEKLXtl9V9msg38sia789W");
        }
    }
}
