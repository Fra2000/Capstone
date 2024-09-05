using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace CapstoneBack.Migrations
{
    /// <inheritdoc />
    public partial class UserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Creazione della tabella UserBook
            migrationBuilder.CreateTable(
                name: "UserBooks",
                columns: table => new
                {
                    UserBookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBooks", x => x.UserBookId);
                    table.ForeignKey(
                        name: "FK_UserBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Aggiornamento della password dell'utente predefinito, se necessario
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$R.mkFG5B.Bc5k.6/8nrwje1lRY03cCyifoAwS2qJzRW726es22WUW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Elimina la tabella UserBooks in caso di rollback
            migrationBuilder.DropTable(
                name: "UserBooks");

            // Ripristina l'hash della password precedente, se necessario
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$3wSMhv27P4k/A57/adYaiucmpS.jRaA.MkeLsfP8vyi0AES1Fg1wC");
        }
    }
}
