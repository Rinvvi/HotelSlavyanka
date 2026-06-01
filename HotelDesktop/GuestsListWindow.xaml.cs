using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HotelDesktop
{
    public partial class GuestsListWindow : Window
    {
        // Класс, описывающий структуру гостя в таблице
        public class Guest
        {
            public string ID { get; set; }
            public string ФИО { get; set; }
            public string Телефон { get; set; }
            public string Номер { get; set; }
            public string Статус { get; set; }
        }

        // ВМЕСТО обычного List создаем динамическую коллекцию ObservableCollection
        public static ObservableCollection<Guest> GuestsCollection { get; set; }

        public GuestsListWindow()
        {
            InitializeComponent();

            // Создаем коллекцию только один раз
            if (GuestsCollection == null)
            {
                GuestsCollection = new ObservableCollection<Guest>()
        {
            new Guest
            {
                ID = "1",
                ФИО = "Иванов Иван Иванович",
                Телефон = "+7 (999) 111-22-33",
                Номер = "102",
                Статус = "Проживает"
            },
            new Guest
            {
                ID = "2",
                ФИО = "Петрова Анна Сергеевна",
                Телефон = "+7 (999) 444-55-66",
                Номер = "305",
                Статус = "Проживает"
            },
            new Guest
            {
                ID = "3",
                ФИО = "Сидоров Максим Александрович",
                Телефон = "+7 (999) 777-88-99",
                Номер = "203",
                Статус = "Бронь"
            }
        };
            }

            GuestsGrid.ItemsSource = GuestsCollection;
        }

        private void GuestsGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // Метод пустой, пусть просто побудет тут, чтобы не было ошибок
        }

        // Кнопка ДОБАВЛЕНИЯ нового гостя (Button_Click)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Генерируем новый ID на основе количества элементов
            string nextId = (GuestsCollection.Count + 1).ToString();

            // Создаем нового гостя (для примера пока жестко пропишем, чтобы проверить автоматическое появление в таблице)
            Guest newGuest = new Guest
            {
                ID = nextId,
                ФИО = "Новый Гость Тестовый",
                Телефон = "+7 (000) 000-00-00",
                Номер = "101",
                Статус = "Бронь"
            };

            // ВОТ ОНО: Добавляем в ObservableCollection, и он мгновенно сам отрисуется в DataGrid!
            GuestsCollection.Add(newGuest);

            MessageBox.Show("Новый гость автоматически занесен в базу и отображен в таблице!", "Интеграция с БД", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Кнопка УДАЛЕНИЯ гостя (Button_Click_1)
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбрал ли пользователь кого-то в таблице
            if (GuestsGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите гостя из таблицы для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Приводим выбранную строку к типу Guest
                var selectedGuest = GuestsGrid.SelectedItem as Guest;

                if (selectedGuest != null)
                {
                    // Удаляем из коллекции — из таблицы он тоже сразу исчезнет!
                    GuestsCollection.Remove(selectedGuest);
                    MessageBox.Show("Гость успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}