using System;
using System.Windows;

namespace HotelDesktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordInput.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (login == "admin" && password == "admin")
            {
                MessageBox.Show("Успешный вход! Роль: Администратор системы.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Передаем роль "admin" в конструктор
                MainMenuWindow menu = new MainMenuWindow("admin");
                menu.Show();
                this.Close();
            }
            else if (login == "reception" && password == "123")
            {
                MessageBox.Show("Успешный вход! Роль: Сотрудник ресепшена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Передаем роль "reception" в конструктор
                MainMenuWindow menu = new MainMenuWindow("reception");
                menu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}