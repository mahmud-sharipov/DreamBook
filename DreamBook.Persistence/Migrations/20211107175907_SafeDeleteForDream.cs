using Microsoft.EntityFrameworkCore.Migrations;

namespace DreamBook.Persistence.Migrations
{
    public partial class SafeDeleteForDream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MovedToRecycleBin",
                table: "Dream",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovedToRecycleBin",
                table: "Dream");
        }
    }
}
