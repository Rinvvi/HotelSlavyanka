using System;

namespace HotelApi.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest? Guest { get; set; } // Разрешаем null для связи

        public int RoomId { get; set; }
        public Room? Room { get; set; }   // Разрешаем null для связи

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string? Status { get; set; }
    }
}