using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Migrations
{
    public partial class fixauthen35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "5ffa1b7e-1894-4a75-a2d2-b522d370bc38", "fa3d69b2-ff5f-46b0-8066-2a9d20cebb7b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6e7eff8-0909-4a81-862b-60127536a025", "9252bae5-3642-40a1-bba2-642aa54e7d4b", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb3ee8e4-f8da-49da-b7f8-2e85fb31161c", "cb1f39f6-2d5b-4fe3-8d1a-3ac180a83580", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ffa1b7e-1894-4a75-a2d2-b522d370bc38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6e7eff8-0909-4a81-862b-60127536a025");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb3ee8e4-f8da-49da-b7f8-2e85fb31161c");

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
    }
}
