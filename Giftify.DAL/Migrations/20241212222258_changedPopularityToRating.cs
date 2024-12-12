using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Giftify.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedPopularityToRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Popularity",
                table: "Books",
                newName: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Books",
                newName: "Popularity");
        }
    }
}
