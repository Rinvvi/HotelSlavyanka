using System.Collections.ObjectModel;
using System.Windows;

namespace HotelDesktop
{
    public partial class UsersWindow : Window
    {
        public class User
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string Role { get; set; }
        }

        private ObservableCollection<User> users =
            new ObservableCollection<User>();

        public UsersWindow()
        {
            InitializeComponent();

            users.Add(new User
            {
                Id = 1,
                Login = "admin",
                Role = "admin"
            });

            users.Add(new User
            {
                Id = 2,
                Login = "reception",
                Role = "reception"
            });

            UsersGrid.ItemsSource = users;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User
            {
                Id = users.Count + 1,
                Login = "newuser",
                Role = "reception"
            });
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User user)
            {
                users.Remove(user);
            }
        }
    }
}