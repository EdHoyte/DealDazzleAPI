using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThriftStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubCategories",
                newName: "SubCategoryName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Items",
                newName: "ItemName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategoryName",
                table: "SubCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "Items",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2024, 4, 29, 12, 51, 56, 693, DateTimeKind.Local).AddTicks(3279), true, false, "Electronics", null, null },
                    { 2L, null, new DateTime(2024, 4, 29, 12, 51, 56, 693, DateTimeKind.Local).AddTicks(3305), true, false, "Fashion", null, null },
                    { 3L, null, new DateTime(2024, 4, 29, 12, 51, 56, 693, DateTimeKind.Local).AddTicks(3307), true, false, "Phones & Tabs", null, null },
                    { 4L, null, new DateTime(2024, 4, 29, 12, 51, 56, 693, DateTimeKind.Local).AddTicks(3309), true, false, "Computers & Accessories", null, null },
                    { 5L, null, new DateTime(2024, 4, 29, 12, 51, 56, 693, DateTimeKind.Local).AddTicks(3311), true, false, "Home & Kitchen", null, null }
                });
        }
    }
}
