using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaremintShop.Module.Catalog.Migrations
{
    /// <inheritdoc />
    public partial class AddAltTextToProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImages",
                newName: "ImagePath");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "ProductImages",
                newName: "ImageUrl");
        }
    }
}
