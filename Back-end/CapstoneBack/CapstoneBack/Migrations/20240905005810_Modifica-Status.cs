using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneBack.Migrations
{
    /// <inheritdoc />
    public partial class ModificaStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPages",
                table: "UserBookStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$SbGEttHg1jj0owW00pf0HeNOOPXR1gBhR9VnjVPTL/6GEhyZLNqG.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPages",
                table: "UserBookStatuses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$o.kYf3pOYDuVvqF3Usze7OKoxOUaW5BhtwYqmkgAe1p2ZrHQfzWIq");
        }
    }
}
