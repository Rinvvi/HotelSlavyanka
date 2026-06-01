using Microsoft.EntityFrameworkCore;
using HotelApi.Models;

namespace HotelApi.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}