using HotelApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 1. Явно указываем локальный порт бэкенда для связи с WPF
builder.WebHost.UseUrls("http://localhost:5157");

// 2. НАСТРОЙКА СЛУЖБ (СТРОГО ДО builder.Build())
builder.Services.AddControllers();

// Добавляем CORS, чтобы WPF-окна могли слать запросы к API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Настройка подключения к локальной БД SQLite
// Находим путь к домашней папке пользователя Windows (например, C:\Users\Имя)
string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string dbPath = System.IO.Path.Combine(userFolder, "hotel_slavyanka.db");

builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. СБОРКА ПРИЛОЖЕНИЯ (После этой строки службы настраивать нельзя!)
var app = builder.Build();

// 4. КОНФИГУРАЦИЯ КОНВЕЙЕРА ОБРАБОТКИ (ПОСЛЕ builder.Build())
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

    // Создаем файл базы данных и таблицы, если их нет
    db.Database.EnsureCreated();
    Console.WriteLine("Guests count = " + db.Guests.Count());
    Console.WriteLine("Bookings count = " + db.Bookings.Count());
    Console.WriteLine("Categories count = " + db.Categories.Count());
    Console.WriteLine("Users count = " + db.Users.Count());
    Console.WriteLine("Payments count = " + db.Payments.Count());
    Console.WriteLine("Services count = " + db.Services.Count());
    Console.WriteLine("Employees count = " + db.Employees.Count());

    // ПРОВЕРКА: Если таблица комнат пустая — принудительно добавляем их!
    if (!db.Rooms.Any())
    {
        string[] initialRooms = {
            "101", "102", "103", "104", "105",
            "201", "202", "203", "204", "205",
            "301", "302", "303", "304", "305",
            "401", "402", "403", "404", "405"
        };

        foreach (var num in initialRooms)
        {
            db.Rooms.Add(new HotelApi.Models.Room
            {
                Number = num,
                Status = "Свободен",
                CurrentGuest = string.Empty
            });
        }
        db.SaveChanges(); // Сохраняем в файл hotel.db
    }
}

app.Run();
