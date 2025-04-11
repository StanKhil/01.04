using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _01._04.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "Id", "DeletedAt", "Description", "ImageUrl", "Name", "ParentId", "Slug" },
                values: new object[,]
                {
                    { new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), null, "Декоративні вироби, посуд з кольорового та прозорого скла", "glass.jpg", "Скло", null, "glass" },
                    { new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), null, "Офісне приладдя та настільні сувеніри", "office.jpg", "Офіс", null, "office" },
                    { new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"), null, "Вироби з природнього та штучного каменю", "stone.jpg", "Камінь", null, "stone" },
                    { new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"), null, "Сувеніри та декоративні вироби, а також посуд з деревини", "wood.jpg", "Дерево", null, "wood" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DeletedAt", "Description", "GroupId", "ImageUrl", "Name", "Price", "Slug", "Stock" },
                values: new object[,]
                {
                    { new Guid("144501e6-981c-4871-9505-435fd84861a7"), null, "Декоративний прес для паперів з кулями-орбітами", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office7.jpg", "Прес для паперу", 450.0m, "office7", 10 },
                    { new Guid("1a268929-778b-4632-a505-b749ac3fac9b"), null, "Набір з двох дерев'яних кухолів", new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"), "wood3.jpg", "Кухоль", 850.0m, "wood3", 10 },
                    { new Guid("1bedf52f-280a-4dab-a84a-631c13dcdf0b"), null, "Декоративна настільна статуетка у формі золотого бика", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office8_1.jpg", "Золотий бик", 750.0m, "office-bull", 10 },
                    { new Guid("1da8df6d-b9e3-4ba0-9470-7682b3124717"), null, "Настільний декор з маятником Ньютона", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office2.jpg", "Маятник Ньютона", 450.0m, "office2", 10 },
                    { new Guid("2c0ef23e-b65e-4353-b991-a52c7d3d029f"), null, "Настільна статуетка у формі вершника", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office1.jpeg", "Вершник", 350.0m, "office1", 10 },
                    { new Guid("371fbea0-30fe-4396-a174-4f3e526996aa"), null, "Настільний декор з маятником Жордана", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office4.jpg", "Маятник Жордана", 450.0m, null, 10 },
                    { new Guid("38cb6817-606e-489c-8381-2b42032cbc22"), null, "Скляна куля з новорічною ялинкою", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass1.png", "Ялинка", 250.0m, "glass-tree", 10 },
                    { new Guid("476332f3-de96-4340-8a3d-4d0ffc77a390"), null, "Настільний декоративний маятник Жордана", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office6.jpg", "Орбіти", 400.0m, "office6", 10 },
                    { new Guid("48f93159-243d-4975-be8f-a68316a391cf"), null, "Дерев'яний настільний декор з левами", new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"), "wood5.jpeg", "Леви", 900.0m, "wood-lions", 10 },
                    { new Guid("51bb87d3-ce91-4a07-9f9e-78f8abe02356"), null, "Маленька фігурка кота з прозорого скла", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass7.jpg", "Кіт", 220.0m, "glass-cat", 10 },
                    { new Guid("6af692a1-991f-4de6-84ad-6f4539a5cde7"), null, "Дерев'яна настільна підставка для ручок з черепахою", new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"), "wood4.jpg", "Черепаха", 750.0m, "wood4", 10 },
                    { new Guid("74df6c67-fe0b-43b4-a735-81a59293e1eb"), null, "Кам'яна плошка для дрібних речей", new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"), null, "Плошка", 250.0m, null, 10 },
                    { new Guid("87bfc6ba-2227-4e44-97ea-52d20222e23a"), null, "Декоративний кошик з різьбленого дерева", new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"), "wood1.jpg", "Кошик", 750.0m, "wood1", 10 },
                    { new Guid("8e0f04f7-e614-49d3-be95-c5c48c866043"), null, "Склянка для води з прозорого скла", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), null, "Склянка", 50.0m, null, 10 },
                    { new Guid("aa712e14-856e-4619-a1f9-c7a0b50148de"), null, "Настільний декор у формі круглої каруселі", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office3.jpg", "Карусель", 450.0m, null, 10 },
                    { new Guid("b253d8eb-c9dc-40a3-97c3-e967da09eada"), null, "Маленька фігурка лисиці з кольорового скла", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass4.jpg", "Лис", 150.0m, "glass-fox", 10 },
                    { new Guid("bd2c7fbb-26ef-4175-8b15-774e560815c6"), null, "Скляна куля з образом гелікоптера", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass3.jpg", "Гелікоптер", 249.50m, "glass-helicopter", 10 },
                    { new Guid("bed842bf-8f30-41f2-b4e7-797ccbbff8da"), null, "Настільна кругла цукерниця з прозорого скла", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass5.jpg", "Цукерниця", 400.0m, null, 10 },
                    { new Guid("c1921a57-bbdc-47a7-889e-d075ecb7ce79"), null, "Кам'яна декоративна фігурка у формі слоника", new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"), null, "Слон", 350.0m, null, 10 },
                    { new Guid("c2ccdcca-adbb-43bf-9910-6c24e0fd79c0"), null, "Скляна фігура бика з різнокольорового скла", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass2.jpg", "Бик", 350.0m, "glass-buffalo", 10 },
                    { new Guid("c4a7c325-9289-4730-826d-83cc396bd60d"), null, "Скляна куля з образом терез в середині", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass8.jpg", "Терези", 270.0m, null, 10 },
                    { new Guid("c9588769-226e-472d-bae5-b7465aa8b98a"), null, "Маленька фігурка павича з кольорового скла", new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"), "glass6.jpg", "Павич", 179.50m, null, 10 },
                    { new Guid("d9c05b09-62cf-4bfa-ab4c-4082dccfd5e2"), null, "Настільний декор у формі корабельного штурвалу", new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"), "office5.jpeg", "Штурвал", 500.0m, "office5", 10 },
                    { new Guid("e3a8c0a9-d5fd-4059-8d3c-e9d91d45906c"), null, "Дерев'яна булава з підставкою", new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"), "wood2.jpg", "Булава", 950.0m, "wood2", 10 },
                    { new Guid("e6da52ce-fb71-40de-8b5f-4f561b2fc24e"), null, "Кам'яна декоративна фігурка у формі сови", new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"), "stone3.jpg", "Сова", 450.0m, "stone-owl", 10 },
                    { new Guid("e72753eb-b0a1-42fe-a243-e806ae0c1ee8"), null, "Кам'яний декоративний виріб у формі груши", new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"), "stone2.jpg", "Груша", 650.0m, "stone2", 10 },
                    { new Guid("ed186a0a-7470-474e-85e7-efc6eeaf7705"), null, "Кам'яний малий глечик з кришкою", new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"), "stone1.jpg", "Глечик", 350.0m, "stone1", 10 }
                });

            migrationBuilder.InsertData(
                table: "ItemImages",
                columns: new[] { "ImageUrl", "ItemId", "Order" },
                values: new object[,]
                {
                    { "office8_2.jpg", new Guid("1bedf52f-280a-4dab-a84a-631c13dcdf0b"), 1 },
                    { "office8_3.jpg", new Guid("1bedf52f-280a-4dab-a84a-631c13dcdf0b"), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemImages",
                keyColumns: new[] { "ImageUrl", "ItemId" },
                keyValues: new object[] { "office8_2.jpg", new Guid("1bedf52f-280a-4dab-a84a-631c13dcdf0b") });

            migrationBuilder.DeleteData(
                table: "ItemImages",
                keyColumns: new[] { "ImageUrl", "ItemId" },
                keyValues: new object[] { "office8_3.jpg", new Guid("1bedf52f-280a-4dab-a84a-631c13dcdf0b") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("144501e6-981c-4871-9505-435fd84861a7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1a268929-778b-4632-a505-b749ac3fac9b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1da8df6d-b9e3-4ba0-9470-7682b3124717"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2c0ef23e-b65e-4353-b991-a52c7d3d029f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("371fbea0-30fe-4396-a174-4f3e526996aa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("38cb6817-606e-489c-8381-2b42032cbc22"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("476332f3-de96-4340-8a3d-4d0ffc77a390"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("48f93159-243d-4975-be8f-a68316a391cf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("51bb87d3-ce91-4a07-9f9e-78f8abe02356"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6af692a1-991f-4de6-84ad-6f4539a5cde7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("74df6c67-fe0b-43b4-a735-81a59293e1eb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("87bfc6ba-2227-4e44-97ea-52d20222e23a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8e0f04f7-e614-49d3-be95-c5c48c866043"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("aa712e14-856e-4619-a1f9-c7a0b50148de"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b253d8eb-c9dc-40a3-97c3-e967da09eada"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bd2c7fbb-26ef-4175-8b15-774e560815c6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bed842bf-8f30-41f2-b4e7-797ccbbff8da"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1921a57-bbdc-47a7-889e-d075ecb7ce79"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c2ccdcca-adbb-43bf-9910-6c24e0fd79c0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c4a7c325-9289-4730-826d-83cc396bd60d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c9588769-226e-472d-bae5-b7465aa8b98a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d9c05b09-62cf-4bfa-ab4c-4082dccfd5e2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e3a8c0a9-d5fd-4059-8d3c-e9d91d45906c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e6da52ce-fb71-40de-8b5f-4f561b2fc24e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e72753eb-b0a1-42fe-a243-e806ae0c1ee8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ed186a0a-7470-474e-85e7-efc6eeaf7705"));

            migrationBuilder.DeleteData(
                table: "ProductGroups",
                keyColumn: "Id",
                keyValue: new Guid("0dc4a692-2137-4694-bcb3-684ed826b520"));

            migrationBuilder.DeleteData(
                table: "ProductGroups",
                keyColumn: "Id",
                keyValue: new Guid("3ec0edc9-b252-4470-bc1b-f66daea28bce"));

            migrationBuilder.DeleteData(
                table: "ProductGroups",
                keyColumn: "Id",
                keyValue: new Guid("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1bedf52f-280a-4dab-a84a-631c13dcdf0b"));

            migrationBuilder.DeleteData(
                table: "ProductGroups",
                keyColumn: "Id",
                keyValue: new Guid("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"));
        }
    }
}
