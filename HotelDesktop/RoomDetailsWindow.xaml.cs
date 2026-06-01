using System;
using System.Windows;

namespace HotelDesktop
{
    public partial class RoomDetailsWindow : Window
    {
        private string _roomNumber;

        public string EnteredGuestName { get; private set; }
        public string EnteredGuestPhone { get; private set; }

        public RoomDetailsWindow(string roomNumber)
        {
            InitializeComponent();

            _roomNumber = roomNumber;
            Title = $"Управление номером №{_roomNumber}";

            EnteredGuestName = string.Empty;
            EnteredGuestPhone = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GuestNameBox.Text))
            {
                MessageBox.Show("Введите ФИО гостя!");
                return;
            }

            EnteredGuestName = GuestNameBox.Text.Trim();
            EnteredGuestPhone = GuestPhoneBox.Text.Trim();

            DialogResult = true;
            Close();
        }


        // Кнопка "Выселить / Отмена"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void GuestPhoneBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}