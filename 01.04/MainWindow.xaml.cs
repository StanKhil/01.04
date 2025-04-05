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

    }
}