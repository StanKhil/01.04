using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using _01._04.Data.Entities;

namespace _01._04.Data
{
    public class DataContext : DbContext
    {
        // Контекст даних - відображення бд
        // Таблиці БД відповідають коллекціям в контексті
        public DbSet<Entities.User> Users { get; set; } = null!;
        public DbSet<Entities.UserRole> UserRoles { get; set; } = null!;
        public DbSet<Entities.UserAccess> UserAccesses { get; set; } = null!;


        public DbSet<Entities.ProductGroup> ProductGroups { get; set; }
        public DbSet<Entities.Product> Products { get; set; }
        public DbSet<Entities.ItemImage> ItemImages { get; set; }

        public DataContext() : base(){}

        // Налаштування контексту даних - перевизначення методу OnConfiguring та OnModelCreating

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Встановлення з'єднання з БД
            // Дістаємось до конфігурації
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            // Зазначаємо рядок підключення та провайдер БД
            optionsBuilder.UseSqlServer(config.GetConnectionString("LocalDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.ItemImage>()
                .HasKey(i => new { i.ItemId, i.ImageUrl });

            modelBuilder.Entity<Entities.Product>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<Entities.ProductGroup>()
                .HasAlternateKey(pg => pg.Slug);

            modelBuilder.Entity<Entities.Product>()
                .HasOne(p => p.ProductGroup)
                .WithMany(pg => pg.Products)
                .HasForeignKey(p => p.GroupId);

            modelBuilder.Entity<Entities.Product>()
                .HasMany(p => p.Images)
                .WithOne()
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(i => i.ItemId);

            modelBuilder.Entity<Entities.ProductGroup>()
                .HasMany(p => p.Images)
                .WithOne()
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(i => i.ItemId);

            modelBuilder.Entity<Entities.ProductGroup>()
                .HasOne(pg => pg.ParentGroup)
                .WithMany()
                .HasForeignKey(pg => pg.ParentId);

            // Встановлення зв'язків між таблицями
            modelBuilder.Entity<Entities.UserAccess>()
                .HasIndex(ua => ua.Login)
                .IsUnique();
            modelBuilder.Entity<Entities.UserAccess>() //Налаштування зв'язків через навігаційні властивості
                .HasOne(ua => ua.User)
                .WithMany(u => u.userAccesses)
                .HasForeignKey(ua => ua.UserId) // ці інструкції не обов'язкові, якщо дотримуватися іменнування
                .HasPrincipalKey(u => u.Id);

            /*modelBuilder.Entity<Entities.User>() аналогічна інструкція, що починається з User
                .HasMany(u => u.userAccesses)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .HasPrincipalKey(u => u.Id);*/

            modelBuilder.Entity<Entities.UserAccess>()
                .HasOne(ua => ua.UserRole)
                .WithMany(ur => ur.UserAccesses)
                .HasForeignKey(ua => ua.RoleId);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Додавання даних в таблиці
            
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = "SelfRegistered",
                    Description = "Самостійно зареєстрований користувач",
                    CanCreate = false,
                    CanRead = false,
                    CanUpdate = false,
                    CanDelete = false
                },
                new UserRole
                {
                    Id = "Employee",
                    Description = "Співробітник компанії",
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = false,
                    CanDelete = false
                },
                new UserRole
                {
                    Id = "Moderator",
                    Description = "Редактор контенту",
                    CanCreate = false,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true
                },
                new UserRole
                {
                    Id = "Administrator",
                    Description = "Системний адміністратор",
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"),
                    Name = "Палійчук Яків",
                    Email = "jakiv@ukr.net",
                    BirthDate = new DateTime(1998, 3, 15),
                    RegisteredAt = new DateTime(2025, 3, 10)
                },
                new User
                {
                    Id = Guid.Parse("bdf41cd9-c0f1-4349-8a44-4e67755d0415"),
                    Name = "Сторож Чеслава",
                    Email = "storozh@ukr.net",
                    BirthDate = new DateTime(1999, 5, 11),
                    RegisteredAt = new DateTime(2025, 3, 15)
                },
                new User
                {
                    Id = Guid.Parse("03767d46-aab3-4cc4-989c-a696a7fdd434"),
                    Name = "Дністрянський Збоїслав",
                    Email = "dnistr@ukr.net",
                    BirthDate = new DateTime(1989, 7, 10),
                    RegisteredAt = new DateTime(2024, 8, 5)
                },
                new User
                {
                    Id = Guid.Parse("0d156354-89f1-4d58-a735-876b7add59d2"),
                    Name = "Гординська Діна",
                    Email = "dina@ukr.net",
                    BirthDate = new DateTime(2005, 2, 15),
                    RegisteredAt = new DateTime(2024, 12, 20)
                },
                new User
                {
                    Id = Guid.Parse("a3c55a79-05ea-4053-ad3c-7301f3b7a7e2"),
                    Name = "Ромашко Жадан",
                    Email = "romashko@ukr.net",
                    BirthDate = new DateTime(2005, 2, 15),
                    RegisteredAt = new DateTime(2024, 12, 20)
                },
                new User
                {
                    Id = Guid.Parse("eadb0b3b-523e-478b-88ee-b6cf57cbc05d"),
                    Name = "Ерстенюк Вікторія",
                    Email = "erstenuk@ukr.net",
                    BirthDate = new DateTime(2001, 12, 21),
                    RegisteredAt = new DateTime(2025, 1, 21)
                },
                new User
                {
                    Id = Guid.Parse("a0f7b463-6eef-4a70-8444-789bbea23369"),
                    Name = "Бондарко Юрій",
                    Email = "bondarko@ukr.net",
                    BirthDate = new DateTime(1999, 10, 21),
                    RegisteredAt = new DateTime(2025, 2, 2)
                }
            );

