using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2eac7e91-0c06-468b-89d8-8a87d5e5922f", "bdcfb0e2-68f1-4963-b7f4-14a98afdd9c4", "User", "USER" },
                    { "e350b4d1-eee8-4b82-8bfd-cfb72f7963b6", "82802498-47a9-4905-ba96-b79886f912db", "Admin", "ADMIN" },
                    { "e8762975-737d-4816-b80c-3e29f68676a0", "0db51f0e-f7b0-4d5d-8062-c5d17fa832a6", "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2eac7e91-0c06-468b-89d8-8a87d5e5922f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e350b4d1-eee8-4b82-8bfd-cfb72f7963b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8762975-737d-4816-b80c-3e29f68676a0");
        }
    }
}
