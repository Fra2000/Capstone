using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneBack.Migrations
{
    /// <inheritdoc />
    public partial class CorrezioneErroreUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$3wSMhv27P4k/A57/adYaiucmpS.jRaA.MkeLsfP8vyi0AES1Fg1wC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$5t6YeaknKm8rJaokLHHKAurecPRgcSttFLoTkUfvj5RQBDuerccy.");
        }
    }
}
