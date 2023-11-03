using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "57a20b50-e0a6-4d6a-8c34-b399591d32fc", "fdfe2427-bef7-4cd5-8868-823ccd3f8285", "Admin", "ADMIN" },
                    { "bb6ef126-1462-431c-846b-a08fee1fa9ec", "edefe35e-8197-43a4-ad12-0b786d1683d6", "Editor", "EDITOR" },
                    { "d89a3190-51f3-424e-bce5-40b658465442", "6bc98660-f29b-45f9-a340-55aff2e7503f", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57a20b50-e0a6-4d6a-8c34-b399591d32fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb6ef126-1462-431c-846b-a08fee1fa9ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d89a3190-51f3-424e-bce5-40b658465442");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers");

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
    }
}
