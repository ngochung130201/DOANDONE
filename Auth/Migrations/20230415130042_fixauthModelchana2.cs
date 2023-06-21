using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Migrations
{
    public partial class fixauthModelchana2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b621ce6-0415-443f-a95f-c26f820b126f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "684721fc-cb04-4464-a22a-3a5ba13fa01b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aabfba7b-b59a-4444-b80b-8a3c4f9b4083");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "AspNetUsers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3412c2df-5f14-4064-9d63-0b2bc2f60d57", "2f4c7001-aa5a-4efa-bd82-2c939936ba81", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "612857d8-a63c-4640-9720-1b3203bee06b", "631a4841-9f4b-43da-9499-15573d813be8", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e16e39c-7a7d-42bb-9887-0e133a9ab0ae", "8e1cc74c-69dc-4016-8531-b7b689d868b0", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3412c2df-5f14-4064-9d63-0b2bc2f60d57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "612857d8-a63c-4640-9720-1b3203bee06b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e16e39c-7a7d-42bb-9887-0e133a9ab0ae");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b621ce6-0415-443f-a95f-c26f820b126f", "aa747a68-268e-4b81-bab0-cc91dbf6c2fa", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "684721fc-cb04-4464-a22a-3a5ba13fa01b", "36f14816-46e4-495c-a878-862751889ff3", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aabfba7b-b59a-4444-b80b-8a3c4f9b4083", "506b9fe8-58c5-48e6-aad3-e89590a1b9d4", "User", "USER" });
        }
    }
}
