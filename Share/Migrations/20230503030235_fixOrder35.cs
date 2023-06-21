using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share.Migrations
{
    public partial class fixOrder35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "UserDentity",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "UserDentityId",
                table: "Orders",
                type: "varchar(95)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserDentityId",
                table: "Orders",
                column: "UserDentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserDentity_UserDentityId",
                table: "Orders",
                column: "UserDentityId",
                principalTable: "UserDentity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDentity_UserDentityId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserDentityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "UserDentity");

            migrationBuilder.DropColumn(
                name: "UserDentityId",
                table: "Orders");
        }
    }
}