            modelBuilder.Entity<UserAccess>().HasData(
                new UserAccess
                {
                    Id = Guid.Parse("e29b6a1a-5bc7-4f42-9fa4-db25de342b42"),
                    UserId = Guid.Parse("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"),
                    Login = "jakiv",
                    Salt = "Salt1",
                    Dk = "Salt1123",
                    RoleId = "SelfRegistered"
                },
                new UserAccess
                {
                    Id = Guid.Parse("fb4ad18c-d916-4708-be71-a9bbcf1eb806"),
                    UserId = Guid.Parse("bdf41cd9-c0f1-4349-8a44-4e67755d0415"),
                    Login = "storozh",
                    Salt = "Salt2",
                    Dk = "Salt2123",
                    RoleId = "Employee"
                },
                new UserAccess
                {
                    Id = Guid.Parse("b31355b7-aa02-4b10-afda-eb9ec8294e78"),
                    UserId = Guid.Parse("03767d46-aab3-4cc4-989c-a696a7fdd434"),
                    Login = "dnistr",
                    Salt = "Salt3",
                    Dk = "Salt3123",
                    RoleId = "SelfRegistered"
                },
                new UserAccess
                {
                    Id = Guid.Parse("92cd36b8-ea5a-4cbb-a232-268d942c97fd"),
                    UserId = Guid.Parse("0d156354-89f1-4d58-a735-876b7add59d2"),
                    Login = "dina",
                    Salt = "Salt4",
                    Dk = "Salt4123",
                    RoleId = "Employee"
                },
                new UserAccess
                {
                    Id = Guid.Parse("7a38a3aa-de9f-4519-bb48-eeb86c1efcdf"),
                    UserId = Guid.Parse("0d156354-89f1-4d58-a735-876b7add59d2"),
                    Login = "dina@ukr.net",
                    Salt = "Salt5",
                    Dk = "Salt5123",
                    RoleId = "Moderator"
                },
                new UserAccess
                {
                    Id = Guid.Parse("f1ea6b3f-0021-417b-95c8-f6cd333d7207"),
                    UserId = Guid.Parse("a3c55a79-05ea-4053-ad3c-7301f3b7a7e2"),
                    Login = "romashko",
                    Salt = "Salt6",
                    Dk = "Salt6123",
                    RoleId = "SelfRegistered"
                },
                new UserAccess
                {
                    Id = Guid.Parse("8806ca58-8daa-4576-92ba-797de42ffaa7"),
                    UserId = Guid.Parse("eadb0b3b-523e-478b-88ee-b6cf57cbc05d"),
                    Login = "erstenuk",
                    Salt = "Salt7",
                    Dk = "Salt7123",
                    RoleId = "Employee"
                },
                new UserAccess
                {
                    Id = Guid.Parse("97191468-a02f-4a78-927b-9ea660e9ea36"),
                    UserId = Guid.Parse("eadb0b3b-523e-478b-88ee-b6cf57cbc05d"),
                    Login = "erstenuk@ukr.net",
                    Salt = "Salt8",
                    Dk = "Salt8123",
                    RoleId = "Administrator"
                },
                new UserAccess {
                    Id = Guid.Parse("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"),
                    UserId = Guid.Parse("a0f7b463-6eef-4a70-8444-789bbea23369"),
                    Login = "bondarko",
                    Salt = "Salt9",
                    Dk = "Salt9123",
                    RoleId = "SelfRegistered"
                }
            );

