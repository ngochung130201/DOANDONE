using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share.Migrations
{
    public partial class fixallmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductComments",
                newName: "ProductCommentName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductCategories",
                newName: "ProductCateName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PostComments",
                newName: "PostCommentName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PostCategories",
                newName: "PostCateName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brands",
                newName: "BrandName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductCommentName",
                table: "ProductComments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductCateName",
                table: "ProductCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PostCommentName",
                table: "PostComments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PostCateName",
                table: "PostCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brands",
                newName: "Name");
        }
    }
}
