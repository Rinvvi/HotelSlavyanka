using System.ComponentModel.DataAnnotations;

namespace HotelApi.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; } // Первичный ключ
        public string Number { get; set; } // Номер комнаты (например, "101")
        public string Status { get; set; } // Статус ("Свободен", "Занят", "Уборка")
        public string CurrentGuest { get; set; } // ФИО проживающего гостя
    }
}