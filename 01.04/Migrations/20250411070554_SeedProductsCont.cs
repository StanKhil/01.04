using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _01._04.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductsCont : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DeletedAt", "Description", "GroupId", "ImageUrl", "Name", "Price", "Slug", "Stock" },
                values: new object[,]
                {
                    { new Guid("5f58f9f3-efd5-4010-bc72-c451eb45b8fe"), null, "Дерев'яна настільна підставка для ручок з слоном", new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"), "wood6.jpg", "Слон", 750.0m, null, 10 },
                    { new Guid("8b597422-082c-4112-aeba-2dee8d4f4082"), null, "Кам'яний кубок для напоїв", new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"), "stone4.jpg", "Кубок", 500.0m, null, 10 },
                    { new Guid("c9418272-0f98-4915-a4b4-083676dd7e53"), null, "Скляна ваза для квітів з прозорого скла", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass9.jpg", "Ваза", 150.0m, null, 10 },
                    { new Guid("e7d2d965-d653-4c62-9c6d-2f6a64771362"), null, "Настільний глобус з підсвіткою", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office9.jpg", "Глобус", 800.0m, null, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5f58f9f3-efd5-4010-bc72-c451eb45b8fe"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8b597422-082c-4112-aeba-2dee8d4f4082"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c9418272-0f98-4915-a4b4-083676dd7e53"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e7d2d965-d653-4c62-9c6d-2f6a64771362"));
        }
    }
}
