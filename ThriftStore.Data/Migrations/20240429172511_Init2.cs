using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThriftStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, "Electronics", null, new DateTime(2024, 4, 29, 18, 25, 10, 34, DateTimeKind.Local).AddTicks(168), true, false, null, null },
                    { 2L, "Fashion", null, new DateTime(2024, 4, 29, 18, 25, 10, 34, DateTimeKind.Local).AddTicks(184), true, false, null, null },
                    { 3L, "Phones & Tabs", null, new DateTime(2024, 4, 29, 18, 25, 10, 34, DateTimeKind.Local).AddTicks(186), true, false, null, null },
                    { 4L, "Computers & Accessories", null, new DateTime(2024, 4, 29, 18, 25, 10, 34, DateTimeKind.Local).AddTicks(188), true, false, null, null },
                    { 5L, "Home & Kitchen", null, new DateTime(2024, 4, 29, 18, 25, 10, 34, DateTimeKind.Local).AddTicks(189), true, false, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5L);
        }
    }
}
