using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todos.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserWithHashedPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "AQAAAAIAAYagAAAAECQBra+UW7+4VKD564iceOgzkpsFyJHKqlvLTPTqr2BleW9WZb53NvztHm+5hGuP+g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "admin");
        }
    }
}
