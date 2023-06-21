using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Migrations
{
    public partial class fixauthModelchana5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "4ceb6505-25ef-4eaf-83c9-97b943c1b082", "cd488b1d-93d0-484b-a9a7-8ee21e67db65", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f9576dd-d7f9-4d9e-9868-b9b082b1af35", "2bc6bde8-f16e-4ce1-9822-0b787418f83c", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7608024-b00a-4d13-bfa5-7ab4f0d8bf13", "8a4ec69a-4fc6-45bf-bced-cdb9f5239180", "SuperAdmin", "SUPERADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ceb6505-25ef-4eaf-83c9-97b943c1b082");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f9576dd-d7f9-4d9e-9868-b9b082b1af35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7608024-b00a-4d13-bfa5-7ab4f0d8bf13");

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
    }
}