            modelBuilder.Entity<ProductGroup>().HasData(
                new ProductGroup
                {
                    Id = Guid.Parse("0dc4a692-2137-4694-bcb3-684ed826b520"),
                    Name = "Скло",
                    Description = "Декоративні вироби, посуд з кольорового та прозорого скла",
                    Slug = "glass",
                    ImageUrl = "glass.jpg"
                },
                new ProductGroup
                {
                    Id = Guid.Parse("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2"),
                    Name = "Офіс",
                    Description = "Офісне приладдя та настільні сувеніри",
                    Slug = "office",
                    ImageUrl = "office.jpg"
                },
                new ProductGroup
                {
                    Id = Guid.Parse("3ec0edc9-b252-4470-bc1b-f66daea28bce"),
                    Name = "Камінь",
                    Description = "Вироби з природнього та штучного каменю",
                    Slug = "stone",
                    ImageUrl = "stone.jpg"
                },
                new ProductGroup
                {
                    Id = Guid.Parse("f3d4aee1-3ee1-4f2e-b244-026bd45207ec"),
                    Name = "Дерево",
                    Description = "Сувеніри та декоративні вироби, а також посуд з деревини",
                    Slug = "wood",
                    ImageUrl = "wood.jpg"
                }
            );

            SeedGlass(modelBuilder);
            SeedOffice(modelBuilder);
            SeedStone(modelBuilder);
            SeedWood(modelBuilder);
        }

