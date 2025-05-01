using _01._04.Data;
using _01._04.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Windows;
using Dapper;

namespace _01._04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext _context;
        private UserAccess authUser;
        public MainWindow()
        {
            _context = new DataContext();
            InitializeComponent();
            
        }

        /*private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var oldiest = _context.Users.OrderBy(u => u.BirthDate).FirstOrDefault();
            TextBlock1.Text += $"{oldiest?.Name} {oldiest?.BirthDate}\n";
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var query = _context
                .Users
                .OrderByDescending(u => u.RegisteredAt);
            int count = 1;
            foreach (var item in query)
            {
                TextBlock1.Text += $"{count}: {item.Name} {item.RegisteredAt}\n";
                count++;
                if (count > 3) break;
            }


        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            var query = _context.UserRoles
        .GroupJoin(
            _context.UserAccesses,
            role => role.Id,
            access => access.RoleId,
            (role, accesses) => new
            {
                RoleName = role.Description,
                Count = accesses.Count()
            });


            foreach (var item in query)
            {
                TextBlock1.Text += $"{item.RoleName} — {item.Count}\n";
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text += string.Join("\n",
                _context.Users.GroupJoin(
                    _context.UserAccesses,
                    u => u.Id,
                    ua => ua.UserId,
                    (u, uas) => $"{u.Name} {uas.Count()}"
                    ));


            TextBlock1.Text += "\n\n";

            foreach (User user in _context.Users.Include(u => u.userAccesses))
            {
                TextBlock1.Text += $"{user.Name} {user.userAccesses.Count}\n";
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            *//*TextBlock1.Text = String.Join("\n", _context
                .Users
                .FromSqlRaw(@"SELECT Users.* FROM Users
                JOIN UserAccesses on Users.id = UserAccesses.UserId
                JOIN UserRoles on UserAccesses.RoleId = UserRoles.Id
                WHERE UserRoles.CanUpdate = 1")
                .Select(u => u.Name));*/
            /*TextBlock1.Text = string.Join("\n",
    _context.Users
        .Join(_context.UserAccesses,
            u => u.Id,
            ua => ua.UserId,
            (u, ua) => new { u.Name, ua.RoleId })
        .Join(_context.UserRoles,
            uua => uua.RoleId,
            ur => ur.Id,
            (uua, ur) => new { uua.Name, ur.CanUpdate })
        .Where(u => u.CanUpdate)
        .Select(u => u.Name));

            TextBlock1.Text += String.Join("\n", _context
                .Users
                .AsNoTracking()
                .Include(u => u.userAccesses)
                .ThenInclude(ua => ua.UserRole)
                .Where(u => u.userAccesses
                .Any(ua => ua.UserRole.CanUpdate))
                .Select(u => u.Name)
                ); //Якщо резальтат потрібен тільки для читання можна відключити трекінг

            TextBlock1.Text += String.Join("\n", _context.UserRoles
                .Where(ur => ur.CanUpdate)
                .Include(ur => ur.UserAccesses)
                .ThenInclude(ua => ua.User)
                .Select(ur => String.Join("\n", ur.UserAccesses
                    .Select(ua => ua.User.Name)))
                );
        }*/

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if(authUser == null)
                SignUp(sender, e);
            else
                Update(sender, e);
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            String name = nameTextBox.Text;
            String email = emailTextBox.Text;
            String login = loginTextBox.Text;
            String pass = passTextBox.Password;

            if (_context.UserAccesses.Any(ua => ua.Login == login && ua.User.DeletedAt == null))
            {
                MessageBox.Show("Login already exists");
                return;
            }

            Guid userId = Guid.NewGuid();

            User user = new()
            {
                Id = userId,
                Name = name,
                Email = email,
                RegisteredAt = DateTime.Now,
            };

            String salt = Random.Shared.Next().ToString();
            UserAccess userAccess = new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Login = login,
                Salt = salt,
                Dk = kdf(salt, pass),
                RoleId = "SelfRegistered"
            };

            _context.Users.Add(user);
            _context.UserAccesses.Add(userAccess);

            _context.SaveChanges();

            MessageBox.Show("Registered");

            nameTextBox.Text = "";
            emailTextBox.Text = "";
            loginTextBox.Text = "";
            passTextBox.Password = "";
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            String login = loginTextBoxSignIn.Text;
            String pass = passTextBoxSignIn.Password;
            var userAccess = _context.UserAccesses
                .Include(ua => ua.User)
                .FirstOrDefault(ua => ua.Login == login && ua.User.DeletedAt == null);
            if (userAccess == null)
            {
                MessageBox.Show("User not found");
                return;
            }
            String dk = kdf(userAccess.Salt, pass);
            if (dk!=userAccess.Dk)
            {
                MessageBox.Show("Wrong password");
                return;
            }
            MessageBox.Show($"Welcome {userAccess.User.Name}");

            authUser = userAccess;
            nameTextBox.Text = authUser.User.Name;
            emailTextBox.Text = authUser.User.Email;
            loginTextBox.Text = userAccess.Login;

            loginTextBoxSignIn.Text = "";
            passTextBoxSignIn.Password = "";

            Register.Content = "Update";
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            String name = nameTextBox.Text;
            String email = emailTextBox.Text;
            String login = loginTextBox.Text;
            String pass = passTextBox.Password;
            if (!String.IsNullOrEmpty(login))
            {
                if (_context.UserAccesses.Any(ua => ua.Login == login && ua.User.DeletedAt == null))
                {
                    MessageBox.Show("Login already exists");
                    return;
                }
                authUser.Login = login;
            }
            if (!String.IsNullOrEmpty(name))
                authUser.User.Name = name;

            if (!String.IsNullOrEmpty(email))
                authUser.User.Email = email;
            
                
            if (!String.IsNullOrEmpty(pass))
                authUser.Dk = kdf(authUser.Salt, pass);

            
            _context.SaveChanges();
            MessageBox.Show("Updated");
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (authUser == null)
                return;
            if(MessageBoxResult.Yes == MessageBox.Show("Deleted", "DB", MessageBoxButton.YesNo))
            {
                authUser.User.Name = "";
                authUser.User.Email = "";
                authUser.User.BirthDate = null;
                authUser.User.DeletedAt = DateTime.Now;
                _context.SaveChanges();

                authUser = null;

                Register.Content = "Register";
                nameTextBox.Text = "";
                emailTextBox.Text = "";
                loginTextBox.Text = "";
                passTextBox.Password = "";
            }
        }

        String kdf(String password, String salt)
        {
            int c = 3;
            int dkLength = 20;
            String t = password + salt;
            for(int i = 0; i < c; i++)
            {
                t = hash(t);
            }
            return t[0..dkLength];
        }

        String hash(String input)
        {
            return Convert.ToHexString(System.Security.Cryptography.SHA1.HashData(Encoding.UTF8.GetBytes(input)));
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            // Зазначаємо рядок підключення та провайдер БД
            SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("LocalDb"));
            /*String cntSql = "SELECT COUNT(*) FROM ProductGroups";
            int cnt = sqlConnection.ExecuteScalar<int>(cntSql);
            TextBlock1.Text += $"Count: {cnt}";
            String sqlTime = "SELECT CURRENT_TIMESTAMP";
            TextBlock1.Text += $"DbTime {sqlConnection.ExecuteScalar<DateTime>(sqlTime)}";

            String sqlId = "SELECT NEWID()";
            TextBlock1.Text += $"\nNewId: {sqlConnection.ExecuteScalar<Guid>(sqlId)}";*/

            /*String sql = "SELECT TOP 2 * FROM Products ORDER BY Price ASC";
            Product product = sqlConnection.QueryFirst<Product>(sql);
            TextBlock1.Text += $"Most expensive product: {product.Name} {product.Price}";*/

            /*String sql = "SELECT TOP 2 * FROM Products WHERE Price<0";
            var products = sqlConnection.QueryFirstOrDefault<Product>(sql);
            TextBlock1.Text += $"Most expensive product: {products?.Name} {products?.Price}";*/

            /*var products = sqlConnection.Query<Product>("Select * From Products");
            foreach (var product in products)
            {
                TextBlock1.Text += $"{product.Name} {product.Price}\n";
            }*/

            /*String sql = "SELECT TOP 3 * FROM Products WHERE Price BETWEEN @minPrice and @maxPrice";

            var products = sqlConnection.Query<Product>(sql, new { minPrice = 10, maxPrice = 100 });

            foreach (var product in products)
            {
                TextBlock1.Text += $"{product.Name} {product.Price}\n";
            }*/

            /*String sql = "SELECT TOP 3 * FROM Products WHERE Name IN @names";
            var products = sqlConnection.Query<Product>(sql, new { names = new[] { "Склянка", "Груша" } });

            foreach (var product in products)
            {
                TextBlock1.Text += $"{product.Name} {product.Price}\n";
            }*/

            sqlConnection.Execute("UPDATE Products SET Price = 55 WHERE Name = @name", new { name = "Склянка" });

            String sql = "SELECT TOP 3 * FROM Products Where Price = 55";
            var products = sqlConnection.Query<Product>(sql);
            foreach (var product in products)
            {
                TextBlock1.Text += $"{product.Name} {product.Price}\n";
            }
        }
    }
}