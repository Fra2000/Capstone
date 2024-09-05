using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneBack.Migrations
{
    /// <inheritdoc />
    public partial class Recensioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "ReviewText",
                table: "UserBooks");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$5t6YeaknKm8rJaokLHHKAurecPRgcSttFLoTkUfvj5RQBDuerccy.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "UserBooks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                table: "UserBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewText",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$SbGEttHg1jj0owW00pf0HeNOOPXR1gBhR9VnjVPTL/6GEhyZLNqG.");
        }
    }
}
