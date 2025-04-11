using _01._04.Data;
using _01._04.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01._04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext _context;
        public MainWindow()
        {
            _context = new DataContext();
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
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
            /*TextBlock1.Text = String.Join("\n", _context
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
        .Select(u => u.Name));*/

            /*TextBlock1.Text += String.Join("\n", _context
                .Users
                .Include(u => u.userAccesses)
                .ThenInclude(ua => ua.UserRole)
                .Where(u => u.userAccesses
                .Any(ua => ua.UserRole.CanUpdate))
                .Select(u => u.Name)
                );*/

            TextBlock1.Text += String.Join("\n", _context.UserRoles
                .Where(ur => ur.CanUpdate)
                .Include(ur => ur.UserAccesses)
                .ThenInclude(ua => ua.User)
                .Select(ur => String.Join("\n", ur.UserAccesses
                    .Select(ua => ua.User.Name)))
                );
        }
    }
}