using System.Collections.ObjectModel;
using System.Windows;

namespace HotelDesktop
{
    public partial class EmployeesWindow : Window
    {
        public class Employee
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Position { get; set; }
        }

        private ObservableCollection<Employee> employees =
            new ObservableCollection<Employee>();

        public EmployeesWindow()
        {
            InitializeComponent();

            employees.Add(new Employee
            {
                Id = 1,
                FullName = "Иванов И.И.",
                Position = "Администратор"
            });

            employees.Add(new Employee
            {
                Id = 2,
                FullName = "Петрова А.А.",
                Position = "Ресепшен"
            });

            EmployeesGrid.ItemsSource = employees;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            employees.Add(new Employee
            {
                Id = employees.Count + 1,
                FullName = "Новый сотрудник",
                Position = "Ресепшен"
            });
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is Employee employee)
            {
                employees.Remove(employee);
            }
        }
    }
}