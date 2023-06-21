using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share.Migrations
{
    public partial class fixOrder25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDentity_UserDentityId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserDentityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SumPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserDentityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsFreeShip",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SumPrice",
                table: "Orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UserDentityId",
                table: "Orders",
                type: "varchar(95)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeShip",
                table: "OrderDetails",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderDetails",
                type: "longtext",
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
    }
}
