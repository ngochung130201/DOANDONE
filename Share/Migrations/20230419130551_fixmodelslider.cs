using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Share.Migrations
{
    public partial class fixmodelslider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tiltle",
                table: "Sliders",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Sliders",
                newName: "Tiltle");
        }
    }
}
