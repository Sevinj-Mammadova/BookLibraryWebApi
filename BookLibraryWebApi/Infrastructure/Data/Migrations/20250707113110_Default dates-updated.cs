using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibraryWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Defaultdatesupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(DAY, 14, CAST(GETDATE() AS DATE))",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(DAY, 14, GETDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CAST(GETDATE() AS DATE)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(DAY, 14, GETDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(DAY, 14, CAST(GETDATE() AS DATE))");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowDate",
                table: "BorrowRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CAST(GETDATE() AS DATE)");
        }
    }
}
