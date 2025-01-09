using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAppWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Checkouts");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Books",
                newName: "ReleaseDate");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Books",
                newName: "ReleaseYear");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Checkouts",
                type: "int",
                nullable: true);
        }
    }
}
