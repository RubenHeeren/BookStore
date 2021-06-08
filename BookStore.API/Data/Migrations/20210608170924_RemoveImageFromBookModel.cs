using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.API.Data.Migrations
{
    public partial class RemoveImageFromBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Books",
                type: "TEXT",
                nullable: true);
        }
    }
}
