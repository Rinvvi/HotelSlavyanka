using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HotelDesktop
{
    public partial class MainMenuWindow : Window
    {
        private readonly HttpClient _httpClient;
        private string _userRole;

        public MainMenuWindow(string role)
        {
            InitializeComponent();
            var handler = new HttpClientHandler
            {
                UseProxy = false
            };

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri("http://127.0.0.1:5157");
            _userRole = role;
            ApplyRestrictions();
        }

        private void ApplyRestrictions()
        {
            if (_userRole == "reception")
            {
                UsersButton.Visibility = Visibility.Collapsed;
                EmployeesButton.Visibility = Visibility.Collapsed;

                Title = "Панель ресепшена";
            }
            else
            {
                Title = "Панель администратора";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            GuestsListWindow guests = new GuestsListWindow();
            guests.ShowDialog();
        }

        private async void Room_Click(object sender, RoutedEventArgs e)
        {
            Button roomButton = sender as Button;
            if (roomButton == null || roomButton.Content == null)
                return;

            string roomContent = roomButton.Content.ToString();
            string roomNumber = roomContent.Split(' ')[0].Replace("№", "").Trim();

            if (roomContent.Contains("Свободен") || roomContent.Contains("Уборка"))
            {
                RoomDetailsWindow detailsWindow = new RoomDetailsWindow(roomNumber);

                if (detailsWindow.ShowDialog() == true)
                {
                    string realGuestName = detailsWindow.EnteredGuestName;
                    string realGuestPhone = detailsWindow.EnteredGuestPhone;

                    if (string.IsNullOrWhiteSpace(realGuestName))
                        return;

                    try
                    {
                        var content = new StringContent(
                            $"\"{realGuestName}\"",
                            Encoding.UTF8,
                            "application/json");

                        HttpResponseMessage response =
                            await _httpClient.PostAsync(
                            $"/api/rooms/{roomNumber}/checkin",
                            content);

                        string responseText =
                            await response.Content.ReadAsStringAsync();

                        
                        if (!response.IsSuccessStatusCode)
                            return;
                        if (GuestsListWindow.GuestsCollection == null)
                        {
                            GuestsListWindow.GuestsCollection =
                                new System.Collections.ObjectModel.ObservableCollection<GuestsListWindow.Guest>();
                        }

                        string nextId =
                            (GuestsListWindow.GuestsCollection.Count + 1).ToString();

                        GuestsListWindow.GuestsCollection.Add(
    new GuestsListWindow.Guest
    {
        ID = nextId,
        ФИО = realGuestName,
        Телефон = realGuestPhone,
        Номер = roomNumber,
        Статус = "Проживает"
    });

                        roomButton.Content = $"№{roomNumber} (Занят)";
                        roomButton.Background =
                            new SolidColorBrush(Color.FromRgb(239, 68, 68));

                        MessageBox.Show(
                            $"Гость {realGuestName} успешно заселен!",
                            "Успех",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.ToString(),
                            "Ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            else if (roomContent.Contains("Занят"))
            {
                var result = MessageBox.Show(
                    $"Выселить гостя из номера №{roomNumber}?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        HttpResponseMessage response =
                           await _httpClient.PostAsync(
    $"/api/rooms/{roomNumber}/checkout",
    null);

                        string responseText =
                            await response.Content.ReadAsStringAsync();

                        MessageBox.Show(
                            $"HTTP {(int)response.StatusCode}\n\n{responseText}",
                            "Ответ API");

                        if (!response.IsSuccessStatusCode)
                            return;

                        roomButton.Content = $"№{roomNumber} (Свободен)";
                        roomButton.Background =
                            new SolidColorBrush(Color.FromRgb(34, 197, 94));

                        MessageBox.Show(
                            $"Номер №{roomNumber} освобожден!",
                            "Успех",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.ToString(),
                            "Ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) { Room_Click(sender, e); }
        private void Button_Click_2(object sender, RoutedEventArgs e) { }
        private void Button_Click_3(object sender, RoutedEventArgs e) { Room_Click(sender, e); }
        private void Button_Click_4(object sender, RoutedEventArgs e) { Room_Click(sender, e); }
        private void Button_Click_5(object sender, RoutedEventArgs e) { Room_Click(sender, e); }
        private void Button_Click_6(object sender, RoutedEventArgs e) { Room_Click(sender, e); }
        private void Room102_Click(object sender, RoutedEventArgs e) { Room_Click(sender, e); }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow window = new UsersWindow();
            window.ShowDialog();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow window = new EmployeesWindow();
            window.ShowDialog();
        }
    }
}