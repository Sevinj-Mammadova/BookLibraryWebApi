using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibraryWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Defaultdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(DAY, 14, GETDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(DAY, 14, GETDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
