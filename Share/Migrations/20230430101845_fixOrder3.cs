using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share.Migrations
{
    public partial class fixOrder3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SumPrice",
                table: "OrderDetails");

            migrationBuilder.AddColumn<double>(
                name: "SumPrice",
                table: "Orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SumPrice",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "SumPrice",
                table: "OrderDetails",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
