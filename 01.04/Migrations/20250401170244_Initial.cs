using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _01._04.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dk = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanCreate = table.Column<bool>(type: "bit", nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserAccesses",
                columns: new[] { "Id", "Dk", "Login", "RoleId", "Salt", "UserId" },
                values: new object[,]
                {
                    { new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), "Salt9123", "bondarko", "SelfRegistered", "Salt9", new Guid("a0f7b463-6eef-4a70-8444-789bbea23369") },
                    { new Guid("7a38a3aa-de9f-4519-bb48-eeb86c1efcdf"), "Salt5123", "dina@ukr.net", "Moderator", "Salt5", new Guid("0d156354-89f1-4d58-a735-876b7add59d2") },
                    { new Guid("8806ca58-8daa-4576-92ba-797de42ffaa7"), "Salt7123", "erstenuk", "Employee", "Salt7", new Guid("eadb0b3b-523e-478b-88ee-b6cf57cbc05d") },
                    { new Guid("92cd36b8-ea5a-4cbb-a232-268d942c97fd"), "Salt4123", "dina", "Employee", "Salt4", new Guid("0d156354-89f1-4d58-a735-876b7add59d2") },
                    { new Guid("97191468-a02f-4a78-927b-9ea660e9ea36"), "Salt8123", "erstenuk@ukr.net", "Administrator", "Salt8", new Guid("eadb0b3b-523e-478b-88ee-b6cf57cbc05d") },
                    { new Guid("b31355b7-aa02-4b10-afda-eb9ec8294e78"), "Salt3123", "dnistr", "SelfRegistered", "Salt3", new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434") },
                    { new Guid("e29b6a1a-5bc7-4f42-9fa4-db25de342b42"), "Salt1123", "jakiv", "SelfRegistered", "Salt1", new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d") },
                    { new Guid("f1ea6b3f-0021-417b-95c8-f6cd333d7207"), "Salt6123", "romashko", "SelfRegistered", "Salt6", new Guid("a3c55a79-05ea-4053-ad3c-7301f3b7a7e2") },
                    { new Guid("fb4ad18c-d916-4708-be71-a9bbcf1eb806"), "Salt2123", "storozh", "Employee", "Salt2", new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CanCreate", "CanDelete", "CanRead", "CanUpdate", "Description" },
                values: new object[,]
                {
                    { "Administrator", true, true, true, true, "Системний адміністратор" },
                    { "Employee", true, false, true, false, "Співробітник компанії" },
                    { "Moderator", false, true, true, true, "Редактор контенту" },
                    { "SelfRegistered", false, false, false, false, "Самостійно зареєстрований користувач" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "DeletedAt", "Email", "Name", "RegisteredAt" },
                values: new object[,]
                {
                    { new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"), new DateTime(1989, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "dnistr@ukr.net", "Дністрянський Збоїслав", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("0d156354-89f1-4d58-a735-876b7add59d2"), new DateTime(2005, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "dina@ukr.net", "Гординська Діна", new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"), new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "jakiv@ukr.net", "Палійчук Яків", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a0f7b463-6eef-4a70-8444-789bbea23369"), new DateTime(1999, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "bondarko@ukr.net", "Бондарко Юрій", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a3c55a79-05ea-4053-ad3c-7301f3b7a7e2"), new DateTime(2005, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "romashko@ukr.net", "Ромашко Жадан", new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"), new DateTime(1999, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "storozh@ukr.net", "Сторож Чеслава", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("eadb0b3b-523e-478b-88ee-b6cf57cbc05d"), new DateTime(2001, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "erstenuk@ukr.net", "Ерстенюк Вікторія", new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccesses_Login",
                table: "UserAccesses",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccesses");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
