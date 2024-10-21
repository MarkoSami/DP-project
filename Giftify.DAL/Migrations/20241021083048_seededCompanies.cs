using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Giftify.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seededCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Country", "Fax", "Name", "Phone", "PostalCode", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Cairo", "Egypt", null, "Walmart", "01264543232", "123123", "21 Imaginary street" },
                    { 2, "Cairo", "Egypt", null, "Karfour", "012645213232", "125123", "26 Imaginary street" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
