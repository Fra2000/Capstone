using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneBack.Migrations
{
    /// <inheritdoc />
    public partial class ModificheAiCampiUserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$TM3ZvRJIuAaKsMt/l/m7JeJ7LMs0H.h5my/3oe3BF0YNWjnbDb8m.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$jLFeG3Tc60QXNsNgftBbcuJifq3Q46zkS11OscdEaEuECbyLm9IkK");
        }
    }
}
