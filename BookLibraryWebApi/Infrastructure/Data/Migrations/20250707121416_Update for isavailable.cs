using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibraryWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Updateforisavailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Books SET IsAvailable = 1 WHERE IsAvailable = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