        private void SeedGlass(ModelBuilder modelBuilder)
        {
            Guid grp = Guid.Parse("0dc4a692-2137-4694-bcb3-684ed826b520");
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("38cb6817-606e-489c-8381-2b42032cbc22"),
                    GroupId = grp,
                    ImageUrl = "glass1.png",
                    Name = "Ялинка",
                    Description = "Скляна куля з новорічною ялинкою",
                    Slug = "glass-tree",
                    Stock = 10,
                    Price = 250.0M,
                },
                new Product
                {
                    Id = Guid.Parse("c2ccdcca-adbb-43bf-9910-6c24e0fd79c0"),
                    GroupId = grp,
                    ImageUrl = "glass2.jpg",
                    Name = "Бик",
                    Description = "Скляна фігура бика з різнокольорового скла",
                    Slug = "glass-buffalo",
                    Stock = 10,
                    Price = 350.0M,
                },
                new Product
                {
                    Id = Guid.Parse("bd2c7fbb-26ef-4175-8b15-774e560815c6"),
                    GroupId = grp,
                    ImageUrl = "glass3.jpg",
                    Name = "Гелікоптер",
                    Description = "Скляна куля з образом гелікоптера",
                    Slug = "glass-helicopter",
                    Stock = 10,
                    Price = 249.50M,
                },
                new Product
                {
                    Id = Guid.Parse("b253d8eb-c9dc-40a3-97c3-e967da09eada"),
                    GroupId = grp,
                    ImageUrl = "glass4.jpg",
                    Name = "Лис",
                    Description = "Маленька фігурка лисиці з кольорового скла",
                    Slug = "glass-fox",
                    Stock = 10,
                    Price = 150.0M,
                },
                new Product
                {
                    Id = Guid.Parse("bed842bf-8f30-41f2-b4e7-797ccbbff8da"),
                    GroupId = grp,
                    ImageUrl = "glass5.jpg",
                    Name = "Цукерниця",
                    Description = "Настільна кругла цукерниця з прозорого скла",
                    Slug = null,
                    Stock = 10,
                    Price = 400.0M,
                },
                new Product
                {
                    Id = Guid.Parse("c9588769-226e-472d-bae5-b7465aa8b98a"),
                    GroupId = grp,
                    ImageUrl = "glass6.jpg",
                    Name = "Павич",
                    Description = "Маленька фігурка павича з кольорового скла",
                    Slug = null,
                    Stock = 10,
                    Price = 179.50M,
                },
                new Product
                {
                    Id = Guid.Parse("51bb87d3-ce91-4a07-9f9e-78f8abe02356"),
                    GroupId = grp,
                    ImageUrl = "glass7.jpg",
                    Name = "Кіт",
                    Description = "Маленька фігурка кота з прозорого скла",
                    Slug = "glass-cat",
                    Stock = 10,
                    Price = 220.0M,
                },
                new Product
                {
                    Id = Guid.Parse("c4a7c325-9289-4730-826d-83cc396bd60d"),
                    GroupId = grp,
                    ImageUrl = "glass8.jpg",
                    Name = "Терези",
                    Description = "Скляна куля з образом терез в середині",
                    Slug = null,
                    Stock = 10,
                    Price = 270.0M,
                },
                new Product
                {
                    Id = Guid.Parse("8e0f04f7-e614-49d3-be95-c5c48c866043"),
                    GroupId = grp,
                    ImageUrl = null,
                    Name = "Склянка",
                    Description = "Склянка для води з прозорого скла",
                    Slug = null,
                    Stock = 10,
                    Price = 50.0M,
                },
                new Product
                {
                    Id = Guid.Parse("c9418272-0f98-4915-a4b4-083676dd7e53"),
                    GroupId = grp,
                    ImageUrl = "glass9.jpg",
                    Name = "Ваза",
                    Description = "Скляна ваза для квітів з прозорого скла",
                    Slug = null,
                    Stock = 10,
                    Price = 150.0M,
                }
            );
        }

        private void SeedOffice(ModelBuilder modelBuilder)
        {
            Guid grp = Guid.Parse("2444fc1e-5bc5-4a9a-8c69-fab1905a11a2");
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("2c0ef23e-b65e-4353-b991-a52c7d3d029f"),
                    GroupId = grp,
                    ImageUrl = "office1.jpeg",
                    Name = "Вершник",
                    Description = "Настільна статуетка у формі вершника",
                    Slug = "office1",
                    Stock = 10,
                    Price = 350.0M,
                },
                new Product
                {
                    Id = Guid.Parse("1da8df6d-b9e3-4ba0-9470-7682b3124717"),
                    GroupId = grp,
                    ImageUrl = "office2.jpg",
                    Name = "Маятник Ньютона",
                    Description = "Настільний декор з маятником Ньютона",
                    Slug = "office2",
                    Stock = 10,
                    Price = 450.0M,
                },
                new Product
                {
                    Id = Guid.Parse("aa712e14-856e-4619-a1f9-c7a0b50148de"),
                    GroupId = grp,
                    ImageUrl = "office3.jpg",
                    Name = "Карусель",
                    Description = "Настільний декор у формі круглої каруселі",
                    Slug = null,
                    Stock = 10,
                    Price = 450.0M,
                },
                new Product
                {
                    Id = Guid.Parse("371fbea0-30fe-4396-a174-4f3e526996aa"),
                    GroupId = grp,
                    ImageUrl = "office4.jpg",
                    Name = "Маятник Жордана",
                    Description = "Настільний декор з маятником Жордана",
                    Slug = null,
                    Stock = 10,
                    Price = 450.0M,
                },
                new Product
                {
                    Id = Guid.Parse("d9c05b09-62cf-4bfa-ab4c-4082dccfd5e2"),
                    GroupId = grp,
                    ImageUrl = "office5.jpeg",
                    Name = "Штурвал",
                    Description = "Настільний декор у формі корабельного штурвалу",
                    Slug = "office5",
                    Stock = 10,
                    Price = 500.0M,
                },
                new Product
                {
                    Id = Guid.Parse("476332f3-de96-4340-8a3d-4d0ffc77a390"),
                    GroupId = grp,
                    ImageUrl = "office6.jpg",
                    Name = "Орбіти",
                    Description = "Настільний декоративний маятник Жордана",
                    Slug = "office6",
                    Stock = 10,
                    Price = 400.0M,
                },
                new Product
                {
                    Id = Guid.Parse("144501e6-981c-4871-9505-435fd84861a7"),
                    GroupId = grp,
                    ImageUrl = "office7.jpg",
                    Name = "Прес для паперу",
                    Description = "Декоративний прес для паперів з кулями-орбітами",
                    Slug = "office7",
                    Stock = 10,
                    Price = 450.0M,
                },
                new Product
                {
                    Id = Guid.Parse("1bedf52f-280a-4dab-a84a-631c13dcdf0b"),
                    GroupId = grp,
                    ImageUrl = "office8_1.jpg",
                    Name = "Золотий бик",
                    Description = "Декоративна настільна статуетка у формі золотого бика",
                    Slug = "office-bull",
                    Stock = 10,
                    Price = 750.0M,
                },
                new Product
                {
                    Id = Guid.Parse("e7d2d965-d653-4c62-9c6d-2f6a64771362"),
                    GroupId = grp,
                    ImageUrl = "office9.jpg",
                    Name = "Глобус",
                    Description = "Настільний глобус з підсвіткою",
                    Slug = null,
                    Stock = 10,
                    Price = 800.0M,
                }
            );
            modelBuilder.Entity<ItemImage>().HasData(
                new ItemImage
                {
                    ItemId = Guid.Parse("1bedf52f-280a-4dab-a84a-631c13dcdf0b"),
                    ImageUrl = "office8_2.jpg",
                    Order = 1
                },
                new ItemImage
                {
                    ItemId = Guid.Parse("1bedf52f-280a-4dab-a84a-631c13dcdf0b"),
                    ImageUrl = "office8_3.jpg",
                    Order = 1
                }
            );

        }

        private void SeedStone(ModelBuilder modelBuilder)
        {
            Guid grp = Guid.Parse("3ec0edc9-b252-4470-bc1b-f66daea28bce");
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("ed186a0a-7470-474e-85e7-efc6eeaf7705"),
                    GroupId = grp,
                    ImageUrl = "stone1.jpg",
                    Name = "Глечик",
                    Description = "Кам'яний малий глечик з кришкою",
                    Slug = "stone1",
                    Stock = 10,
                    Price = 350.0M,
                },
                new Product
                {
                    Id = Guid.Parse("e72753eb-b0a1-42fe-a243-e806ae0c1ee8"),
                    GroupId = grp,
                    ImageUrl = "stone2.jpg",
                    Name = "Груша",
                    Description = "Кам'яний декоративний виріб у формі груши",
                    Slug = "stone2",
                    Stock = 10,
                    Price = 650.0M,
                },
                new Product
                {
                    Id = Guid.Parse("e6da52ce-fb71-40de-8b5f-4f561b2fc24e"),
                    GroupId = grp,
                    ImageUrl = "stone3.jpg",
                    Name = "Сова",
                    Description = "Кам'яна декоративна фігурка у формі сови",
                    Slug = "stone-owl",
                    Stock = 10,
                    Price = 450.0M,
                },
                new Product
                {
                    Id = Guid.Parse("74df6c67-fe0b-43b4-a735-81a59293e1eb"),
                    GroupId = grp,
                    ImageUrl = null,
                    Name = "Плошка",
                    Description = "Кам'яна плошка для дрібних речей",
                    Slug = null,
                    Stock = 10,
                    Price = 250.0M,
                },
                new Product
                {
                    Id = Guid.Parse("c1921a57-bbdc-47a7-889e-d075ecb7ce79"),
                    GroupId = grp,
                    ImageUrl = null,
                    Name = "Слон",
                    Description = "Кам'яна декоративна фігурка у формі слоника",
                    Slug = null,
                    Stock = 10,
                    Price = 350.0M,
                },
                new Product
                {
                    Id = Guid.Parse("8b597422-082c-4112-aeba-2dee8d4f4082"),
                    GroupId = grp,
                    ImageUrl = "stone4.jpg",
                    Name = "Кубок",
                    Description = "Кам'яний кубок для напоїв",
                    Slug = null,
                    Stock = 10,
                    Price = 500.0M,
                }
            );
        }

        private void SeedWood(ModelBuilder modelBuilder)
        {
            Guid grp = Guid.Parse("f3d4aee1-3ee1-4f2e-b244-026bd45207ec");
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("87bfc6ba-2227-4e44-97ea-52d20222e23a"),
                    GroupId = grp,
                    ImageUrl = "wood1.jpg",
                    Name = "Кошик",
                    Description = "Декоративний кошик з різьбленого дерева",
                    Slug = "wood1",
                    Stock = 10,
                    Price = 750.0M,
                },
                new Product
                {
                    Id = Guid.Parse("e3a8c0a9-d5fd-4059-8d3c-e9d91d45906c"),
                    GroupId = grp,
                    ImageUrl = "wood2.jpg",
                    Name = "Булава",
                    Description = "Дерев'яна булава з підставкою",
                    Slug = "wood2",
                    Stock = 10,
                    Price = 950.0M,
                },
                new Product
                {
                    Id = Guid.Parse("1a268929-778b-4632-a505-b749ac3fac9b"),
                    GroupId = grp,
                    ImageUrl = "wood3.jpg",
                    Name = "Кухоль",
                    Description = "Набір з двох дерев'яних кухолів",
                    Slug = "wood3",
                    Stock = 10,
                    Price = 850.0M,
                },
                new Product
                {
                    Id = Guid.Parse("6af692a1-991f-4de6-84ad-6f4539a5cde7"),
                    GroupId = grp,
                    ImageUrl = "wood4.jpg",
                    Name = "Черепаха",
                    Description = "Дерев'яна настільна підставка для ручок з черепахою",
                    Slug = "wood4",
                    Stock = 10,
                    Price = 750.0M,
                },
                new Product
                {
                    Id = Guid.Parse("48f93159-243d-4975-be8f-a68316a391cf"),
                    GroupId = grp,
                    ImageUrl = "wood5.jpeg",
                    Name = "Леви",
                    Description = "Дерев'яний настільний декор з левами",
                    Slug = "wood-lions",
                    Stock = 10,
                    Price = 900.0M,
                },
                new Product
                {
                    Id = Guid.Parse("5f58f9f3-efd5-4010-bc72-c451eb45b8fe"),
                    GroupId = grp,
                    ImageUrl = "wood6.jpg",
                    Name = "Слон",
                    Description = "Дерев'яна настільна підставка для ручок з слоном",
                    Slug = null,
                    Stock = 10,
                    Price = 750.0M,
                }
            );
        }
    }
}
