using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Migrations
{
    public partial class fixauthModelchana3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2335f6d7-7810-4769-97fb-c0357e70b651", "5b2b6a19-a44c-496b-ba89-c3c5ac8c5616", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81d49678-5bc3-4313-8060-704e820cbfd2", "2ded94c6-16f8-4e02-8c1d-cdf3b6e379b7", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb044c82-12a5-42c1-8b20-f615f990d745", "6fdcc168-6444-4f1a-8e0b-ab951f76d5ae", "SuperAdmin", "SUPERADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2335f6d7-7810-4769-97fb-c0357e70b651");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81d49678-5bc3-4313-8060-704e820cbfd2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb044c82-12a5-42c1-8b20-f615f990d745");

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
    }
}
