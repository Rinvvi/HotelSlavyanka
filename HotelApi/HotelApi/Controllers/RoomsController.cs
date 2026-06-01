using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelApi.Data;
using HotelApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        [HttpPost("{roomNumber}/checkin")]
        public async Task<IActionResult> CheckIn(string roomNumber, [FromBody] string guestName)
        {
            Console.WriteLine($"CHECKIN: {roomNumber} {guestName}");

            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Number == roomNumber);

                if (room == null)
                    return NotFound();

                room.Status = "Занят";
                room.CurrentGuest = guestName;

                await _context.SaveChangesAsync();

                Console.WriteLine("SAVE OK");

                return Ok(room);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, ex.ToString());
            }
        
        }

        // POST: api/rooms/{roomNumber}/checkout
        [HttpPost("{roomNumber}/checkout")]
        public async Task<IActionResult> CheckOut(string roomNumber)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Number == roomNumber);
            if (room == null)
            {
                return NotFound($"Комната №{roomNumber} не найдена в базе данных SQLite.");
            }

            room.Status = "Свободен";
            room.CurrentGuest = string.Empty;

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(room);
        }
    }
}