using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Migrations
{
    public partial class fixauthModelchana6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "66708b55-3fda-4289-933b-7f9ff39a3d32", "0487f333-6bfc-404c-83b9-e3194e997511", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa9ac6d6-4c2a-41f8-a43c-e6013c52d5bc", "3fd63380-8bfa-4f1a-9962-4a77ca4adba5", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fadb4a29-318f-432a-87bf-4dd3f0a49877", "c7ae6262-df4d-4a3a-8f6e-cde14acfad03", "SuperAdmin", "SUPERADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66708b55-3fda-4289-933b-7f9ff39a3d32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa9ac6d6-4c2a-41f8-a43c-e6013c52d5bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fadb4a29-318f-432a-87bf-4dd3f0a49877");

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
    }
}
